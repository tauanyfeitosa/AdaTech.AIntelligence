using AdaTech.AIntelligence.Service.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;
using AdaTech.AIntelligence.Attributes;
using System.Text.Json.Serialization;

namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    /// <summary>
    /// Represents the DTO for user register.
    /// </summary>
    public class DTOUserRegister : IUserRegister
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo email é inválido.")]
        [EmailDomain("EmailSettings:Domain")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [Required(ErrorMessage = "O campo sobrenome é obrigatório.")]
        [JsonPropertyName("Sobrenome")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birth date of the user.
        /// </summary>
        [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
        [JsonPropertyName("Data_Nascimento")]
        [DateAge(14)]
        public DateOnly DateBirth { get; set; }


        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [JsonPropertyName("Senha")]
        [StrongPassword(8)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the CPF (Brazilian individual taxpayer registry) of the user.
        /// </summary>
        [CPF]
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string CPF { get; set; }
    }
}
