using Core.Errors;
using Services.Responses;

namespace Services.Requests;

public record DeleteContactResponse(List<Error> Errors) : Response(Errors);