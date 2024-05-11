using Microsoft.AspNetCore.Hosting;
using ProyectoFarmacia.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFarmacia.DAO
{
    public class ProductDAO
    {
        ConectionBD con = new ConectionBD();


        public string insertProduct(ProductModel model, string uniqueFileName)
        {

            string messageGeneric = "";
            

            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {

                    try
                    {
                        SqlCommand cmd = new SqlCommand("usp_insert_product", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NameProduct", model.NameProduct);
                        cmd.Parameters.AddWithValue("@IdCategory", model.IdCategory);
                        cmd.Parameters.AddWithValue("@PriceUnit", model.PriceUnit);
                        cmd.Parameters.AddWithValue("@UnitsStock", model.UnitsStock);
                        cmd.Parameters.AddWithValue("@ImageProduct", uniqueFileName);

                        cn.Open();
                        int c = cmd.ExecuteNonQuery();
                        messageGeneric = $"Se inserto {c} producto ";
                    }
                    catch (Exception ex)
                    {
                        messageGeneric = ex.Message;
                    }
                

            }            
            return  messageGeneric;
        }

        public bool editPorductoNoImg(ProductModel pro)
        {

            bool rpta = false;
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_update_product_not_img", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", pro.IdProduct);
                    cmd.Parameters.AddWithValue("@Nombre", pro.NameProduct);
                    cmd.Parameters.AddWithValue("@Categoria", pro.IdCategory);
                    cmd.Parameters.AddWithValue("@Price", pro.PriceUnit);
                    cmd.Parameters.AddWithValue("@Unit", pro.UnitsStock);
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

        public bool editPorductoImg(ProductModel pro)
        {

            bool rpta = false;
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_update_product_with_img", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", pro.IdProduct);
                    cmd.Parameters.AddWithValue("@Nombre", pro.NameProduct);
                    cmd.Parameters.AddWithValue("@Categoria", pro.IdCategory);
                    cmd.Parameters.AddWithValue("@Price", pro.PriceUnit);
                    cmd.Parameters.AddWithValue("@Unit", pro.UnitsStock);
                    cmd.Parameters.AddWithValue("@Img", pro.ImageProduct);
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



        public ProductModel SearchProducts(int id)
        {
            ProductModel listProducts = new ProductModel();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec sp_search_product @id", cn);
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    listProducts.IdProduct = Convert.ToInt32(dr["IdProduct"]);
                    listProducts.NameProduct = dr["NameProduct"].ToString();
                    listProducts.IdCategory = Convert.ToInt32(dr["IdCategory"]);
                    listProducts.PriceUnit = Convert.ToDecimal(dr["PriceUnit"]);
                    listProducts.UnitsStock = Convert.ToInt32(dr["UnitsStock"]);
                    listProducts.ImageProduct = dr["ImageProduct"].ToString();
                    
                };
            }
            return listProducts;
        }


        public IEnumerable<ProductModel> listProducts()
        {
            List<ProductModel> listProducts = new List<ProductModel>();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_products ", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listProducts.Add(new ProductModel()
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
            return listProducts;
        }

    }
}
