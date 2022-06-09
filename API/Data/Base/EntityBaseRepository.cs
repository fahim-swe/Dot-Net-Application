using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce_App_Practices_1.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DataContext _context;

        public EntityBaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var resutl = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(resutl);
            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> getAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();

            return result;
        }

        public async Task<T> getByIdAsync(Guid id)
        {
            var resutl = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            return resutl;
        }


      
       
        public async Task UpdateAsync(Guid id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
