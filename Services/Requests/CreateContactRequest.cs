using Core.Errors;
using Services.Helpers;

namespace Services.Requests;

public record CreateContactRequest(string FirstName, string LastName, string Phone, string Email) : Request
{
    public override List<Error> Validate()
    {
        return ContactValidationHelper.ValidateContactStringProperties(FirstName, LastName, Phone, Email);
    }
}