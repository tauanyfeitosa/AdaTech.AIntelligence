﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.Tests")]
namespace AdaTech.AIntelligence.Attributes
{
    /// <summary>
    /// Validates that a given password meets the criteria for a strong password.
    /// </summary>
    public partial class StrongPasswordAttribute : ValidationAttribute
    {
        [GeneratedRegex(@"[^A-Za-z0-9]")]
        private static partial Regex specialCharRegex();

        [GeneratedRegex("\\d")]
        private static partial Regex digitRegex();

        [GeneratedRegex("[a-z]")]
        private static partial Regex lowerCaseRegex();

        [GeneratedRegex("[A-Z]")]
        private static partial Regex upperCaseRegex();

        private readonly int _minumumLength;
        private readonly int MimumumCharCombinations = 3;

        /// <summary>
        /// Initializes <see cref="StrongPasswordAttribute"/> class with the specified minimum length.
        /// </summary>
        /// <param name="minimumLength">The minimum length required for the password.</param>
        public StrongPasswordAttribute(int minimumLength)
        {
            _minumumLength = minimumLength;
        }

        /// <summary>
        /// Determines whether the specified value is a strong password.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the value is a strong password.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("A senha não pode ser vazia.");
            }
            if (password.Length < _minumumLength)
            {
                return new ValidationResult($"A senha deve ter no mínimo {_minumumLength} caracteres.");
            }
            if (!HasRequiredCombinations(password))
            {
                return new ValidationResult("A senha deve conter ao menos 3 das seguintes combinações: letra maiúscula, letra minúscula, caractere especial ou número.");
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks if password contains the required combinations.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password contains the required combinations; otherwise, false.</returns>
        internal bool HasRequiredCombinations(string password)
        {
            var hasUpperCase = upperCaseRegex().IsMatch(password);
            var hasLowerCase = lowerCaseRegex().IsMatch(password);
            var hasDigit = digitRegex().IsMatch(password);
            var hasSpecialChar = specialCharRegex().IsMatch(password);

            var combinations = new[] { hasUpperCase, hasLowerCase, hasDigit, hasSpecialChar };
            var countTrue = combinations.Count(c => c);

            return countTrue >= MimumumCharCombinations;
        }
    }
}
