using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserInfo user);
    }
}
