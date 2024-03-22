using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Entities.Objects
{
    /// <summary>
    /// Class to represent the role requirement with properties to control the role requests.
    /// </summary>
    public class RoleRequirement
    {
        public int Id { get; set; }
        public UserInfo UserInfo { get; set; }
        public string UserInfoId { get; set; }
        public Status Status { get; set; }
        public Roles Role { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime CreatAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
    }
}
