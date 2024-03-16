using AdaTech.AIntelligence.Attributes;
using AdaTech.AIntelligence.DbLibrary.Repository;
using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.IoC.Extensions.Filters;
using AdaTech.AIntelligence.Service.Exceptions;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using AdaTech.AIntelligence.Service.Services.UserSystem.PromotionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("User Management")]
    public class UserManagementController : ControllerBase
    {
        private readonly IAIntelligenceRepository<UserInfo> _userRepository;
        private readonly ILogger<PromotionController> _logger;
        private readonly IUserCRUDService _userCRUDService;
        private readonly UserManager<UserInfo> _userManager;

        public UserManagementController(IAIntelligenceRepository<UserInfo> userRepository, ILogger<PromotionController> logger, IUserCRUDService userCRUDService, UserManager<UserInfo> userManager)
        {
            _logger = logger;
            _userCRUDService = userCRUDService;
            _userManager = userManager;
            _userRepository = userRepository;
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
            var success = await _userCRUDService.GetOne(id);

            if (success == null)
                throw new NotFoundException("Não existe um usuário cadastrado com o Id fornecido.");

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
            var success = await _userCRUDService.GetAll();

            if (success == null)
                throw new NotFoundException("Não existem usuários cadastrados.");

            return Ok(success);
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