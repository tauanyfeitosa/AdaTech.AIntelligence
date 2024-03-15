using AdaTech.AIntelligence.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Entities.Objects
{
    public class RoleRequirement
    {
        public int Id { get; set; }
        public UserInfo UserInfo { get; set; }
        public string UserInfoId { get; set; }
        public Status Status { get; set; }
        public Roles Role { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
