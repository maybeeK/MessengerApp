using MessengerApp.Server.Extentions;
using MessengerApp.Server.Services.Interfaces;
using MessengerApp.Shared.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MessengerApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectController : ControllerBase
    {
        private readonly IDirectService _directService;
        public DirectController(IDirectService directService)
        {
            _directService = directService;
        }

        [HttpGet]
        [Route("{userId}/Chats")]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> GetUserChats(string userId)
        {
            try
            {
                var chats = await _directService.GetUserChats(userId);
                if (chats == null)
                {
                    return NoContent();
                }

                var chatsDto = chats.ConvertToDto();

                return Ok(chatsDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{charId}/Messages")]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> GetChatMessages(int chatId)
        {
            try
            {
                var messages = await _directService.GetChatMessages(chatId);

                if (messages == null)
                {
                    return NoContent();
                }

                var messagesDto = messages.ConvertToDto();

                return Ok(messagesDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
