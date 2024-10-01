using Core.Errors;

namespace Services.Requests;

// Base for all requests with basic validation
public abstract record Request()
{
    public abstract List<Error> Validate();
}