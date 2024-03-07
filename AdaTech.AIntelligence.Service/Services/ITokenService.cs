namespace AdaTech.AIntelligence.Service.Services
{
    public interface ITokenService
    {
        string GenerateToken(string user, string password);
    }
}