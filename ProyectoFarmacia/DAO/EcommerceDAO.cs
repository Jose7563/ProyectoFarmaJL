using System.Data.SqlClient;
using System.Data;
using System.Net.Sockets;
using ProyectoFarmacia.Models;
using System.Xml.Linq;

namespace ProyectoFarmacia.DAO
{
    public class EcommerceDAO
    {

        ConectionBD con = new ConectionBD();


        public IEnumerable<ProductModel> listProducts(string name)
        {
            List<ProductModel> list = new List<ProductModel>();
            if (String.IsNullOrEmpty(name)) name = string.Empty;
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_filter_product @name", cn);
                cmd.Parameters.AddWithValue("@name", name);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new ProductModel()
                    {
                        IdProduct = Convert.ToInt32(dr["IdProduct"]),
                        NameProduct = dr["NameProduct"].ToString(),
                        IdCategory = Convert.ToInt32(dr["IdCategory"]),
                        PriceUnit = Convert.ToDecimal(dr["PriceUnit"]),
                        UnitsStock = Convert.ToInt32(dr["UnitsStock"]),
                        ImageProduct = dr["ImageProduct"].ToString()

                    });
                }
            }
            return list;
        }

        public ProductModel findProductId(int id)
        {
            ProductModel list = new ProductModel();
            
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_findProductID @id", cn);
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list= new ProductModel()
                    {
                        IdProduct = Convert.ToInt32(dr["IdProduct"]),
                        NameProduct = dr["NameProduct"].ToString(),
                        IdCategory = Convert.ToInt32(dr["IdCategory"]),
                        PriceUnit = Convert.ToDecimal(dr["PriceUnit"]),
                        UnitsStock = Convert.ToInt32(dr["UnitsStock"]),
                        ImageProduct = dr["ImageProduct"].ToString()

                    };
                }
            }
            return list;
        }

        public void UpdateProductoAfterOrder(ItemModel p)
        {
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                ProductModel pro = findProductId(p.IdProduct);
                int newUnit = pro.UnitsStock - p.Units;
                try
                {

                    SqlCommand cmd = new SqlCommand("usp_update_product", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", p.IdProduct);
                    cmd.Parameters.AddWithValue("@unid", newUnit);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                }
            }
        }


        public void InsertDetail(ItemModel it, int idTicket) {

            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insert_tickect_detail", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@amount", it.Units);
                    cmd.Parameters.AddWithValue("@ticket_id", idTicket);
                    cmd.Parameters.AddWithValue("@price", it.PriceUnit);
                    cmd.Parameters.AddWithValue("@product_id", it.IdProduct);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    
                }
            }
            
        }


        public void InsertTicket(TicketModel ticket)
        {
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insert_ticket", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", ticket.DateCreation);
                    cmd.Parameters.AddWithValue("@total", ticket.Total);
                    cmd.Parameters.AddWithValue("@id_user", ticket.IdUser);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
            }

        }


    }
}






