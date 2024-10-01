using Core.Errors;

namespace Services.Responses;

// Base for all request responses
public record Response(List<Error> Errors);