using MySql.Data.MySqlClient;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.Ganancias.Models;
using SUMAAmbulanciasAPI.Areas.GeneralModels;

namespace SUMAAmbulanciasAPI.Areas.Ganancias.Services
{
    public class gananciasService
    {
        private readonly IConfiguration _configuration;
        public gananciasService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public List<gananciasModel> GetGanancias()
        {
            List<gananciasModel> listaGanancias = new List<gananciasModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Id_Entrada,MCantidad,Fecha_Inicio,dt_FechaRegistro FROM entradasmembrecias ORDER BY dt_FechaRegistro DESC", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gananciasModel ganancia = new gananciasModel
                            {
                                Id_Entrada = Convert.ToInt32(reader["Id_Entrada"]),
                                MCantidad = Convert.ToDouble(reader["MCantidad"]),
                                Fecha_Inicio = DateTime.Parse(reader["Fecha_Inicio"].ToString()).ToString("dd/MM/yyyy"),
                                dt_FechaRegistro = DateTime.Parse(reader["dt_FechaRegistro"].ToString()).ToString("dd/MM/yyyy")
                            };

                            listaGanancias.Add(ganancia);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener ganancias: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaGanancias;
        }

        public List<gastosModel> GetGastos()
        {
            List<gastosModel> listaGastos = new List<gastosModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Id_Salida,VcMotivoSalida,MmontoSalida,dt_FechaRegistro FROM salidas ORDER BY dt_FechaRegistro DESC", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gastosModel gasto = new gastosModel
                            {
                                Id_Salida = Convert.ToInt32(reader["Id_Salida"]),
                                VcMotivoSalida = reader["VcMotivoSalida"].ToString(),
                                MmontoSalida = Convert.ToDouble(reader["MmontoSalida"]),
                                dt_FechaRegistro = DateTime.Parse(reader["dt_FechaRegistro"].ToString()).ToString("dd/MM/yyyy")
                            };

                            listaGastos.Add(gasto);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener las salidas: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaGastos;
        }

        public ResponseModel EliminarSalida(int id)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Preparar la consulta SQL para eliminar
                    string query = "DELETE FROM salidas WHERE Id_Salida = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@Id", id);

                        // Ejecutar el comando
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response.IsSuccess = true;
                            response.Message = "Asignacion eliminada correctamente.";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "No se encontró la asignacion para eliminar.";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al eliminar la asignacion: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response; // Retornar el resultado
        }

        public ResponseModel EliminarGanancia(int id)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Preparar la consulta SQL para eliminar
                    string query = "DELETE FROM entradasmembrecias WHERE Id_Entrada = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@Id", id);

                        // Ejecutar el comando
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response.IsSuccess = true;
                            response.Message = "Asignacion eliminada correctamente.";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "No se encontró la asignacion para eliminar.";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al eliminar la asignacion: " + ex.Message;
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
