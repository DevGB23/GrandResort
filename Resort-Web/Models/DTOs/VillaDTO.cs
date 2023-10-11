using System.ComponentModel.DataAnnotations;

namespace Resort_Web.Models.DTOs
{
    public class VillaDTO
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public int Occupancy { get; set; }
        public int SqFt { get; set; }
        public string Details { get; set; } = string.Empty;
        [Required]
        public string Rate { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Amenity { get; set; } = string.Empty;        
    }
}
