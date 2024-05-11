using System.ComponentModel.DataAnnotations;
using System.Web;


namespace ProyectoFarmacia.Models
{
    public class ProductModel
    {
        
        [Display(Name = "Code")]  
        public int IdProduct { get; set; }


        [Required]
        [Display(Name = "Product ")]    
        public string? NameProduct { get; set; }

        [Required]
        [Display(Name = "Code Cateogry")]
        public int IdCategory { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal PriceUnit { get; set; }

        [Required]
        [Display(Name = "Unit in Stock")]
        public int UnitsStock { get; set; }

        [Required]
        [Display(Name = "Image ")]

        public string? ImageProduct { get; set; }

        public IFormFile ImageFile { get; set; }


    }
}
