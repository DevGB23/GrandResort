using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resort_Web.Models;

namespace Resort_Web.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity); 
    }
}