using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial
{
    public class RoleManagerService
    {
        private readonly UserManager<UserInfo> _userManager;

        public RoleManagerService(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        public async Task AssignRolesAsync(UserInfo user, params string[] roles)
        {
            foreach(var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
