namespace ProyectoFarmacia.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string?  UserSession { get; set; }
        public string? Names { get; set; }
        public string? LastNameUser { get; set; }
 
        public string? Phone { get; set; }
        public string? PasswordUser { get; set; }

        public int Attempts { get; set; }

        public decimal NivelSeg { get; set; }

        public DateTime DateReg { get; set; }
        public int TypeUser { get; set; }
    }
}
