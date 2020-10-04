using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebRestApi.Service.Repository
{
    public abstract class RepositoryBase<TModel> : IRepository<TModel>
    {
        protected AbstractDbContext Context { get; }

        protected RepositoryBase(AbstractDbContext context)
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
