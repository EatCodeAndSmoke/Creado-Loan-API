using CredoLoan.Business.Models;
using CredoLoan.Domain.Enums;
using FluentValidation;
using System;

namespace CredoLoan.Business.ModelValidators {
    public class LoanApplicationModelValidator : AbstractValidator<LoanApplicationModel> {

        public LoanApplicationModelValidator() {
            RuleFor(l => l.Currency)
                .Must(b => Enum.IsDefined(typeof(LoanCurrency), b)).WithMessage("არასწორი ვალუტა");
            RuleFor(l => l.LoanType)
                .Must(b => Enum.IsDefined(typeof(LoanType), b)).WithMessage("სესხის არასწორი ტიპი");
            RuleFor(l => l.Period)
                .GreaterThan(0).WithMessage("სესხის პერიოდი უნდა იყოს 0-ზე მეტი");
            RuleFor(l => l.LoanAmount)
                .GreaterThan(0).WithMessage("სესხის თანხა უნდა იყოს 0-ზე მეტი");
        }
    }
}
