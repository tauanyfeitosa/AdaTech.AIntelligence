using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CPFAttributeTests")]
namespace AdaTech.AIntelligence.Service.Attributes
{
    
    public class CPFAttribute : ValidationAttribute
    {
        private static int LenghtCPF = 11;

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
