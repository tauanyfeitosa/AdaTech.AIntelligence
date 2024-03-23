using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    public class DeleteUsersNotConfirmed
    {
        private readonly UserManager<UserInfo> _userManager;

        public DeleteUsersNotConfirmed(UserManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        public async Task DeleteUsers()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                if (!user.EmailConfirmed && (user.CreatAt.Date.AddDays(+7) <= DateTime.Now.Date))
                {
                    await _userManager.DeleteAsync(user);
                }
            }
        }
    }
}