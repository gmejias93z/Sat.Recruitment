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
            RuleFor(user => user.Name).NotEmpty();

            RuleFor(user => user.Email).NotEmpty()
                .DependentRules(() => RuleFor(user => user.Email).EmailAddress());

            RuleFor(user => user.Address).NotEmpty();

            RuleFor(user => user.Phone).NotEmpty()
                .DependentRules(() => RuleFor(user => user.Phone).Matches(PHONE_REGEX));

            // Check that UserType value matches to one of the constants value in UserType class (using reflection).
            RuleFor(user => user.UserType).NotEmpty()
                .DependentRules(() => RuleFor(user => user.UserType)
                    .Must((userType) => typeof(UserType).GetFields()
                    .Select(x => x.GetRawConstantValue())
                    .Contains(userType))
                .WithMessage((context) => $"Invalid {nameof(context.UserType)}."));

            // Custom rule to check that Money string value is convertible to Decimal and check that is equal or greater than 0.
            RuleFor(user => user.Money).NotEmpty()
                .DependentRules(() => RuleFor(user => user.Money).Custom((x, context) =>
            {
                if ((!(decimal.TryParse(x, out decimal value)) || value < 0))
                {
                    context.AddFailure($"Value for '{context.PropertyName}' is not a valid amount format or is less than 0");
                }
            }));
                
        }
    }
}
