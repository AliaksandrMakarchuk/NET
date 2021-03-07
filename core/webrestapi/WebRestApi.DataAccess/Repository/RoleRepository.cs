using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebRestApi.Service;
using WebRestApi.Service.Models;
using WebRestApi.Service.Repository;

namespace WebRestApi.DataAccess.Repository
{
    public class RoleRepository : RoleRepositoryBase
    {
        public RoleRepository(AbstractDbContext context) : base(context) { }

        public override Task<Role> AddAsync(Role model)
        {
            throw new System.NotImplementedException();
        }

        public override Task<Role> DeleteAsync(Role model)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await Context.Roles.ToListAsync();
        }

        public override async Task<Role> GetByIdAsync(int id)
        {
            return await Context.Roles.SingleOrDefaultAsync(r => r.Id == id);
        }

        public override async Task<Role> GetByUserRole(UserRole role)
        {
            return await Context.Roles.SingleOrDefaultAsync(r => r.Name == role.RoleName);
        }

        public override Task<Role> UpdateAsync(Role model)
        {
            throw new System.NotImplementedException();
        }
    }
}