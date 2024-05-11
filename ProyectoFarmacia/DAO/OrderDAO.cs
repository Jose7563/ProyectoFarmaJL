using ProyectoFarmacia.DTO;
using ProyectoFarmacia.Models;
using System.Data.SqlClient;

namespace ProyectoFarmacia.DAO
{
    public class OrderDAO
    {
        ConectionBD con = new ConectionBD();

        public int MaxIdTicket()
        {
            int id = 0;

            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_max_id_ticket", cn);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    id = dr.GetInt32(0);
                }
            }
            return id;
        }

        public List<DetailTicketDTO> details(int id)
        {
            List<DetailTicketDTO> list = new List<DetailTicketDTO>();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_list_detail  @id", cn);
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new DetailTicketDTO()
                    {
                        NameProduct = dr.GetString(0),
                        Price = dr.GetDecimal(1),
                        Amount = dr.GetInt32(2),
                        TotalItem = dr.GetDecimal(3)
                    });
                }
            }
            return list;
        }



        public List<TicketModel> orders(int id)
        {
            List<TicketModel> list = new List<TicketModel>();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_ticket_user @id_user", cn);
                cmd.Parameters.AddWithValue("@id_user", id);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new TicketModel()
                    {
                        IdTicket = dr.GetInt32(0),
                        DateCreation = dr.GetDateTime(1),
                        Total = dr.GetDecimal(2)
                    });
                }
            }
            return list;
        }
    }
}
