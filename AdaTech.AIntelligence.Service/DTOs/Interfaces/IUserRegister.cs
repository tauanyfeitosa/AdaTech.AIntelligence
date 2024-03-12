﻿using AdaTech.AIntelligence.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.DTOs.Interfaces
{
    public interface IUserRegister 
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateOnly DateBirth { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
    }
}
