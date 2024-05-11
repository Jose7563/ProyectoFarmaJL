using System.ComponentModel.DataAnnotations;

namespace ProyectoFarmacia.Models
{
    public class TicketModel
    {
        [Display(Name = "Codigo")] public int IdTicket { get; set; }
        [Display(Name = "Fecha Compra")] public DateTime DateCreation { get; set; }
        public decimal Total { get; set; }
        public int IdUser { get; set; }
    }
}
