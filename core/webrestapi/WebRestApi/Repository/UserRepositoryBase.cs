using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Models;

#pragma warning disable 1591
namespace WebRestApi.Repository
{
    public abstract class UserRepositoryBase : RepositoryBase<User, WebRestApiContext>
    {
        protected UserRepositoryBase(WebRestApiContext context) : base(context) { }

        public abstract Task<IEnumerable<User>> GetByNameAsync(string userName);

        public override async Task<User> AddAsync(User user)
        {
            Context.Users.Add(user);
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
