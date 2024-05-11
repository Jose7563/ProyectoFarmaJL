using System.ComponentModel.DataAnnotations;

namespace ProyectoFarmacia.Models
{
    public class CategoryModel
    {
        public int IdCategory { get; set; }
        [Required]
        public string? NameCategory { get; set; }
    }
}
