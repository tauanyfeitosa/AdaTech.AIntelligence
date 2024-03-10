using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.DTOs.ModelRequest;

namespace AdaTech.AIntelligence.Service.Services
{
    public interface IUserAuthService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> RegisterUserAsync(DTOUserRegister userRegister);
        Task LogoutAsync();
    }
}
