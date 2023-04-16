using System.Data;
using Sat.Recruitment.Api.Models;
using FluentValidation;
using System.Globalization;
using FluentValidation.Validators;
using System.Net.Mail;

namespace Sat.Recruitment.Api.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
            RuleFor(user => user.Name).NotNull();
            RuleFor(user => user.Email).NotNull();
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.Address).NotNull();
            RuleFor(user => user.Phone).NotNull();
            RuleFor(user => user.UserType).NotNull();
            RuleFor(user => user.Money).NotNull();
            RuleFor(user => user.Money).GreaterThanOrEqualTo(0);
        }
    }
}
