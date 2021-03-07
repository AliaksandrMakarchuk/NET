using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Models.Client;

namespace WebRestApi.Service
{
    public interface IDataService
    {
        Task<IEnumerable<ClientUser>> GetAllUsers();

        Task<IEnumerable<ClientUser>> GetUsersByNameAsync(string userName);

        Task<ClientUser> GetUserByIdAsync(int id);

        Task<ClientUser> DeleteUserAsync(int id);

        Task<ClientUser> UpdateUserAsync(ClientUser user);

        Task<ClientUser> CreateNewUserAsync(IdentityUser user);

        Task<Message> GetMessageById(int id);

        Task<IEnumerable<Message>> GetAllMessagesAsync(ClaimsPrincipal user);

        Task<bool> SendMessageAsync(ClientMessage message);

        Task<ClientMessage> UpdateMessageAsync(ClientMessage message);

        Task<Message> DeleteMessageAsync(int id);
    }
}
