using Segunda_Pre_Entrega.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace Segunda_Pre_Entrega.Database
{
    public static class ProductoDataAccess
    {
        public static List<Producto> ObtenerProducto(int id, string connectionString)  //se agregó el parámetro de ID
        {
            List<Producto> lista = new List<Producto>();
            string query = "SELECT Id, Descripcion, Costo, PrecioVenta, Stock, IdUsuario FROM Producto WHERE Id = @Id";

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
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    var producto = new Producto();
                                    producto.Id = Convert.ToInt32(dr["Id"]);
                                    producto.Descripcion = dr["Descripcion"].ToString();
                                    producto.Costo = (float)Convert.ToDecimal(dr["Costo"]);
                                    producto.PrecioVenta = (float)Convert.ToDecimal(dr["PrecioVenta"]);
                                    producto.Stock = Convert.ToInt32(dr["Stock"]);
                                    producto.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                                    lista.Add(producto);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while fetching product", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching product", ex);
            }

            return lista;
        }

        public static List<Producto> ListarProductos(string connectionString)
        {
            List<Producto> lista = new List<Producto>();
            string query = "SELECT Id, Descripcion, Costo, PrecioVenta, Stock, IdUsuario FROM Producto";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    var producto = new Producto
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        Descripcion = dr["Descripcion"].ToString(),
                                        Costo = (float)Convert.ToDecimal(dr["Costo"]),
                                        PrecioVenta = (float)Convert.ToDecimal(dr["PrecioVenta"]),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        IdUsuario = Convert.ToInt32(dr["IdUsuario"])
                                    };
                                    lista.Add(producto);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while listing products", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while listing products", ex);
            }

            return lista;
        }

        public static void CrearProducto(Producto producto, string connectionString)
        {
            string query = "INSERT INTO Producto (Descripcion, Costo, PrecioVenta, Stock, IdUsuario) " +
                           "VALUES (@Descripcion, @Costo, @PrecioVenta, @Stock, @IdUsuario)";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = producto.Descripcion });
                        comando.Parameters.Add(new SqlParameter("@Costo", SqlDbType.Money) { Value = producto.Costo });
                        comando.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Money) { Value = producto.PrecioVenta });
                        comando.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = producto.Stock });
                        comando.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = producto.IdUsuario });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while creating product", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating product", ex);
            }
        }

        public static void ModificarProducto(Producto producto, string connectionString)
        {
            string query = @"UPDATE Producto
                             SET Descripcion = @Descripcion, 
                                 Costo = @Costo,
                                 PrecioVenta = @PrecioVenta,
                                 Stock = @Stock,
                                 IdUsuario = @IdUsuario
                             WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = producto.Id });
                        comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = producto.Descripcion });
                        comando.Parameters.Add(new SqlParameter("@Costo", SqlDbType.Money) { Value = producto.Costo });
                        comando.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Money) { Value = producto.PrecioVenta });
                        comando.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = producto.Stock });
                        comando.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = producto.IdUsuario });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while updating product", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating product", ex);
            }
        }

        public static void EliminarProducto(int productoId, string connectionString)
        {
            string query = "DELETE FROM Producto WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = productoId });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while deleting product", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting product", ex);
            }
        }
    }
}
