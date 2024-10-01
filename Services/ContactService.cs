using Core;
using Core.Errors;
using Data;
using Microsoft.EntityFrameworkCore;
using Services.Requests;
using Services.Responses;

namespace Services;

public class ContactService : IContactService
{
    private readonly IRepository _repository;

    public ContactService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateContactResponse> CreateContact(CreateContactRequest request, CancellationToken cancellationToken = default)
    {
        // Validate the request before processing
        var errors = request.Validate();
        if (errors.Any())
        {
            return new CreateContactResponse(Errors: errors, Contact: null);
        }

        var contact = _repository.Create(new Contact(request.FirstName, request.LastName, request.Phone, request.Email));
        await _repository.SaveAsync(cancellationToken);
        return new CreateContactResponse(null, contact);
    }

    public async Task<UpdateContactResponse> UpdateContact(UpdateContactRequest request, CancellationToken cancellationToken = default)
    {
        var errors = request.Validate();
        if (errors.Any())
        {
            return new UpdateContactResponse(Errors: errors, Contact: null);
        }

        var contact = _repository.EntitySet<Contact>().SingleOrDefault(c => c.Id == request.Id);

        if (contact is null)
        {
            errors.Add(new EntityNotFoundError("Contact not found."));
            return new UpdateContactResponse(Errors: errors, Contact: null);
        }

        contact.FirstName = request.FirstName;
        contact.LastName = request.LastName;
        contact.PhoneNumber = request.Phone;
        contact.Email = request.Email;
        await _repository.SaveAsync(cancellationToken);

        return new UpdateContactResponse(null, contact);
    }

    public async Task<DeleteContactResponse> DeleteContact(int id, CancellationToken cancellationToken = default)
    {
        var contact = await _repository.EntitySet<Contact>().SingleOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        if (contact is null)
        {
            var errors = new List<Error> { new EntityNotFoundError("Contact not found.") };
            return new DeleteContactResponse(Errors: errors);
        }

        _repository.Delete(contact);
        await _repository.SaveAsync(cancellationToken);
        return new DeleteContactResponse(Errors: null);
    }

    public async Task<GetContactResponse> GetContact(int id, CancellationToken cancellationToken = default)
    {
        var contact = await _repository.EntitySet<Contact>().AsNoTracking().SingleOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        if (contact is null)
        {
            var errors = new List<Error> { new EntityNotFoundError("Contact not found.") };
            return new GetContactResponse(Errors: errors, Contact: null);
        }

        return new GetContactResponse(Errors: null, Contact: contact);
    }

    public async Task<GetContactsListResponse> GetContactsList(GetContactListRequest request, CancellationToken cancellationToken = default)
    {
        var errors = request.Validate();
        if (errors.Any())
        {
            return new GetContactsListResponse(Errors: errors, Contacts: null);
        }

        // Prevent EF Core from tracking entities for memory efficiency, as we won't be modifying them
        var contacts = _repository.EntitySet<Contact>().AsNoTracking();
        // Simple search utilizing indexes
        if (!string.IsNullOrWhiteSpace(request.SearchToken))
        {
            contacts = contacts.Where(c => EF.Functions.Like(c.FirstName, $"{request.SearchToken}%")
            || EF.Functions.Like(c.LastName, $"{request.SearchToken}%")
            || EF.Functions.Like(c.PhoneNumber, $"{request.SearchToken}%")
            || EF.Functions.Like(c.Email, $"{request.SearchToken}%"));
        }

        contacts = request.SortToken switch
        {
            nameof(Contact.FirstName) => request.Descending
                ? contacts.OrderByDescending(c => c.FirstName)
                : contacts.OrderBy(c => c.FirstName),
            nameof(Contact.LastName) => request.Descending
                ? contacts.OrderByDescending(c => c.LastName)
                : contacts.OrderBy(c => c.LastName),
            _ => contacts
        };

        var skip = (request.PageNumber - 1) * request.PageSize;
        contacts = contacts.Skip(skip).Take(request.PageSize);

        return new GetContactsListResponse(Errors: null, Contacts: await contacts.ToListAsync(cancellationToken: cancellationToken));
    }
}