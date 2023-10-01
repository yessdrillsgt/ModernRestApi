using FluentValidation;
using ModernRestApi.Domain.Common;

namespace ModernRestApi.Application.Features.UserFeatures.DeleteUser
{
    public sealed class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.NameEmpty)
                .MaximumLength(50).WithMessage(ValidationErrorMessages.NameMaxReached)
                .Matches(@"^[A-Za-z\s]*$").WithMessage(ValidationErrorMessages.NameLettersSpacesOnly);
        }
    }
}
