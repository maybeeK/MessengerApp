using MessengerApp.Server.Entyties;
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
        [Route("Chats/{chatId}/Users")]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetChatUsers(int chatId)
        {
            try
            {
                var chatUsers = await _directService.GetChatUsers(chatId);
                var users = await _directService.GetUsers();

                var usersInChat = (from user in chatUsers
                                   join appUser in users
                                   on user.UserId equals appUser.Id
                                   where user.ChatId == chatId
                                   select new AppUserDTO
                                   {
                                       Id = user.UserId,
                                       Email = appUser.Email
                                   }).ToList();

                if (usersInChat.Count == 0)
                {
                    return NoContent();
                }

                return usersInChat;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Users")]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetUsers()
        {
            try
            {
                var users = await _directService.GetUsers();

                if (users == null)
                {
                    throw new Exception();
                }

                var usersDto = users.ConvertToDto();

                return Ok(usersDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
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
        [Route("Chats/{chatId}/Messages")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetChatMessages(int chatId)
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

        [HttpPost("[action]")]
        public async Task<ActionResult<ChatUserDTO>> CreateChat([FromBody] string creatorId)
        {
            try
            {
                var chatUser = await _directService.CreateChat(creatorId);

                var chatUserDto = chatUser.ConvertToDto();
                
                return Ok(chatUserDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ChatUserDTO>> AddUserToChat([FromBody] AddUserToChatDTO userToChatDTO)
        {
            try
            {
                var user = await _directService.AddUserToChat(userToChatDTO.UserId, userToChatDTO.ChatId);

                if (user == null)
                {
                    return BadRequest();
                }

                var userDto = user.ConvertToDto();

                return Ok(userDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<MessageDTO>> AddMessageToChat([FromBody]MessageDTO messageDto)
        {
            try
            {
                var addedMessage = await _directService.AddMessageToChat(messageDto);

                if (addedMessage == null)
                {
                    return BadRequest();
                }

                return Ok(addedMessage);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult<ChatDTO>> RenameChat(ChatDTO newNamedChat)
        {
            try
            {
                var renamedChat = await _directService.RenameChat(newNamedChat);

                if (renamedChat == null)
                {
                    BadRequest();
                }

                return Ok(renamedChat);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("[action]/{userId}/{chatId:int}")]
        public async Task<ActionResult<ChatUserDTO>> RemoveUserFromChat(string userId, int chatId)
        {
            try
            {
                var userToRemove = new ChatUser() { UserId = userId, ChatId = chatId };

                var removedUser = await _directService.RemoveUserFromChat(userToRemove);

                if (removedUser == null)
                {
                    return NotFound();
                }

                var userDto = removedUser.ConvertToDto();

                return Ok(userDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
