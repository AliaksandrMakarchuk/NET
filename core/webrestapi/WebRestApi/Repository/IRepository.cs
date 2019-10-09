using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebRestApi.Repository
{
    public interface IRepository<TModel>
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> AddAsync(TModel model);

        Task<TModel> GetByIdAsync(int id);

        Task<TModel> UpdateAsync(TModel model);

        Task<TModel> DeleteAsync(TModel model);
    }
}
