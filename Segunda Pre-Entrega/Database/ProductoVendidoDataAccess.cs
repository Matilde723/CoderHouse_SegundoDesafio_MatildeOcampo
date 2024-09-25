using Segunda_Pre_Entrega.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Segunda_Pre_Entrega.Database
{
    public static class ProductoVendidoDataAccess
    {
        
        public static ProductoVendido ObtenerProductoVendido(int id, string connectionString)
        {
            ProductoVendido productoVendido = null;
            string query = "SELECT Id, IdProducto, Stock, IdVenta FROM ProductoVendido WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.HasRows && dr.Read())
                            {
                                productoVendido = new ProductoVendido
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    IdVenta = Convert.ToInt32(dr["IdVenta"])
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while fetching ProductoVendido", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching ProductoVendido", ex);
            }

            return productoVendido;
        }

        
        public static List<ProductoVendido> ListarProductosVendidos(string connectionString)
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            string query = "SELECT Id, IdProducto, Stock, IdVenta FROM ProductoVendido";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.HasRows && dr.Read())
                            {
                                var productoVendido = new ProductoVendido
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    IdVenta = Convert.ToInt32(dr["IdVenta"])
                                };
                                lista.Add(productoVendido);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while listing ProductosVendidos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while listing ProductosVendidos", ex);
            }

            return lista;
        }

        
        public static void CrearProductoVendido(ProductoVendido productoVendido, string connectionString)
        {
            string query = "INSERT INTO ProductoVendido (IdProducto, Stock, IdVenta) " +
                           "VALUES (@IdProducto, @Stock, @IdVenta)";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int) { Value = productoVendido.IdProducto });
                        comando.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = productoVendido.Stock });
                        comando.Parameters.Add(new SqlParameter("@IdVenta", SqlDbType.Int) { Value = productoVendido.IdVenta });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while creating ProductoVendido", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating ProductoVendido", ex);
            }
        }

   
        public static void ModificarProductoVendido(ProductoVendido productoVendido, string connectionString)
        {
            string query = @"UPDATE ProductoVendido
                             SET IdProducto = @IdProducto, 
                                 Stock = @Stock,
                                 IdVenta = @IdVenta
                             WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = productoVendido.Id });
                        comando.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int) { Value = productoVendido.IdProducto });
                        comando.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = productoVendido.Stock });
                        comando.Parameters.Add(new SqlParameter("@IdVenta", SqlDbType.Int) { Value = productoVendido.IdVenta });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while updating ProductoVendido", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating ProductoVendido", ex);
            }
        }

        
        public static void EliminarProductoVendido(int id, string connectionString)
        {
            string query = "DELETE FROM ProductoVendido WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while deleting ProductoVendido", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting ProductoVendido", ex);
            }
        }
    }
}
