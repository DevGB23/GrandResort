using System.ComponentModel.DataAnnotations;

namespace Resort_Web.Models.DTOs
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int SqFt { get; set; }
        public string Details { get; set; } = string.Empty;
        [Required]
        public double Rate { get; set; }
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        public string Amenity { get; set; } = string.Empty;        
    }
}
