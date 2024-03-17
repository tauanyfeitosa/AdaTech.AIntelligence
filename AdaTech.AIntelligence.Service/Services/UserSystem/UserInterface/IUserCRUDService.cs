using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.UserSystem.UserInterface
{
    public interface IUserCRUDService
    {
        Task<bool> CreateUser(UserInfo user);
        Task<UserInfo> GetOne(string id);
        Task<IEnumerable<UserInfo>> GetAll();
        Task<bool> UpdateUser(UserInfo user);
        Task<string> DeleteUser(string id, bool isHardDelete);
    }
}