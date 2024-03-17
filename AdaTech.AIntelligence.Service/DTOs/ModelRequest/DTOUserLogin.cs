using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    /// <summary>
    /// Represents the DTO for user login.
    /// </summary>
    public class DTOUserLogin
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required(ErrorMessage = "O campo senha é obrigatório e deve ter no mínimo 8 caracteres.")]
        [JsonPropertyName("Senha")]
        public string Password { get; set; }
    }
}
