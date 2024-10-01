using Core;
using Core.Errors;

namespace Services.Responses;

public record GetContactsListResponse(List<Error> Errors, IReadOnlyCollection<Contact> Contacts) : Response(Errors);