using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.Tests")]
namespace AdaTech.AIntelligence.Attributes
{
    /// <summary>
    /// Validates that a given date is of a minimum age.
    /// </summary>
    public class DateAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;
        // <summary>
        /// Initializes need a <see cref="DateAgeAttribute"/> class with the specified minimum age.
        /// </summary>
        /// <param name="minimumAge">The minimum age allowed.</param>
        public DateAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the value is valid.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is DateOnly))
            {
                return new ValidationResult("The value must be of type DateOnly.");
            }


            if (value != null && value is DateOnly)
            {
                DateOnly dateOfBirth = (DateOnly)value;
                int age = CalculateAge(dateOfBirth);

                if (age < _minimumAge)
                {
                    return new ValidationResult($"A idade mínima é {_minimumAge} anos.");
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Calculates the age based on the provided date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth.</param>
        /// <returns>The age calculated based on the date of birth.</returns>
        internal int CalculateAge(DateOnly dateOfBirth)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Today);
            int age = now.Year - dateOfBirth.Year;

            if (dateOfBirth.DayOfYear > now.DayOfYear)
            {
                age--;
            }

            return age;
        }
    }
}
