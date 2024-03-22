using AdaTech.AIntelligence.Service.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Attributes;
using System.Text.Json.Serialization;

namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    /// <summary>
    /// Represents the DTO for registering a super user.
    /// </summary>
    public class DTOSuperUserRegister : IUserRegister
    {
        /// <summary>
        /// Gets or sets the email of the super user.
        /// </summary>
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the first name of the super user.
        /// </summary>
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [JsonPropertyName("Nome")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the last name of the super user.
        /// </summary>
        [Required(ErrorMessage = "O campo sobrenome é obrigatório.")]
        [JsonPropertyName("Sobrenome")]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the super user.
        /// </summary>
        [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
        [JsonPropertyName("Data_Nascimento")]
        [DateAge(18)]
        public DateOnly DateBirth { get; set; }

        /// <summary>
        /// Gets or sets the password of the super user.
        /// </summary>
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [JsonPropertyName("Senha")]
        [StrongPassword(8)]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the CPF (Brazilian individual taxpayer registry) of the super user.
        /// </summary>
        [CPF]
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string? CPF { get; set; }

        /// <summary>
        /// Gets or sets the roles of the super user.
        /// </summary>
        [Required(ErrorMessage = "O campo de permissões é obrigatório.")]
        [JsonPropertyName("Permissões")]
        public List<Roles>? Roles { get; set; }
    }
}
