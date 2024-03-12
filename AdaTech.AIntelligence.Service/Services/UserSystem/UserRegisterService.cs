using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.DTOs.Interfaces;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    public static class UserRegisterService
    {
        public static Task<UserInfo> RegisterUserAsync(this IUserRegister userRegister)
        {
            var userInfo = new UserInfo
            {
                UserName = userRegister.Email,
                Name = userRegister.Name,
                LastName = userRegister.LastName,
                CPF = userRegister.CPF,
                Email = userRegister.Email,
                DateBirth = new DateTime(userRegister.DateBirth.Year, userRegister.DateBirth.Month, userRegister.DateBirth.Day, 0, 0, 0),
                IsStaff = true,
                
            };
            return Task.FromResult(userInfo);
        }
    }

}
