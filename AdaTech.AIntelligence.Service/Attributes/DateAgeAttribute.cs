﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DateAgeAttributeTests")]
namespace AdaTech.AIntelligence.Service.Attributes
{
    public class DateAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public DateAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

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

        protected int CalculateAge(DateOnly dateOfBirth)
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
