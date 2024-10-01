using System.Text.RegularExpressions;
using Core.Errors;

namespace Services.Helpers;

public static class ContactValidationHelper
{
    // Validation for basic contact properties, used by create and update requests
    public static List<Error> ValidateContactStringProperties(string firstName, string lastName, string phone,
        string email)
    {
        var errors = new List<Error>();

        // Check required fields
        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add(new ValidationError("First name must not be empty."));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add(new ValidationError("Last name must not be empty."));
        }

        if (string.IsNullOrWhiteSpace(phone))
        {
            errors.Add(new ValidationError("Phone number must not be empty."));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add(new ValidationError("Email must not be empty."));
        }

        // Validate email format
        if (!string.IsNullOrWhiteSpace(email)
            && !Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(250)))
        {
            errors.Add(new ValidationError("Invalid email address."));
        }

        return errors;
    }
}