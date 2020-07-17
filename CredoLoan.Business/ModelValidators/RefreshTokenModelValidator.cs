using CredoLoan.Business.Models;
using FluentValidation;

namespace CredoLoan.Business.ModelValidators {
    public class RefreshTokenModelValidator : AbstractValidator<RefreshTokenModel> {

        public RefreshTokenModelValidator() {
            RuleFor(rt => rt.AccessToken)
                .NotEmpty().WithMessage("access token is empty");
            RuleFor(rt => rt.RefreshToken)
                .NotEmpty().WithMessage("refresh token is empty");
        }
    }
}
