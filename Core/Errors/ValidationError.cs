namespace Core.Errors;

public record ValidationError(string Message) : Error(Message);