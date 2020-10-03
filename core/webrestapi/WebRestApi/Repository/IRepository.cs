using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable 1591
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
