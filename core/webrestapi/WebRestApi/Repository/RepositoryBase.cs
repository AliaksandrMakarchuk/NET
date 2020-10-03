using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace WebRestApi.Repository
{
    public abstract class RepositoryBase<TModel, TContext> : IRepository<TModel>
        where TContext : DbContext
    {
        protected TContext Context { get; }

        protected RepositoryBase(TContext context)
        {
            Context = context;
        }

        public abstract Task<IEnumerable<TModel>> GetAllAsync();

        public abstract Task<TModel> GetByIdAsync(int id);

        public abstract Task<TModel> AddAsync(TModel model);

        public abstract Task<TModel> UpdateAsync(TModel model);

        public abstract Task<TModel> DeleteAsync(TModel model);
    }
}
