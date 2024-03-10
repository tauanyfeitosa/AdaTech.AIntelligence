using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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
        public DateOnly DateBirth { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
    }
}
