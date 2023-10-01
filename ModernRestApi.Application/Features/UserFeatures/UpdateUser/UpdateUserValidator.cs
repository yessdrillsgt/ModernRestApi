using FluentValidation;
using ModernRestApi.Domain.Common;

namespace ModernRestApi.Application.Features.UserFeatures.UpdateUser
{
    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.NameEmpty)
                .MaximumLength(50).WithMessage(ValidationErrorMessages.NameMaxReached)
                .Matches(@"^[A-Za-z\s]*$").WithMessage(ValidationErrorMessages.NameLettersSpacesOnly);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(ValidationErrorMessages.AddressEmpty)
                .MinimumLength(3).WithMessage(ValidationErrorMessages.AddressMinLength)
                .MaximumLength(50).WithMessage(ValidationErrorMessages.AddressMaxReached);
        }
    }
}
