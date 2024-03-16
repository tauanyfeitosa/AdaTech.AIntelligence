using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AdaTech.AIntelligence.Attributes;
using AdaTech.AIntelligence.Service.DTOs.Interfaces;

namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    public class DTOUserRegister : IUserRegister
    {
        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo Email é inválido!")]
        [EmailDomain("EmailSettings:Domain")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo sobrenome é obrigatório!")]
        [JsonPropertyName("Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo data de nascimento é obrigatório!")]
        [JsonPropertyName("Data_Nascimento")]
        [DateAge(14)]
        public DateOnly DateBirth { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório!")]
        [JsonPropertyName("Senha")]
        [StrongPassword(8)]
        public string Password { get; set; }

        [CPF]
        [Required(ErrorMessage = "O campo CPF é obrigatório!")]
        public string CPF { get; set; }
    }
}
