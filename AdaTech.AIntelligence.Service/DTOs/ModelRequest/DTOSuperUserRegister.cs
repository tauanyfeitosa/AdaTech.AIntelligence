using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Attributes;
using AdaTech.AIntelligence.Service.DTOs.Interfaces;

namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    public class DTOSuperUserRegister : IUserRegister
    {
        [Required(ErrorMessage = "O campo email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo Email é inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo sobrenome é obrigatório!")]
        [JsonPropertyName("Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo data de nascimento é obrigatório!")]
        [JsonPropertyName("Data_Nascimento")]
        [DateAge(18)]
        public DateOnly DateBirth { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório!")]
        [JsonPropertyName("Senha")]
        [StrongPassword(8)]
        public string Password { get; set; }

        [CPF]
        [Required(ErrorMessage = "O campo CPF é obrigatório!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo permissões é obrigatório!")]
        [JsonPropertyName("Permissões")]
        public List<Roles> Roles { get; set; }
    }
}
