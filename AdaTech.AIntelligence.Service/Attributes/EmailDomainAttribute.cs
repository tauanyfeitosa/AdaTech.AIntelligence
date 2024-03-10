
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace AdaTech.AIntelligence.Service.Attributes
{
    public class EmailDomainAttribute: ValidationAttribute
    {
        private readonly string _domain;

        public EmailDomainAttribute(string domain)
        {
            _domain = domain;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var configuration = (IConfiguration)validationContext.GetService(typeof(IConfiguration));
            var allowedDomain = configuration.GetValue<string>(_domain);

            string email = value as string;

            if (!string.IsNullOrWhiteSpace(email) && email.EndsWith(allowedDomain))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"Email must be from the domain {allowedDomain}.");
            }
        }
    }
}
