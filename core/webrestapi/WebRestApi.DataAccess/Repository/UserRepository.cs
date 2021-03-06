using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebRestApi.Service;
using WebRestApi.Service.Models;
using WebRestApi.Service.Repository;

namespace WebRestApi.DataAccess.Repository
{
    public class UserRepository : UserRepositoryBase
    {
        public UserRepository(AbstractDbContext context) : base(context) { }

        public override async Task<IEnumerable<User>> GetByNameAsync(string userName)
        {
            string query = $"SELECT * FROM Users WHERE UPPER(FirstName) LIKE '%{userName}%' or UPPER(LastName) LIKE '%{userName}%'";
            return await Context.Users.FromSqlRaw(query).Include(u => u.Role).ToListAsync();
        }

        public override async Task<User> AddAsync(User user)
        {
            var roles = await Context.Roles.ToListAsync();
            user.RoleId = roles.Single(x => x.Name == "user").Id;
            
            var newUser = await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            return await GetByIdAsync(newUser.Entity.Id);
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Context.Users.Include(u => u.Role).ToListAsync();
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await Context.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<User> UpdateAsync(User user)
        {
            if (user == null)
            {
                return null;
            }

            var existingUser = await Context.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Id == user.Id);

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
            if (user == null)
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