using Core;
using Core.Errors;

namespace Services.Responses;

public record CreateContactResponse(List<Error> Errors, Contact Contact) : Response(Errors);