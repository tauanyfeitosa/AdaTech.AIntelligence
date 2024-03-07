namespace AdaTech.AIntelligence.Service.Services
{
    public interface IUserService
    {
        Task<(bool Succeeded, string Role)> AuthenticateAsync(string email, string password);
        Task<bool> RegisterUserAsync(string email, string password);
        Task LogoutAsync();
    }
}
