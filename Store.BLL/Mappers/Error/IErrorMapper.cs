using FluentValidation.Results;
using Store.Common;

namespace Store.BLL
{
    public interface IErrorMapper
    {
        Dictionary<string, List<Error>> MapError(ValidationResult validationResult);
    }
}