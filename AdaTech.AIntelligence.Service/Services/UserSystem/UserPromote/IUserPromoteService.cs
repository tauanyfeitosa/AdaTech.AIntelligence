using AdaTech.AIntelligence.Entities.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services.UserSystem.IUserPromote
{
    public interface IUserPromoteService
    {
        Task<bool> PromoteUser(string email);
        Task<bool> RequestPromotion(string email);
    }
}
