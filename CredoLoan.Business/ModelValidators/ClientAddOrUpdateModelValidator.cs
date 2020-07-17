using CredoLoan.Business.Models;
using FluentValidation;
using System.Linq;

namespace CredoLoan.Business.ModelValidators {
    public class ClientAddOrUpdateModelValidator : GenericClientModelValidator<ClientAddOrUpdateModel> {

        public ClientAddOrUpdateModelValidator() {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("პაროლი არ არის მითითებული")
                .DependentRules(() => {
                    RuleFor(c => c.Password)
                        .Must(s => s?.Length >= 8)
                        .WithMessage("პაროლი უნდა შედგებოდეს მინიმუმ 8 სიმბოლოსგან")
                        .Must(s => s.Any(c => char.IsDigit(c)))
                        .WithMessage("პაროლში უნდა იყოს მინიმუმ 1 ციფრი")
                        .Must(s => s.Any(c => char.IsUpper(c)))
                        .WithMessage("პაროლში უნდა იყოს მინიმუმ 1 capital ასო");
                });
        }
    }
}
