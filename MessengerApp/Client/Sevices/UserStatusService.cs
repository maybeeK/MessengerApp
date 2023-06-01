using MessengerApp.Client.Sevices.Interfaces;
using MessengerApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Client.Sevices
{
    public class UserStatusService : IUserStatusService
    {
        private readonly HttpClient _httpClient;
        public UserStatusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task AddUserToOnlineStatus(OnlineUserDTO onlineUserDTO)
        {
            try
            {
                if (!(await IsUserOnline(onlineUserDTO.OnlineUserId)))
                {
                    var response = await _httpClient.PostAsJsonAsync<OnlineUserDTO>("api/OnlineUsers/SetUserStatusOnline", onlineUserDTO);

                    if (!response.IsSuccessStatusCode)
                    {
                        var message = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> IsUserOnline(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/OnlineUsers/{userId}/Status");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
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

        public async Task RemoveUserFromOnlineStatus(string connectionId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/OnlineUsers/RemoveUserFromCnline/{connectionId}");

                if (!response.IsSuccessStatusCode)
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
