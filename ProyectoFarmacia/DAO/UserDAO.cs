using ProyectoFarmacia.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFarmacia.DAO
{
    public class UserDAO
    {
        ConectionBD con = new ConectionBD();

        public UserModel UserSession(string name)
        {

            UserModel u = new UserModel();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("usp_user", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user", name);
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u.Id = dr.GetInt32(0);
                    u.Names = dr.GetString(1);
                    u.LastNameUser = dr.GetString(2);
                    u.Phone = dr.GetString(3);
                }
            }
            return u;
        }
    }
}
