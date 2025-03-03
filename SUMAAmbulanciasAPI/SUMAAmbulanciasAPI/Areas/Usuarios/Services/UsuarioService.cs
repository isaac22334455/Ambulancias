using MySql.Data.MySqlClient;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.GeneralModels;
using SUMAAmbulanciasAPI.Areas.Usuarios.Models;

namespace SUMAAmbulanciasAPI.Areas.Usuarios.Services
{
    public class UsuarioService
    {
        private readonly IConfiguration _configuration;

        public UsuarioService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<UsuarioModel> GetUsuarios()
        {
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Usar la cadena de conexión con ADO.NET
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuarios ORDER BY dtFechaRegistro DESC", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Crear una instancia del modelo Usuario
                        UsuarioModel usuario = new UsuarioModel
                        {
                            Id_Usuario = Convert.ToInt32(reader["Id_Usuario"]),
                            Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                            IdRol_Usuario = Convert.ToInt32(reader["IdRol_Usuario"]),
                            Pass_Usuario = reader["Pass_Usuario"].ToString(),
                            dtFechaRegistro = DateTime.Parse(reader["dtFechaRegistro"].ToString()).ToString("dd/MM/yyyy"),
                        };

                        // Agregar el usuario a la lista
                        listaUsuarios.Add(usuario);
                    }
                }
                return listaUsuarios;
            }
        }

        public List<UsuarioModel> GetUsuarioById(int id)
        {
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Id_Usuario,Nombre_Usuario,IdRol_Usuario,Pass_Usuario,dtFechaRegistro FROM usuarios  WHERE Id_Usuario = @Id_Usuario", conn);
                    cmd.Parameters.AddWithValue("@Id_Usuario", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear una instancia del modelo Usuario
                            UsuarioModel usuario = new UsuarioModel
                            {
                                Id_Usuario = Convert.ToInt32(reader["Id_Usuario"]),
                                Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                IdRol_Usuario = Convert.ToInt32(reader["IdRol_Usuario"]),
                                Pass_Usuario = reader["Pass_Usuario"].ToString(),
                                dtFechaRegistro = DateTime.Parse(reader["dtFechaRegistro"].ToString()).ToString("dd/MM/yyyy"),
                            };

                            // Agregar el usuario a la lista
                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener usuarios: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaUsuarios;
        }

        public List<UsuarioModel> GetUsuarioByName(string nombre)
        {
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Usar la cadena de conexión con ADO.NET
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuarios WHERE Nombre_Usuario = @Nombre", conn);
                cmd.Parameters.AddWithValue("@Nombre", nombre);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Crear una instancia del modelo Usuario
                        UsuarioModel usuario = new UsuarioModel
                        {
                            Id_Usuario = Convert.ToInt32(reader["Id_Usuario"]),
                            Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                            IdRol_Usuario = Convert.ToInt32(reader["IdRol_Usuario"]),
                            Pass_Usuario = reader["Pass_Usuario"].ToString()
                        };

                        // Agregar el usuario a la lista
                        listaUsuarios.Add(usuario);
                    }
                }
                return listaUsuarios;
            }
        }

        public void DeleteUserById(int id)
        {

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Usar la cadena de conexión con ADO.NET
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Preparar la consulta SQL para insertar
                string query = "DELETE FROM usuarios WHERE Id_Usuario = @Id";

             
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Asignar los parámetros
                    cmd.Parameters.AddWithValue("@Id", id);
  
                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void InsertUsuario(UsuarioModel nuevoUsuario)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Usar la cadena de conexión con ADO.NET
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Preparar la consulta SQL para insertar
                string query = "INSERT INTO usuarios (Nombre_Usuario, IdRol_Usuario, Pass_Usuario) VALUES (@Nombre, @IdRol, @Pass)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Asignar los parámetros
                    cmd.Parameters.AddWithValue("@Nombre", nuevoUsuario.Nombre_Usuario);
                    cmd.Parameters.AddWithValue("@IdRol", nuevoUsuario.IdRol_Usuario);
                    cmd.Parameters.AddWithValue("@Pass", nuevoUsuario.Pass_Usuario);

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ResponseModel ActualizarUsuario(UsuarioModel Usuario)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Preparar la consulta SQL para actualizar
                    string query = "UPDATE usuarios SET Nombre_Usuario = @Nombre_Usuario,IdRol_Usuario=@IdRol_Usuario,Pass_Usuario=@Pass_Usuario WHERE Id_Usuario = @Id_Usuario";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@Nombre_Usuario", Usuario.Nombre_Usuario);
                        cmd.Parameters.AddWithValue("@IdRol_Usuario", Usuario.IdRol_Usuario);
                        cmd.Parameters.AddWithValue("@Pass_Usuario", Usuario.Pass_Usuario);
                        cmd.Parameters.AddWithValue("@Id_Usuario", Usuario.Id_Usuario);

                        // Ejecutar el comando
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response.IsSuccess = true;
                            response.Message = "Asignacion actualizada.";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "No se actualizo la asignacion.";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al actualizar la asignacion: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response; // Retornar el resultado
        }

    }
}
