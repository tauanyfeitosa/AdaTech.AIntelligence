using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Entities.Objects
{

    public class UserAuth : IdentityUser
    {
        public bool IsStaff { get; set; } = false;
        public bool IsSuperUser { get; set; } = false;
        public bool IsLogged { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
