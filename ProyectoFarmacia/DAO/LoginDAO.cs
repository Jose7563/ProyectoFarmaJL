using Microsoft.AspNetCore.Mvc;
using ProyectoFarmacia.Models;
using System.Data.SqlClient;
using System.Data;
namespace ProyectoFarmacia.DAO
{
    public class LoginDAO
    {
        ConectionBD con = new ConectionBD();

        
        public UserModel LoginSingIn(UserModel log)
        {
            UserModel us = new UserModel();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("usp_seg_user", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user", log.UserSession);
                cmd.Parameters.AddWithValue("@password", log.PasswordUser);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    us.UserSession = (dr.GetString(0));
                    us.PasswordUser = (dr.GetString(1));
                    us.TypeUser = (dr.GetInt32(2));

                }
            }
            return us;

        }


    }
}
