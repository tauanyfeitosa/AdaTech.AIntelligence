namespace AdaTech.AIntelligence.Service.Services
{
    public interface IUserAuthService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> RegisterUserAsync(string email, string password);
        Task LogoutAsync();
    }
}
