using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Models;

namespace WebRestApi.Repository
{
    public class UserRepository : UserRepositoryBase
    {
        public UserRepository(WebRestApiContext context) : base(context) { }

        public override async Task<IEnumerable<User>> GetByNameAsync(string userName)
        {
            return await Context.Users.FromSql($"SELECT * FROM Users WHERE UPPER(FirstName) LIKE '%{userName}%' or UPPER(LastName) LIKE '%{userName}%'").ToListAsync();
        }
    }
}
