using Core;
using Core.Errors;

namespace Services.Responses;

public record GetContactResponse(List<Error> Errors, Contact Contact) : Response(Errors);