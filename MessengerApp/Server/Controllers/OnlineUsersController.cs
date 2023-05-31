using MessengerApp.Server.Entyties;
using MessengerApp.Server.Services;
using MessengerApp.Server.Services.Interfaces;
using MessengerApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MessengerApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineUsersController : Controller
    {
        private readonly IUserService _userService;
        public OnlineUsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{userId}/Status")]
        public async Task<ActionResult<bool>> UserOnlineStatus(string userId)
        {
            try
            {
                var status = await _userService.IsUserOnline(userId);

                return Ok(status);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SetUserStatusOnline([FromBody] OnlineUserDTO onlineUserDTO)
        {
            try
            {
                await _userService.AddUserToOnlineStatus(onlineUserDTO.OnlineUserId, onlineUserDTO.OnlineUserConnectionId);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("[action]/{userId}")]
        public async Task<ActionResult> RemoveUserFromCnline(string userId)
        {
            try
            {
                await _userService.RemoveUserFromOnlineStatus(userId);
                
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
