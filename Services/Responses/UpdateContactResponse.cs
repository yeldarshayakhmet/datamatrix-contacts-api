using Core;
using Core.Errors;

namespace Services.Responses;

public record UpdateContactResponse(List<Error> Errors, Contact Contact) : Response(Errors);