namespace ProyectoFarmacia.Models
{
    public class TicketDetailModel
    {
        public int IdTicketDetail { get; set; }
        public int Amount { get; set; }
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
    }
}
