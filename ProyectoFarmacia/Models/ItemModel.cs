using System.ComponentModel.DataAnnotations;

namespace ProyectoFarmacia.Models
{
    public class ItemModel
    {
        [Display(Name = "Code")]
        public int IdProduct { get; set; }
        [Display(Name = "Product ")]
        public string? NameProduct { get; set; }
        [Display(Name = "Code Category")]
        public int IdCategory { get; set; }
        [Display(Name = "Price")]
        public decimal PriceUnit { get; set; }
        [Display(Name = "Unit in Stock")]
        public int Units { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get { return PriceUnit * Units; } }

        [Display(Name = "Image ")]
        public string? route { get; set; }

    }
}
