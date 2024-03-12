using System.ComponentModel.DataAnnotations;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Service.Attributes;

namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    public class DTOUserRegister
    {
        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo Email é inválido!")]
        [EmailDomain("EmailSettings:Domain")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [DateAge(14)]
        public DateOnly DateBirth { get; set; }
        [StrongPassword(8)]
        public string Password { get; set; }
        [CPF]
        public string CPF { get; set; }
    }
}
