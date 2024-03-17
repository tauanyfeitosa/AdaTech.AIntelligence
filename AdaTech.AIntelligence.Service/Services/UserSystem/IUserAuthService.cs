using AdaTech.AIntelligence.Service.DTOs.Interfaces;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    /// <summary>
    /// Interface for user authentication and registration services.
    /// </summary>
    public interface IUserAuthService
    {
        /// <summary>
        /// Authenticates a user with the provided email and password asynchronously.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the authentication is successful; otherwise, false.</returns>
        Task<bool> AuthenticateAsync(string email, string password);

        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="userRegister">The user registration information.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the registration is successful; otherwise, false.</returns>
        Task<bool> RegisterUserAsync(IUserRegister userRegister);

        /// <summary>
        /// Logs out the currently authenticated user asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task LogoutAsync();
    }
}
