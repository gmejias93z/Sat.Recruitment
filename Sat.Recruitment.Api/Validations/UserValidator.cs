using FluentValidation;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Api.Data;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Api.Validations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        private readonly Regex PHONE_REGEX = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
        public UserValidator()
        {

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
            RuleFor(user => user.Name).NotNull();
            RuleFor(user => user.Email).NotNull();
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.Address).NotNull();
            RuleFor(user => user.Phone).NotNull();
            RuleFor(user => user.Phone).Matches(PHONE_REGEX);
            RuleFor(user => user.UserType).NotNull();

            RuleFor(user => user.UserType).Must((userType) => typeof(UserType).GetFields()
                .Select(x => x.GetRawConstantValue())
                .Contains(userType))
                .WithMessage("Invalid UserType.");

            RuleFor(user => user.Money).NotNull()
                .Custom((x, context) =>
                {
                    if ((!(decimal.TryParse(x, out decimal value)) || value < 0))
                    {
                        context.AddFailure($"Value for '{context.PropertyName}' is not a valid amount or is less than 0");
                    }
                });
        }
    }
}
