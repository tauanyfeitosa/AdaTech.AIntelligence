using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Service.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    public class DTOUserLogin
    {
        [Required(ErrorMessage = "O campo email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo Email é inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório! O campo senha deve ter no minimo 8 caracteres")]
        [JsonPropertyName("Senha")]
        public string Password { get; set; }
    }
}
