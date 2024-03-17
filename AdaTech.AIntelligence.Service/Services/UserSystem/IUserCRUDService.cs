using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.Service.Services.UserSystem
{
    /// <summary>
    /// Interface for CRUD operations related to users.
    /// </summary>
    public interface IUserCRUDService
    {
        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the creation is successful; otherwise, false.</returns>
        Task<bool> CreateUser(UserInfo user);

        /// <summary>
        /// Retrieves a single user by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. Returns the user if found.</returns>
        Task<UserInfo> GetOne(string id);

        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all users.</returns>
        Task<IEnumerable<UserInfo>> GetAll();

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the update is successful; otherwise, false.</returns>
        Task<bool> UpdateUser(UserInfo user);

        /// <summary>
        /// Deletes a user asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <param name="isHardDelete">A boolean indicating whether to perform a hard delete or a soft delete.</param>
        /// <returns>A task representing the asynchronous operation. Returns a message indicating the result of the delete operation.</returns>
        Task<string> DeleteUser(string id, bool isHardDelete);
    }
}
