using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Models;

#pragma warning disable 1591
namespace WebRestApi.Services
{
    public interface IDataService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<IEnumerable<User>> GetUsersByName(string userName);

        Task<User> GetUserById(int id);

        Task<User> DeleteUser(int id);

        Task<User> UpdateUser(User user);

        Task<User> CreateNewUser(string firstName, string lastName);
    }
}
