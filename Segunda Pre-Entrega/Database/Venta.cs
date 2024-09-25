using Segunda_Pre_Entrega.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Segunda_Pre_Entrega.Database
{
    public static class VentaDataAccess
    {
        // Método para obtener una venta por Id
        public static Venta ObtenerVenta(int id, string connectionString)
        {
            Venta venta = null;
            string query = "SELECT Id, Comentarios, IdUsuario FROM Venta WHERE Id = @Id";

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
                                venta = new Venta
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Comentarios = dr["Comentarios"].ToString(),
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"])
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while fetching Venta", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Venta", ex);
            }

            return venta;
        }

        // Método para poner todas las listas 
        public static List<Venta> ListarVentas(string connectionString)
        {
            List<Venta> lista = new List<Venta>();
            string query = "SELECT Id, Comentarios, IdUsuario FROM Venta";

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
                                var venta = new Venta
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Comentarios = dr["Comentarios"].ToString(),
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"])
                                };
                                lista.Add(venta);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while listing Ventas", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while listing Ventas", ex);
            }

            return lista;
        }

        // Método para crear una vista nueva
        public static void CrearVenta(Venta venta, string connectionString)
        {
            string query = "INSERT INTO Venta (Comentarios, IdUsuario) " +
                           "VALUES (@Comentarios, @IdUsuario)";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                        comando.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = venta.IdUsuario });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while creating Venta", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating Venta", ex);
            }
        }

        // Método para hacer el Update de una venta
        public static void ModificarVenta(Venta venta, string connectionString)
        {
            string query = @"UPDATE Venta
                             SET Comentarios = @Comentarios, 
                                 IdUsuario = @IdUsuario
                             WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = venta.Id });
                        comando.Parameters.Add(new SqlParameter("@Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                        comando.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = venta.IdUsuario });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while updating Venta", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating Venta", ex);
            }
        }

        // Método para eliminar una venta
        public static void EliminarVenta(int id, string connectionString)
        {
            string query = "DELETE FROM Venta WHERE Id = @Id";

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
                throw new Exception("Error while deleting Venta", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting Venta", ex);
            }
        }
    }
}
