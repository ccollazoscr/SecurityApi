using FluentValidation;
using Security.Application.Command;
using Security.Common.Exception;

namespace Security.Application.Validator
{
    public class AuthenticateCommandValidator: AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator() {
            RuleFor(item => item.Username)
            .NotEmpty().WithMessage($"{EnumErrorCode.UserNameMandatory.GetHashCode()}|Username is required")
            .MaximumLength(64).WithMessage($"{EnumErrorCode.UserNameLength.GetHashCode()}|Username must not exceed 64 characters");

            RuleFor(item => item.Password)
           .NotEmpty().WithMessage($"{EnumErrorCode.PasswordMandatory.GetHashCode()}|Password address is required")
           .MaximumLength(128).WithMessage($"{EnumErrorCode.PasswordLength.GetHashCode()}|Password must not exceed 128 characters");
        }
    }
}
