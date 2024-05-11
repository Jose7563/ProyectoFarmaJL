using System.ComponentModel.DataAnnotations;

namespace ProyectoFarmacia.DTO
{
    public class DetailTicketDTO
    {
        [Display(Name = "Nombre Producto")] public string? NameProduct { get; set; }
        [Display(Name = "Precio")] public decimal Price { get; set; }
        [Display(Name = "Cantidad")] public int Amount { get; set; }
        [Display(Name = "Total ")] public decimal TotalItem { get { return Price * Amount; } set { } }
    }
}
