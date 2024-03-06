using FluentValidation;
using Net.WebCore.Validator;
using AppReviewWebApi.Models.Request;

namespace AppReviewWebApi.Validators
{
    public class LoginValidator : BaseValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("UserName must be not blank!");

            RuleFor(model => model.Password).NotEmpty().WithMessage("Password must be not blank!");
        }
    }
}
