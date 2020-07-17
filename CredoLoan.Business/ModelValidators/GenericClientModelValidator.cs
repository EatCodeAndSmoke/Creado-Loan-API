using CredoLoan.Business.Models;
using FluentValidation;
using System.Linq;

namespace CredoLoan.Business.ModelValidators {
    public class GenericClientModelValidator<TClient> : AbstractValidator<TClient> where TClient : ClientReadModel {

        public GenericClientModelValidator() {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("კლიენტის სახელი არ არის მითითებული")
                .MaximumLength(70).WithMessage("კლიენტის სახელის ზომა 70 სიმბოლოზე მეტია");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("კლიენტის გვარი არ არის მითითებული")
                .MaximumLength(70).WithMessage("კლიენტის გვარის ზომა 100 სიმბოლოზე მეტია");

            RuleFor(c => c.PersonalId)
               .NotEmpty().WithMessage("პირადი ნომერი არ არის მითითებული")
               .DependentRules(() => {
                   RuleFor(c => c.PersonalId)
                        .Must(c => c.Length == 11).WithMessage("პირადი ნომერი უნდა შედგებოდეს 11 სიმბოლოსგან")
                        .Must(s => s.All(c => char.IsDigit(c))).WithMessage("პირადი ნომერი უნდა შედგებოდეს მხოლოდ ციფრებისგან");
               });
        }
    }
}
