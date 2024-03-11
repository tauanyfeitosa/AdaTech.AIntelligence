using AdaTech.AIntelligence.Entities.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserInfo user);
    }
}
