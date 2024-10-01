using Core;
using Services.Requests;
using Services.Responses;

namespace Services;

public interface IContactService
{
    // CRUD abstraction for contacts
    Task<CreateContactResponse> CreateContact(CreateContactRequest request, CancellationToken cancellationToken = default);
    Task<UpdateContactResponse> UpdateContact(UpdateContactRequest request, CancellationToken cancellationToken = default);
    Task<DeleteContactResponse> DeleteContact(int id, CancellationToken cancellationToken = default);
    Task<GetContactResponse> GetContact(int id, CancellationToken cancellationToken = default);
    Task<GetContactsListResponse> GetContactsList(GetContactListRequest request, CancellationToken cancellationToken = default);
}