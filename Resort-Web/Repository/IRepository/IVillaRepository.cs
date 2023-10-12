using Resort_Web.Models;

namespace Resort_Web.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);        
    }
}
