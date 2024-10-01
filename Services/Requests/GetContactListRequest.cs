using Core.Errors;

namespace Services.Requests;

public record GetContactListRequest(string SearchToken, string SortToken, bool Descending, int PageSize, int PageNumber) : Request
{
    public override List<Error> Validate()
    {
        var errors = new List<Error>();

        if (PageSize < 1)
        {
            errors.Add(new ValidationError("Page size must not be less than 1."));
        }
        if (PageNumber < 1)
        {
            errors.Add(new ValidationError("Page number must not be less than 1."));
        }

        return errors;
    }
}