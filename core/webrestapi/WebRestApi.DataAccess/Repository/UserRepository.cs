using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Service;
using WebRestApi.Service.Models;
using WebRestApi.Service.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebRestApi.DataAccess.Repository
{
    public class UserRepository : UserRepositoryBase
    {
        public UserRepository(AbstractDbContext context) : base(context) { }

        public override async Task<IEnumerable<User>> GetByNameAsync(string userName)
        {
            // return await Context.Users.FromSql($"SELECT * FROM Users WHERE UPPER(FirstName) LIKE '%{userName}%' or UPPER(LastName) LIKE '%{userName}%'").ToListAsync();
            return await Task.FromResult<IEnumerable<User>>(new List<User>());
        }

        public override async Task<User> AddAsync(User user)
        {
            var newUser = await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            return newUser.Entity;
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Context.Users.ToListAsync();
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

            var newUser = Context.Update(existingUser);
            await Context.SaveChangesAsync();

            return newUser.Entity;
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
