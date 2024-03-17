using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    /// <summary>
    /// Service for CRUD operations related to users.
    /// </summary>
    public class UserCRUDService : IUserCRUDService
    {
        private readonly IAIntelligenceRepository<UserInfo> _repository;
        private readonly GenericDeleteService<UserInfo> _deleteService;
        private readonly UserManager<UserInfo> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCRUDService"/> class.
        /// </summary>
        /// <param name="repository">The repository for user data.</param>
        /// <param name="deleteService">The service for user deletion.</param>
        /// <param name="userManager">The user manager.</param>
        public UserCRUDService(IAIntelligenceRepository<UserInfo> repository, GenericDeleteService<UserInfo> deleteService, UserManager<UserInfo> userManager)
        {
            _deleteService = deleteService;
            _repository = repository;
            _userManager = userManager;
        }

        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the creation is successful; otherwise, false.</returns>
        public async Task<bool> CreateUser(UserInfo user)
        {
            var success = await _repository.Create(user);

            return success;
        }

        /// <summary>
        /// Retrieves a single user by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. Returns the user if found.</returns>
        public async Task<UserInfo> GetOne(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all users.</returns>
        public Task<IEnumerable<UserInfo>> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the update is successful; otherwise, false.</returns>
        public async Task<bool> UpdateUser(UserInfo user)
        {
            return await _repository.Update(user);
        }

        /// <summary>
        /// Deletes a user asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <param name="isHardDelete">A boolean indicating whether to perform a hard delete or a soft delete.</param>
        /// <returns>A task representing the asynchronous operation. Returns a message indicating the result of the delete operation.</returns>
        public async Task<string> DeleteUser(string id, bool isHardDelete)
        {
            return await _deleteService.DeleteAsync(_repository, id, isHardDelete);
        }
    }
}
