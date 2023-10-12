using Resort_Web.Data;
using Resort_Web.Models;
using Resort_Web.Repository.IRepository;

namespace Resort_Web.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db = null!;

        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();        
            return entity;
        }

         
    }
}
