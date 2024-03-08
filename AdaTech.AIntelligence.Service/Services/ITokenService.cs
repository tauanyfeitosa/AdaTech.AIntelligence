using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services
{
    public interface ITokenService
    {
        (string Token, DateTime Expiration) GenerateToken(UserInfo user);
    }
}