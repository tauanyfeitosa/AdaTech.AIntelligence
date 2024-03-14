using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.Tests")]
namespace AdaTech.AIntelligence.Service.Attributes
{
    /// <summary>
    /// Custom validation for CPF format.
    /// </summary>   
    public class CPFAttribute : ValidationAttribute
    {
        private static int LenghtCPF = 11;
        /// <summary>
        /// Validates CPF
        /// </summary>
        /// <param name="value">Value to be validated.</param>
        /// <param name="validationContext">Validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating if CPF is valid or not.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("O CPF não pode ser vazio.");
            }
            string cpf = value.ToString();

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != LenghtCPF || !IsCpfValid(cpf))
            {
                return new ValidationResult("CPF inválido");
            }
            return ValidationResult.Success;
        }
        /// <summary>
        /// Check if a CPF is valid.
        /// </summary>
        /// <param name="cpf">CPF to be checked</param>
        /// <returns><c>true</c>if CPF is valid, <c>false</c> otherwise.</returns>
        private bool IsCpfValid(string cpf)
        {
            int[] numberCPF = cpf.Substring(0, 9).Select(c => int.Parse(c.ToString())).ToArray();
            int[] checkDigits = new int[2];

            for (int i = 0; i < 2; i++)
            {
                int sum = 0;
                int multiplier = 2;

                for (int j = numberCPF.Length - 1; j >= 0; j--)
                {
                    sum += numberCPF[j] * multiplier;
                    multiplier++;
                }

                int rest = sum % 11;

                checkDigits[i] = (rest < 2) ? 0 : 11 - rest;

                if (i == 0)
                {
                    numberCPF = (cpf.Substring(0, 9) + checkDigits[0].ToString()).Select(c => int.Parse(c.ToString())).ToArray();
                }
            }

            return checkDigits[0] == int.Parse(cpf[9].ToString()) && checkDigits[1] == int.Parse(cpf[10].ToString());
        }
    }
}
