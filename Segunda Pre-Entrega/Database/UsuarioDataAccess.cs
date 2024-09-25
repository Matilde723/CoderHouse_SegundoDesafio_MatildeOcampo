using Segunda_Pre_Entrega.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Segunda_Pre_Entrega.Database
{
    public static class UsuarioDataAccess
    {
        // 
        public static Usuario ObtenerUsuario(int id, string connectionString)
        {
            Usuario usuario = null;
            string query = "SELECT Id, Nombre, Apellido, NombreUsuario, Contraseña, Mail FROM Usuario WHERE Id = @Id";

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
                                usuario = new Usuario
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    NombreUsuario = dr["NombreUsuario"].ToString(),
                                    Contraseña = dr["Contraseña"].ToString(),
                                    Mail = dr["Mail"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while fetching Usuario", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Usuario", ex);
            }

            return usuario;
        }

        // Método para listar usuarios
        public static List<Usuario> ListarUsuarios(string connectionString)
        {
            List<Usuario> lista = new List<Usuario>();
            string query = "SELECT Id, Nombre, Apellido, NombreUsuario, Contraseña, Mail FROM Usuario";

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
                                var usuario = new Usuario
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    NombreUsuario = dr["NombreUsuario"].ToString(),
                                    Contraseña = dr["Contraseña"].ToString(),
                                    Mail = dr["Mail"].ToString()
                                };
                                lista.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while listing Usuarios", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while listing Usuarios", ex);
            }

            return lista;
        }

        // Método para Crear Usuario
        public static void CrearUsuario(Usuario usuario, string connectionString)
        {
            string query = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                           "VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail)";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                        comando.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                        comando.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                        comando.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña });
                        comando.Parameters.Add(new SqlParameter("@Mail", SqlDbType.VarChar) { Value = usuario.Mail });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while creating Usuario", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating Usuario", ex);
            }
        }

        // Method to update an existing Usuario
        public static void ModificarUsuario(Usuario usuario, string connectionString)
        {
            string query = @"UPDATE Usuario
                             SET Nombre = @Nombre, 
                                 Apellido = @Apellido,
                                 NombreUsuario = @NombreUsuario,
                                 Contraseña = @Contraseña,
                                 Mail = @Mail
                             WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = usuario.Id });
                        comando.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                        comando.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                        comando.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                        comando.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña });
                        comando.Parameters.Add(new SqlParameter("@Mail", SqlDbType.VarChar) { Value = usuario.Mail });

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while updating Usuario", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating Usuario", ex);
            }
        }

        // Método para borrar Usuario
        public static void EliminarUsuario(int id, string connectionString)
        {
            string query = "DELETE FROM Usuario WHERE Id = @Id";

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
                throw new Exception("Error while deleting Usuario", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting Usuario", ex);
            }
        }
    }
}
