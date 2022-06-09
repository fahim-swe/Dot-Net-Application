using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce_App_Practices_1.Data.Base
{
    public interface IEntityBaseRepository <T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> getAllAsync();

        Task<T> getByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(Guid id, T entity);

        Task DeleteAsync(Guid id);

        
    }
}
