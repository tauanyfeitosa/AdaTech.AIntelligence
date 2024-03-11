using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AdaTech.AIntelligence.Service.Attributes
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        private readonly int _minumumLength;
        private static int MimumumCharCombinations = 3;

        public StrongPasswordAttribute (int minumumLength)
        {
            _minumumLength = minumumLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;
            if(string.IsNullOrEmpty(password) )
            {
                return new ValidationResult("A senha não pode ser vazia");
            }
            if(password.Length < _minumumLength)
            {
                return new ValidationResult($"A senha deve ter no minimo {_minumumLength} caracteres");
            }
            if(!HasRequiredCombinations(password)) 
            {
                return new ValidationResult("A senha deve conter ao menos 3 das seguintes combinações: letra maiúscula, letra minúscula, caractere especial ou número.");
            }

            return ValidationResult.Success;
        }

        private bool HasRequiredCombinations(string password)
        {
            var hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            var hasLowerCase = Regex.IsMatch(password, "[a-z]");
            var hasDigit = Regex.IsMatch(password, "\\d");
            var hasSpecialChar = Regex.IsMatch(password, @"[^A-Za-z0-9]");

            var combinations = new[] { hasUpperCase, hasLowerCase, hasDigit, hasSpecialChar };
            var countTrue = combinations.Count(c => c);

            return countTrue >= MimumumCharCombinations;
        }
    }
}
