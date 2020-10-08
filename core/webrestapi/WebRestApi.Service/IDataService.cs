using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Models.Client;

namespace WebRestApi.Service
{
    public interface IDataService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<IEnumerable<User>> GetUsersByName(string userName);

        Task<User> GetUserById(int id);

        Task<User> DeleteUser(int id);

        Task<User> UpdateUser(ClientUser user);

        Task<User> CreateNewUser(ClientUser user);
    }
}
