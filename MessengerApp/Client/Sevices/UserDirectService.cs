using MessengerApp.Client.Sevices.Interfaces;
using MessengerApp.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace MessengerApp.Client.Sevices
{
    public class UserDirectService : IUserDirectService
    {
        private readonly HttpClient _httpClient;
        public UserDirectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatUserDTO> AddUserToChat(string userId, int chatId)
        {
            try
            {
                var userTochat = new AddUserToChatDTO() { ChatId= chatId, UserId = userId };
                var response = await _httpClient.PostAsJsonAsync<AddUserToChatDTO>("api/Direct/AddUserToChat", userTochat);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ChatUserDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<ChatUserDTO>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ChatUserDTO> CreateChat(string creatorId)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Direct/CreateChat", creatorId);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ChatUserDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<ChatUserDTO>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetChatMessages(int chatId)
        {
            try
            {

                var response = await _httpClient.GetAsync($"api/Direct/Chats/{chatId}/Messages");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<MessageDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<List<MessageDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ChatDTO>> GetChats(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Direct/{userId}/Chats"); ;

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ChatDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<List<ChatDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MessageDTO> SendMessage(MessageDTO messageDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Direct/AddMessageToChat", messageDTO);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(MessageDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<MessageDTO>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
