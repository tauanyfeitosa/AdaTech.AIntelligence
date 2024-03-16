using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Entities.Objects
{
    /// <summary>
    /// Class to represent the user authentication with properties to control the user access
    /// </summary>
    public class UserAuth : IdentityUser
    {
        public bool IsStaff { get; set; } = false;
        public bool IsSuperUser { get; set; } = false;
        public bool IsLogged { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreatAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
    }
}
