using AdaTech.AIntelligence.Entities;

namespace AdaTech.AIntelligence.Service.Services
{
    public interface ITokenService
    {
        (string Token, DateTime Expiration) GenerateToken(UserAuth user);
    }
}