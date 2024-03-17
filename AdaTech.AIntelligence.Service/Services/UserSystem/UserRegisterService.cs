using AdaTech.AIntelligence.Service.DTOs.Interfaces;
using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    /// <summary>
    /// Service class to register a new user asynchronously.
    /// </summary>
    public static class UserRegisterService
    {
        /// <summary>
        /// Registers a new user asynchronously based on the provided user registration information.
        /// </summary>
        /// <param name="userRegister">The user registration information.</param>
        /// <returns>A task representing the asynchronous operation. Returns the newly created user.</returns>
        public static Task<UserInfo> RegisterUserAsync(this IUserRegister userRegister)
        {
            var userInfo = new UserInfo
            {
                UserName = userRegister.Email,
                Name = userRegister.Name,
                LastName = userRegister.LastName,
                CPF = userRegister.CPF,
                Email = userRegister.Email,
                DateBirth = new DateTime(userRegister.DateBirth.Year, userRegister.DateBirth.Month, userRegister.DateBirth.Day, 0, 0, 0),
                IsStaff = true
            };
            return Task.FromResult(userInfo);
        }
    }
}
