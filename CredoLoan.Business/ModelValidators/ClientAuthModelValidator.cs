using CredoLoan.Business.Models;
using FluentValidation;

namespace CredoLoan.Business.ModelValidators {
    public class ClientAuthModelValidator : AbstractValidator<ClientAuthModel> {

        public ClientAuthModelValidator() {
            RuleFor(c => c.PersonalId)
                .NotEmpty().WithMessage("პირადი ნომერი არ არის მითითებული");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("პაროლი არ არის მითითებული");
        }
    }
}
