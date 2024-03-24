using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Service.Services.UserSystem.UserInterface;
using AdaTech.WebAPI.SistemaVendas.Utilities.Filters;
using Microsoft.AspNetCore.Authorization;
using AdaTech.AIntelligence.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AdaTech.AIntelligence.Entities.Objects;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("User Management")]
    [TypeFilter(typeof(LoggingActionFilter))]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserCRUDService _userCRUDService;
        private readonly UserManager<UserInfo> _userManager;

        public UserManagementController(IUserCRUDService userCRUDService, UserManager<UserInfo> userManager)
        {
            _userCRUDService = userCRUDService;
            _userManager = userManager;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("view-user-logged")]
        [Authorize]
        public async Task<IActionResult> ViewUserLogged()
        {
            var user = await _userManager.GetUserAsync(User) ?? throw new NotFoundException("Usuário não encontrado.");
            return Ok(new { values = user });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("view-role-user-logged")]
        [Authorize]
        public async Task<IActionResult> ViewRoleUserLogged()
        {
            var user = await _userManager.GetUserAsync(User) ?? throw new NotFoundException("Usuário não encontrado.");
            var roles = await _userManager.GetRolesAsync(user);
            var rolesArray = roles.ToArray();
            return Ok(new { values = rolesArray });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("view-role-user")]
        [Authorize]
        public async Task<IActionResult> ViewRoleUse([FromQuery] string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new NotFoundException("Usuário não encontrado.");
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new { values = roles.ToArray() });
        }

        /// <summary>
        /// View the data of a single user
        /// </summary>
        /// <param name="id">The ID of the user to view.</param>
        /// <returns>A task representing the asynchronous operation. Returns the user if found.</returns>
        [HttpGet("view-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewUser(string id)
        {
            var success = await _userCRUDService.GetOne(id) ?? throw new NotFoundException("Não existe um usuário cadastrado com o Id fornecido.");
            return Ok(success);
        }

        /// <summary>
        /// View all users
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all users.</returns>
        [HttpGet("view-all-users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewAllUsers()
        {
            var success = await _userCRUDService.GetAll() ?? throw new NotFoundException("Não existem usuários cadastrados.");
            return Ok(success.ToArray());
        }

        /// <summary>
        /// View all users actives
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a collection of all users.</returns>
        [HttpGet("view-all-users-actives")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewAllUsersActives()
        {
            var success = await _userCRUDService.GetAll() ?? throw new NotFoundException("Não existem usuários cadastrados.");
            success = success.Where(u => u.IsActive).ToList();
            return Ok(success.ToArray());
        }

        /// <summary>
        /// Delete an user asynchronously
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <param name="isHardDelete">A boolean indicating whether to perform a hard delete or a soft delete.</param>
        /// <returns>A task representing the asynchronous operation. Returns a message indicating the result of the delete operation.</returns>
        [HttpDelete("delete-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id, [FromQuery] bool isHardDelete = false)
        {
            var result = await _userCRUDService.DeleteUser(id, isHardDelete);

            return Ok(result);
        }
    }
}