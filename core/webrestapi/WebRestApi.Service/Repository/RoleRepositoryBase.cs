using System.Threading.Tasks;
using WebRestApi.Service.Models;

namespace WebRestApi.Service.Repository
{
    public abstract class RoleRepositoryBase : RepositoryBase<Role>
    {
        protected RoleRepositoryBase(AbstractDbContext context) : base(context) { }

        public abstract Task<Role> GetByUserRole(UserRole role);
    }
}
