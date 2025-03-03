using MySql.Data.MySqlClient;
using SUMAAmbulanciasAPI.Areas.Clientes.Models;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.GeneralModels;
using SUMAAmbulanciasAPI.Areas.Membrecias.Models;

namespace SUMAAmbulanciasAPI.Areas.Membrecias.Services
{
    public class MembreciaService
    {
        private readonly IConfiguration _configuration;
        public MembreciaService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public List<MembreciaModel> GetMembrecias()
        {
            List<MembreciaModel> listaMembrecias = new List<MembreciaModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT m.Id_Membrecia,m.IFrecMen_Membrecia,tc.vcTipoCliente,m.Dt_FechaRegistro,m.MCantidadCobrar FROM membrecias m INNER JOIN cattipoclientes tc ON m.Id_TipoCliente = tc.IdTipoCliente ORDER BY m.Dt_FechaRegistro DESC", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MembreciaModel membrecia = new MembreciaModel
                            {
                                Id_Membrecia = Convert.ToInt32(reader["Id_Membrecia"]),
                                IFrecMen_Membrecia = Convert.ToInt32(reader["IFrecMen_Membrecia"]),
                                vcTipoCliente = reader["vcTipoCliente"].ToString(),
                                Dt_FechaRegistro = DateTime.Parse(reader["Dt_FechaRegistro"].ToString()).ToString("dd/MM/yyyy"),
                                MCantidadCobrar = Convert.ToDouble(reader["MCantidadCobrar"])
                            };
                            listaMembrecias.Add(membrecia);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener las membrecias: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaMembrecias;
        }

        public List<MembreciaModel> GetcboMembrecias()
        {
            List<MembreciaModel> listaMembrecias = new List<MembreciaModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT" +
                        "                               m.Id_Membrecia," +
                        "                               CONCAT(t.vcTipoCliente, ' (', m.IFrecMen_Membrecia, ' meses) ','Costo:','$',MCantidadCobrar) AS vcTipoCliente" +
                        "                               FROM membrecias m" +
                        "                               INNER JOIN cattipoclientes t ON m.Id_TipoCliente = t.IdTipoCliente" +
                        "                              ORDER BY m.Dt_FechaRegistro DESC", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MembreciaModel membrecia = new MembreciaModel
                            {
                                Id_Membrecia = Convert.ToInt32(reader["Id_Membrecia"]),
                                vcTipoCliente =reader["vcTipoCliente"].ToString(),
                            };
                            listaMembrecias.Add(membrecia);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener las membrecias: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaMembrecias;
        }

        public List<MembreciaModel> GetMembreciasById(int id)
        {
            List<MembreciaModel> listaMembrecias = new List<MembreciaModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Id_Membrecia,IFrecMen_Membrecia,Id_TipoCliente,Dt_FechaRegistro,MCantidadCobrar FROM membrecias WHERE Id_Membrecia = @Id_Membrecia", conn);
                    cmd.Parameters.AddWithValue("@Id_Membrecia", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear una instancia del modelo Empleado
                            MembreciaModel membrecia = new MembreciaModel
                            {
                                Id_Membrecia = Convert.ToInt32(reader["Id_Membrecia"]),
                                IFrecMen_Membrecia = Convert.ToInt32(reader["IFrecMen_Membrecia"]),
                                Id_TipoCliente = Convert.ToInt32(reader["Id_TipoCliente"]),
                                MCantidadCobrar = Convert.ToDouble(reader["MCantidadCobrar"])
                            };

                            // Agregar el empleado a la lista
                            listaMembrecias.Add(membrecia);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener membrecias: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaMembrecias;
        }

        public ResponseModel InsertarMembrecia(MembreciaModel nuevaMem)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO membrecias (IFrecMen_Membrecia,Id_TipoCliente,MCantidadCobrar) " +
                                   "VALUES (@IFrecMen_Membrecia,@Id_TipoCliente,@MCantidadCobrar)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@IFrecMen_Membrecia", nuevaMem.IFrecMen_Membrecia);
                        cmd.Parameters.AddWithValue("@Id_TipoCliente", nuevaMem.Id_TipoCliente);
                        cmd.Parameters.AddWithValue("@MCantidadCobrar", nuevaMem.MCantidadCobrar);
                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();
                    }
                }
                response.IsSuccess = true; // Inserción exitosa
                response.Message = "Exito.";
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response; // Retornar el resultado
        }

        public ResponseModel ActualizarMembrecia(MembreciaModel ActualizaMem)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE membrecias SET IFrecMen_Membrecia = @IFrecMen_Membrecia,Id_TipoCliente=@Id_TipoCliente,MCantidadCobrar=@MCantidadCobrar WHERE Id_Membrecia = @Id_Membrecia";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@Id_Membrecia", ActualizaMem.Id_Membrecia);
                        cmd.Parameters.AddWithValue("@IFrecMen_Membrecia", ActualizaMem.IFrecMen_Membrecia);
                        cmd.Parameters.AddWithValue("@Id_TipoCliente", ActualizaMem.Id_TipoCliente);
                        cmd.Parameters.AddWithValue("@MCantidadCobrar", ActualizaMem.MCantidadCobrar);
                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();
                    }
                }
                response.IsSuccess = true; // Inserción exitosa
                response.Message = "Exito.";
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response; // Retornar el resultado
        }

        public ResponseModel EliminarMembrecia(int id)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Verificar si existen membresías asociadas a este tipo de cliente
                    string checkQuery = "SELECT COUNT(*) FROM clientes WHERE IdTipoMembrecia = @Id";

                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Id", id);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Existen clientes con este tipo de membrecia asignado. Favor de eliminar primero dichos clientes.";
                            return response;
                        }
                    }

                    string query = "DELETE FROM membrecias WHERE Id_Membrecia = @Id_Membrecia";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@Id_Membrecia",id);
                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();
                    }
                }
                response.IsSuccess = true; // Inserción exitosa
                response.Message = "Exito.";
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error: " + ex.Message;
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
