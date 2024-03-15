
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace AdaTech.AIntelligence.Service.Attributes
{
    /// <summary>
    /// Validates that an email address belongs to a specified domain.
    /// </summary>
    public class EmailDomainAttribute: ValidationAttribute
    {
        private readonly string _domain;

        public EmailDomainAttribute(string domain)
        {
            _domain = domain;
        }

        /// <summary>
        /// Determines whether the specified value is a valid email address for the specified domain.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the value is a valid email address for the specified domain.</returns>
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
                return new ValidationResult($"Email informado não corresponde ao domínio esperado.");
            }
        }
    }
}
