using System.ComponentModel.DataAnnotations;

namespace Resort_Web.Models.DTOs
{
    public class VillaNumberUpdateDTO
    {
        [Key]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; } = string.Empty;       
    }
}
