using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CredoLoan.DAL {
    internal abstract class BaseRepository {

        private readonly DbContext _context;

        public BaseRepository(DbContext context) {
            _context = context;
        }

        protected virtual async Task<TEntity> AddEntityAsync<TEntity>(TEntity entity) where TEntity : class {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        protected virtual async Task<bool> UpdateEntityAsync<TEntity>(TEntity entity, params string[] updatableProperties) where TEntity : class {
            _context.Update(entity);

            if(updatableProperties.Length > 0) {
                var entityEntry = _context.Entry(entity);
                foreach (var prop in entityEntry.Properties) {
                    string propName = prop.Metadata.Name;
                    entityEntry.Property(propName).IsModified = updatableProperties.Contains(propName);
                }
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
