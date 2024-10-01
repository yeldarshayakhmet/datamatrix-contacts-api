namespace Core.Errors;

public record EntityNotFoundError(string Message) : Error(Message);