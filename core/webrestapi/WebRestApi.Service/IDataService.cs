using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Models.Client;

namespace WebRestApi.Service
{
    public interface IDataService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserByLoginPasswordAsync(string login, string password);

        Task<User> GetUserByIdAsync(int id);

        Task<User> DeleteUserAsync(int id);

        Task<User> UpdateUserAsync(ClientUser user);

        Task<User> CreateNewUserAsync(IdentityUser user);

        Task<Message> GetMessageById(int id);

        Task<IEnumerable<Message>> GetAllMessagesAsync(ClaimsPrincipal user);

        Task<bool> SendMessageAsync(ClientMessage message);

        Task<Message> UpdateMessageAsync(ClientMessage message);

        Task<Message> DeleteMessageAsync(int id);
    }
}
