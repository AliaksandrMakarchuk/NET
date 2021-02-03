using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Models.Client;

namespace WebRestApi.Service
{
    public interface IDataService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<IEnumerable<User>> GetUsersByNameAsync(string userName);

        Task<User> GetUserByIdAsync(int id);

        Task<User> DeleteUserAsync(int id);

        Task<User> UpdateUserAsync(ClientUser user);

        Task<User> CreateNewUserAsync(ClientUser user);

        Task<IEnumerable<Message>> GetAllMessagesAsync();

        Task<bool> SendMessageAsync(ClientMessage message);

        Task<Message> DeleteMessageAsync(int id);
    }
}
