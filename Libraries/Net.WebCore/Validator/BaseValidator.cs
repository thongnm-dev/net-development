using FluentValidation;
using Net.WebCore.Model;

namespace Net.WebCore.Validator
{
    public abstract class BaseValidator<TModel> : AbstractValidator<TModel> where TModel : BaseModel
    {
    }
}