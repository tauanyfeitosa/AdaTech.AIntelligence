using AdaTech.AIntelligence.Service.DTOs.Interfaces;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    public interface IUserAuthService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> RegisterUserAsync(IUserRegister userRegister);
        Task LogoutAsync();
        Task<string> DeleteAsync(int id, bool isHardDelete);
    }
}
