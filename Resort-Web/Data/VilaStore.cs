using Resort_Web.Models.DTOs;

namespace Resort_Web.Data
{
    public static class VillaStore
    {
       public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO { Id = 1, Name = "Pool View", SqFt = 100, Occupancy = 4 },
                new VillaDTO { Id = 2, Name = "Beach view", SqFt = 300, Occupancy = 3 }
            };

    }
}
