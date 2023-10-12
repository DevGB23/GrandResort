using Resort_Web.Data;
using Resort_Web.Models;
using Resort_Web.Repository.IRepository;

namespace Resort_Web.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db = null!;

        public VillaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _db.VillaNumbers.Update(entity);
            await _db.SaveChangesAsync();        
            return entity;
        }
    }
}