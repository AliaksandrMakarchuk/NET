using System.Threading.Tasks;
using WebRestApi.Service.Models;

namespace WebRestApi.Service.Repository
{
    public abstract class UserRepositoryBase : RepositoryBase<User>
    {
        protected UserRepositoryBase(AbstractDbContext context) : base(context) { }

        public abstract Task<User> GetByLoginPasswordAsync(string login, string password);
    }
}
