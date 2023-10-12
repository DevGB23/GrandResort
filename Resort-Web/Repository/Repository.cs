using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Resort_Web.Data;
using Resort_Web.Repository.IRepository;

namespace Resort_Web.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db; 
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbset;

            if ( filter is not null)
            {
                query = query.Where(filter);
            };

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false)
        {
            IQueryable<T> query = dbset;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            
            if ( filter is not null)
            {
                query = query.Where(filter);
            };

            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbset.Remove(entity);
            await SaveAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entityCollection)
        {
            dbset.RemoveRange(entityCollection);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
