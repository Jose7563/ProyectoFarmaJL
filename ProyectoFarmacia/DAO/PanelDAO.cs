using System.Data.SqlClient;
using System.Data;
using ProyectoFarmacia.DTO;
using ProyectoFarmacia.Models;

namespace ProyectoFarmacia.DAO
{
    public class PanelDAO
    {
        ConectionBD con = new ConectionBD();

        public List<ProductModel> ListStock(int stock)
        {
            List<ProductModel> ListStockMin = new List<ProductModel>();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("usp_update_stock", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stock", stock);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListStockMin.Add(new ProductModel()
                    {
                        IdProduct = Convert.ToInt32(dr["IdProduct"]),
                        NameProduct = dr["NameProduct"].ToString(),
                        UnitsStock = Convert.ToInt32(dr["UnitsStock"])


                    });

                }
            }
            return ListStockMin;
        }
        public List<Generic> Donuts(int year)
        {
            List<Generic> list = new List<Generic>();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec sp_list_products_sale  @year", cn);
                cmd.Parameters.AddWithValue("@year", year);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Generic()
                    {
                        Name = dr.GetString(0),
                        valuedonut = dr.GetInt32(1)

                    });
                }
            }
            return list;
        }
        public List<Generic> ListSalesMonthInYear(int id)
        {
            List<Generic> list = new List<Generic>();
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("exec usp_sale_month_in_year  @year", cn);
                cmd.Parameters.AddWithValue("@year", id);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Generic()
                    {
                        Value = dr.GetDecimal(0),
                        Name = dr.GetString(1)

                    });
                }
            }
            return list;
        }
        public decimal SaleforMonth(int month)
        {
            decimal SaleM = 0;
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("usp_sale_for_month", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@month", month);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                
                if (dr.Read())
                {
					try
					{
						 SaleM = dr.GetDecimal(0);
						// Realiza las operaciones necesarias con 'saleM'
					}
					catch (System.Data.SqlTypes.SqlNullValueException)
					{
						// Maneja la excepción cuando el valor es nulo
						
                        SaleM = 0;
						
					}



				}
            }
            return SaleM;
        }
        public decimal SalesForYear(int year)
        {
            decimal Sale = 0;
            using (SqlConnection cn = new SqlConnection(con.getConnectionSQL()))
            {
                SqlCommand cmd = new SqlCommand("usp_sale_for_year", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@year", year);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Sale = dr.GetDecimal(0);

                }
            }
            return Sale; 
        }
    }
}
