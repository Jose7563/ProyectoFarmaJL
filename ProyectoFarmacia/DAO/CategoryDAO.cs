using Microsoft.AspNetCore.Mvc;
using ProyectoFarmacia.Models;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace ProyectoFarmacia.DAO
{
    public class CategoryDAO
    {

        ConectionBD con = new ConectionBD();


        public string insertCategory(CategoryModel ca)
        {
            string messageGeneric = "";
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insert_category", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nameCategory", ca.NameCategory);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    messageGeneric = $"Se inserto {c} categoria ";
                }
                catch (Exception ex)
                {
                    messageGeneric = ex.Message;


                }
            }
           return  messageGeneric;
        }
        public bool editCategory(CategoryModel ca)
        {
            
            bool rpta = false; 
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_update_category", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", ca.IdCategory);
                    cmd.Parameters.AddWithValue("@nameCategory", ca.NameCategory);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    rpta = true;
                }
                catch (Exception ex)
                {
                    rpta = false;
                }
            }
            return rpta; 
        }
        public CategoryModel findCategoryId(int id)
        {
            CategoryModel cat = new CategoryModel();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_find_category @id", cn);
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cat.IdCategory = Convert.ToInt32(dr["IdCategory"]);
                    cat.NameCategory = dr["NameCategory"].ToString();

                }
            }
            return cat;

        }
        public IEnumerable<CategoryModel> listCategories()
        {
            
            List<CategoryModel> listCategories = new List<CategoryModel>();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_list_categories", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listCategories.Add(new CategoryModel()
                    {
                        IdCategory = Convert.ToInt32(dr["IdCategory"]),
                        NameCategory = dr["NameCategory"].ToString()
                    });
                }
            }
            return listCategories;
        }

    }
}
