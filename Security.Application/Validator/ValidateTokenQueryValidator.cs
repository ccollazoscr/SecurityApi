using FluentValidation;
using Security.Application.Query;
using Security.Common.Exception;

namespace Security.Application.Validator
{
    public class ValidateTokenQueryValidator : AbstractValidator<ValidateTokenQuery>
    {

        public ValidateTokenQueryValidator()
        {
            RuleFor(item => item.Token)
            .NotEmpty().WithMessage($"{EnumErrorCode.TokenMandatory.GetHashCode()}|Token is required");
        }

    }
}
