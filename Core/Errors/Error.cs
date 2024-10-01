namespace Core.Errors;

// Base class for errors, can be expanded by including error code, description, etc.
public record Error(string Message);