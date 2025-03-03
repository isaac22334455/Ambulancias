using MySql.Data.MySqlClient;
using SUMAAmbulanciasAPI.Areas.Clientes.Models;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.GeneralModels;

namespace SUMAAmbulanciasAPI.Areas.Clientes.Services
{
    public class ClienteService
    {
        private readonly IConfiguration _configuration;
        public ClienteService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public List<ClientesModel> GetClientes()
        {
            List<ClientesModel> listaClientes = new List<ClientesModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT " +
                 "c.Id_Cliente, c.Nombre_Cliente, c.AppMat_Cliente, " +
                 "c.AppPat_Cliente, c.Tel_Cliente, c.Correo_Cliente, " +
                 "IF(TIMESTAMPDIFF(MONTH, CURDATE(), c.Fecha_Fin) < 0 OR " +
                 "(TIMESTAMPDIFF(MONTH, CURDATE(), c.Fecha_Fin) = 0 AND DATEDIFF(c.Fecha_Fin, CURDATE()) < 0), " +
                 "'0 meses y 0 días', " +
                 "CONCAT(TIMESTAMPDIFF(MONTH, CURDATE(), c.Fecha_Fin), ' meses y ', " +
                 "GREATEST(DATEDIFF(c.Fecha_Fin, DATE_ADD(CURDATE(), INTERVAL TIMESTAMPDIFF(MONTH, CURDATE(), c.Fecha_Fin) MONTH)), 0), ' días')) AS TiempoRestante, " +
                 "CONCAT(ct.vcTipoCliente, ' (', m.IFrecMen_Membrecia, ' meses)') AS TipoMembrecia " +
                 "FROM clientes c " +
                 "INNER JOIN membrecias m ON c.IdTipoMembrecia = m.Id_Membrecia " +
                 "INNER JOIN cattipoclientes ct ON m.Id_TipoCliente = ct.IdTipoCliente " +
                 "ORDER BY c.dt_FechaRegistro DESC", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClientesModel Cliente = new ClientesModel
                            {
                                Id_Cliente = Convert.ToInt32(reader["Id_Cliente"]),
                                Nombre_Cliente = reader["Nombre_Cliente"].ToString(),
                                AppMat_Cliente = reader["AppMat_Cliente"].ToString(),
                                AppPat_Cliente = reader["AppPat_Cliente"].ToString(),
                                Tel_Cliente = reader["Tel_Cliente"].ToString(),
                                Correo_Cliente = reader["Correo_Cliente"].ToString(),
                                TipoMembrecia = reader["TipoMembrecia"].ToString(),
                                TiempoRestante = reader["TiempoRestante"].ToString(),
                            };
                            listaClientes.Add(Cliente);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener Tipos de clientes: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaClientes;
        }

        public List<ClientesModel> GetClienteById(int id)
        {
            List<ClientesModel> listaClientes = new List<ClientesModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Id_Cliente,Nombre_Cliente,AppMat_Cliente," +
                        "                                AppPat_Cliente,Tel_Cliente,Correo_Cliente,IdTipoMembrecia FROM clientes  WHERE Id_Cliente = @Id_Cliente", conn);
                    cmd.Parameters.AddWithValue("@Id_Cliente", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear una instancia del modelo Empleado
                            ClientesModel cliente = new ClientesModel
                            {
                                Id_Cliente = Convert.ToInt32(reader["Id_Cliente"]),
                                Nombre_Cliente = reader["Nombre_Cliente"].ToString(),
                                AppMat_Cliente = reader["AppMat_Cliente"].ToString(),
                                AppPat_Cliente = reader["AppPat_Cliente"].ToString(),
                                Tel_Cliente = reader["Tel_Cliente"].ToString(),
                                Correo_Cliente = reader["Correo_Cliente"].ToString(),
                                IdTipoMembrecia = Convert.ToInt32(reader["IdTipoMembrecia"]), // Corregir la asignación de correo
                            };

                            // Agregar el empleado a la lista
                            listaClientes.Add(cliente);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener empleados: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaClientes;
        }

        public ResponseModel InsertarCliente(ClientesModel cliente)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    DateTime fechaInicio = cliente.FechaInicio == default ? DateTime.Today : cliente.FechaInicio;

                    DateTime? fechaFin = null;
                    if (cliente.bPagado == 1)
                    {
                        fechaFin = fechaInicio.AddMonths(cliente.iMeses);
                    }

                    // Preparar la consulta SQL para insertar
                    string query = "INSERT INTO clientes(Nombre_Cliente, AppPat_Cliente, AppMat_Cliente, Tel_Cliente, Correo_Cliente, IdTipoMembrecia" +
                                   (cliente.bPagado == 1 ? ", Fecha_Inicio, Fecha_Fin" : "") + ") " +
                                   "VALUES (@Nombre_Cliente, @AppPat_Cliente, @AppMat_Cliente, @Tel_Cliente, @Correo_Cliente, @IdTipoMembrecia" +
                                   (cliente.bPagado == 1 ? ", @FechaInicio, @FechaFin" : "") + ")";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros comunes
                        cmd.Parameters.AddWithValue("@Nombre_Cliente", cliente.Nombre_Cliente);
                        cmd.Parameters.AddWithValue("@AppPat_Cliente", cliente.AppPat_Cliente);
                        cmd.Parameters.AddWithValue("@AppMat_Cliente", cliente.AppMat_Cliente);
                        cmd.Parameters.AddWithValue("@Tel_Cliente", cliente.Tel_Cliente);
                        cmd.Parameters.AddWithValue("@Correo_Cliente", cliente.Correo_Cliente);
                        cmd.Parameters.AddWithValue("@IdTipoMembrecia", cliente.IdTipoMembrecia);

                        // Asignar los parámetros de las fechas solo si bPagado es 1
                        if (cliente.bPagado == 1)
                        {
                            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                            cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                            // Insertar un nuevo registro en la tabla 'entradasmembrecias' con el valor de MCantidad
                            string insertQuery = "INSERT INTO entradasmembrecias (MCantidad) VALUES (@MCantidad)";

                            using (MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@MCantidad", cliente.MCantidad);
                                // Ejecutar la inserción
                                cmdInsert.ExecuteNonQuery();
                            }

                        }

                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();
                    }
                }

                response.IsSuccess = true; // Inserción exitosa
                response.Message = "Éxito.";
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

        public ResponseModel RenovarMembrecia(RenovarMembreciaModel cliente)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Calcular fecha de inicio y fecha de fin
                    DateTime fechaInicio = cliente.FechaInicio == default ? DateTime.Today : cliente.FechaInicio;
                    DateTime fechaFin = fechaInicio.AddMonths(cliente.iMeses);

                    // Actualizar la tabla 'clientes' para cambiar Fecha_Inicio y Fecha_Fin
                    string updateQuery = "UPDATE clientes SET Fecha_Inicio = @FechaInicio, Fecha_Fin = @FechaFin,IdTipoMembrecia=@IdTipoMembrecia WHERE Id_Cliente = @Id_Cliente";

                    using (MySqlCommand cmdUpdate = new MySqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        cmdUpdate.Parameters.AddWithValue("@FechaFin", fechaFin);
                        cmdUpdate.Parameters.AddWithValue("@Id_Cliente", cliente.Id_Cliente);
                        cmdUpdate.Parameters.AddWithValue("@IdTipoMembrecia", cliente.IdTipoMembrecia);

                        // Ejecutar la actualización
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // Insertar un nuevo registro en la tabla 'entradasmembrecias' con el valor de MCantidad
                    string insertQuery = "INSERT INTO entradasmembrecias (MCantidad) VALUES (@MCantidad)";

                    using (MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@MCantidad", cliente.MCantidad);
                        // Ejecutar la inserción
                        cmdInsert.ExecuteNonQuery();
                    }
                }

                response.IsSuccess = true; 
                response.Message = "Éxito en la renovación de la membresía.";
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

        public ResponseModel ActualizarCliente(ClientesModel cliente)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Preparar la consulta SQL para insertar
                    string query = "UPDATE clientes SET " +
                                   "Nombre_Cliente = @Nombre_Cliente, " +
                                   "AppPat_Cliente = @AppPat_Cliente, " +
                                   "AppMat_Cliente = @AppMat_Cliente, " +
                                   "Tel_Cliente = @Tel_Cliente, " +
                                   "Correo_Cliente = @Correo_Cliente " +
                                   "WHERE Id_Cliente = @Id_Cliente";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@Nombre_Cliente", cliente.Nombre_Cliente);
                        cmd.Parameters.AddWithValue("@AppPat_Cliente", cliente.AppPat_Cliente);
                        cmd.Parameters.AddWithValue("@AppMat_Cliente", cliente.AppMat_Cliente);
                        cmd.Parameters.AddWithValue("@Tel_Cliente", cliente.Tel_Cliente);
                        cmd.Parameters.AddWithValue("@Correo_Cliente", cliente.Correo_Cliente);
                        cmd.Parameters.AddWithValue("@Id_Cliente", cliente.Id_Cliente);
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

        public ResponseModel EliminarClienteById(int id)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM Clientes WHERE id_Cliente = @Id";

                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response.IsSuccess = true;
                            response.Message = "Exito.";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "No se encontró la asignación para eliminar.";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al eliminar: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response; // Retornar el resultado
        }

        public List<TipoClientesModel> GetTiposClientes()
        {
            List<TipoClientesModel> listaTClientes = new List<TipoClientesModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT IdTipoCliente,vcTipoCliente,Dt_FechaRegistro FROM cattipoclientes ORDER BY Dt_FechaRegistro DESC", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoClientesModel TipoCliente = new TipoClientesModel
                            {
                                IdTipoCliente = Convert.ToInt32(reader["IdTipoCliente"]),
                                vcTipoCliente = reader["vcTipoCliente"].ToString(),
                                Dt_FechaRegistro = DateTime.Parse(reader["Dt_FechaRegistro"].ToString()).ToString("dd/MM/yyyy")

                            };
                            listaTClientes.Add(TipoCliente);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener Tipos de clientes: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaTClientes;
        }

        public ResponseModel InsertarTipoCliente(TipoClientesModel nuevoTipo)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Preparar la consulta SQL para insertar
                    string query = "INSERT INTO cattipoclientes (vcTipoCliente) " +
                                   "VALUES (@vcTipoCliente)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@vcTipoCliente", nuevoTipo.vcTipoCliente);
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

        public ResponseModel EliminarTipoClienteById(int id)
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
                    string checkQuery = "SELECT COUNT(*) FROM membrecias WHERE Id_TipoCliente = @Id";

                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Id", id);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Existen tipos de membresía con este tipo de cliente asignado. Favor de eliminar primero dichas membresías.";
                            return response;
                        }
                    }

                    // Preparar la consulta SQL para eliminar si no hay membresías asociadas
                    string deleteQuery = "DELETE FROM cattipoclientes WHERE IdTipoCliente = @Id";

                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response.IsSuccess = true;
                            response.Message = "Exito.";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "No se encontró la asignación para eliminar.";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al eliminar: " + ex.Message;
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
