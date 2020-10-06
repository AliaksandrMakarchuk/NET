using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Repository;

namespace WebRestApi.DataAccess.Repository
{
    public class UserRepository : UserRepositoryBase
    {
        public UserRepository(WebRestApiContext context) : base(context) { }

        public override async Task<IEnumerable<User>> GetByNameAsync(string userName)
        {
            // return await Context.Users.FromSql($"SELECT * FROM Users WHERE UPPER(FirstName) LIKE '%{userName}%' or UPPER(LastName) LIKE '%{userName}%'").ToListAsync();
            return await Task.FromResult<IEnumerable<User>>(new List<User>());
        }

        public override async Task<User> AddAsync(User user)
        {
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            return user;
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            await Task.Delay(1);
            return Context.Users.Local;
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await Context.Users.FindAsync(id);
        }

        public override async Task<User> UpdateAsync(User user)
        {
            if(user == null)
            {
                return null;
            }

            var existingUser = await Context.Users.FindAsync(user.Id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;

            Context.Update(existingUser);
            await Context.SaveChangesAsync();

            return existingUser;
        }

        public override async Task<User> DeleteAsync(User user)
        {
            if(user == null)
            {
                return null;
            }

            var existingUser = await Context.Users.FindAsync(user.Id);

            if (existingUser == null)
            {
                return null;
            }

            Context.Users.Remove(existingUser);
            return existingUser;
        }
    }
}
