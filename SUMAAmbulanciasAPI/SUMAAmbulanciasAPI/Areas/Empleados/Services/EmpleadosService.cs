using MySql.Data.MySqlClient;
using SUMAAmbulanciasAPI.Areas.Empleados.Models;
using SUMAAmbulanciasAPI.Areas.GeneralModels;
using SUMAAmbulanciasAPI.Areas.Usuarios.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SUMAAmbulanciasAPI.Areas.Empleados.Services
{
    public class EmpleadosService
    {
        private readonly IConfiguration _configuration;
        public EmpleadosService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public List<EmpleadosModel> GetEmpleados()
        {
            List<EmpleadosModel> listaEmpleados = new List<EmpleadosModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM empleados ORDER BY dt_FechaRegistro DESC", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear una instancia del modelo Empleado
                            EmpleadosModel empleado = new EmpleadosModel
                            {
                                Id_Empleado = Convert.ToInt32(reader["Id_Empleado"]),
                                AppPat_Empleado = reader["AppPat_Empleado"].ToString(),
                                AppMat_Empleado = reader["AppMat_Empleado"].ToString(),
                                Nombre_Empleado = reader["Nombre_Empleado"].ToString(),
                                Tel_Empleado = reader["Tel_Empleado"].ToString(),
                                correo_Empleado = reader["correo_Empleado"].ToString(), // Corregir la asignación de correo
                                CostHor_Empleado = Convert.ToDouble(reader["CostHor_Empleado"])
                            };

                            // Agregar el empleado a la lista
                            listaEmpleados.Add(empleado);
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

            return listaEmpleados;
        }

        public List<EmpleadosModel> GetEmpleadoById(int id)
        {
            List<EmpleadosModel> listaEmpleados = new List<EmpleadosModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM empleados  WHERE Id_Empleado = @Id_Empleado", conn);
                    cmd.Parameters.AddWithValue("@Id_Empleado", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear una instancia del modelo Empleado
                            EmpleadosModel empleado = new EmpleadosModel
                            {
                                Id_Empleado = Convert.ToInt32(reader["Id_Empleado"]),
                                AppPat_Empleado = reader["AppPat_Empleado"].ToString(),
                                AppMat_Empleado = reader["AppMat_Empleado"].ToString(),
                                Nombre_Empleado = reader["Nombre_Empleado"].ToString(),
                                Tel_Empleado = reader["Tel_Empleado"].ToString(),
                                correo_Empleado = reader["correo_Empleado"].ToString(), // Corregir la asignación de correo
                                CostHor_Empleado = Convert.ToDouble(reader["CostHor_Empleado"])
                            };

                            // Agregar el empleado a la lista
                            listaEmpleados.Add(empleado);
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

            return listaEmpleados;
        }

        public List<EmpleadoHorasModel> GetHorasByIdEmpleado(int id)
        {
            List<EmpleadoHorasModel> listaHoras = new List<EmpleadoHorasModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT H.Id_Horas,H.Id_Empleado,H.iHorasTrabajadas,H.dt_Fecha,E.CostHor_Empleado * CAST(H.iHorasTrabajadas AS DECIMAL(18,2)) AS SaldoTotal FROM horastrabajadas H INNER JOIN  empleados E ON H.Id_Empleado = E.Id_Empleado  WHERE H.Id_Empleado = @Id_Empleado", conn);
                    cmd.Parameters.AddWithValue("@Id_Empleado", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            EmpleadoHorasModel HoraEmpleado = new EmpleadoHorasModel
                            {
                                Id_Horas = Convert.ToInt32(reader["Id_Horas"]),
                                Id_Empleado = Convert.ToInt32(reader["Id_Empleado"]),
                                iHorasTrabajadas = Convert.ToInt32(reader["iHorasTrabajadas"]),
                                SaldoTotal = Convert.ToDouble(reader["SaldoTotal"]),
                                dt_Fecha = DateTime.Parse(reader["dt_Fecha"].ToString()).ToString("dd/MM/yyyy")

                            };

                            listaHoras.Add(HoraEmpleado);
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

            return listaHoras;
        }

        public List<EmpleadoHorasPagadasModel> GetHorasPagadasByIdEmpleado(int id)
        {
            List<EmpleadoHorasPagadasModel> listaHoras = new List<EmpleadoHorasPagadasModel>();

            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                // Usar la cadena de conexión con ADO.NET
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Id_HoraPagada,Id_Empleado,iHorasPagadas,PagoTotal,Feha_Registro FROM horaspagadas WHERE Id_Empleado = @Id_Empleado", conn);
                    cmd.Parameters.AddWithValue("@Id_Empleado", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            EmpleadoHorasPagadasModel HoraEmpleado = new EmpleadoHorasPagadasModel
                            {
                                Id_HoraPagada = Convert.ToInt32(reader["Id_HoraPagada"]),
                                Id_Empleado = Convert.ToInt32(reader["Id_Empleado"]),
                                iHorasPagadas = Convert.ToInt32(reader["iHorasPagadas"]),
                                PagoTotal = Convert.ToDouble(reader["PagoTotal"]),
                                Feha_Registro = DateTime.Parse(reader["Feha_Registro"].ToString()).ToString("dd/MM/yyyy")

                            };

                            listaHoras.Add(HoraEmpleado);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener datos: " + ex.Message);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error: " + ex.Message);

            }

            return listaHoras;
        }

        public ResponseModel AsignarHoras(EmpleadoHorasModel NuevasHoras)
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
                    string query = "INSERT INTO horastrabajadas (Id_Empleado, iHorasTrabajadas) " +
                                   "VALUES (@Id_Empleado, @iHoras)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@Id_Empleado", NuevasHoras.Id_Empleado);
                        cmd.Parameters.AddWithValue("@iHoras", NuevasHoras.iHorasTrabajadas);
                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();
                    }
                }
                response.IsSuccess = true; // Inserción exitosa
                response.Message = "Horas asignadas exitosamente.";
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al asignar las horas: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response; // Retornar el resultado
        }

        public ResponseModel PagarHoras(EmpleadoPagarModel pagar)
        {
            // Leer la cadena de conexión desde appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var response = new ResponseModel();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string ids = string.Join(",", pagar.Ids);

                    MySqlCommand cmd = new MySqlCommand($"SELECT Id_Horas FROM horastrabajadas WHERE Id_Horas IN ({ids})", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        var IdsExist = new List<int>();
                        while (reader.Read())
                        {
                            IdsExist.Add(reader.GetInt32(0));
                        }

                        var IdsFaltantes = pagar.Ids.Distinct().Except(IdsExist).ToList();

                        if (IdsFaltantes.Any())
                        {
                            response.IsSuccess = false;
                            response.Message = "Hay horas que posiblemente ya fueron pagadas refresque la pantalla y vuelva a intentar";
                            return response;
                        }
                    }

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Insertar en la tabla pagarHoras
                            MySqlCommand insertCmd = new MySqlCommand("INSERT INTO horaspagadas(Id_Empleado, PagoTotal,iHorasPagadas) VALUES(@Id_Empleado, @TotalPagar,@iHorasPagadas)", conn, transaction);
                            insertCmd.Parameters.AddWithValue("@Id_Empleado", pagar.Id_Empleado);
                            insertCmd.Parameters.AddWithValue("@TotalPagar", pagar.TotalPagar);
                            insertCmd.Parameters.AddWithValue("@iHorasPagadas", pagar.iHorasPagadas);
                            insertCmd.ExecuteNonQuery();

                           // Insertar en la tabla salidas
                            MySqlCommand insertSalidasCmd = new MySqlCommand("INSERT INTO salidas(VcMotivoSalida,MmontoSalida) VALUES(@VcMotivoSalida, @MmontoSalida)", conn, transaction);
                            insertSalidasCmd.Parameters.AddWithValue("@VcMotivoSalida", "Pago de honorarios");
                            insertSalidasCmd.Parameters.AddWithValue("@MmontoSalida", pagar.TotalPagar);
                            insertSalidasCmd.ExecuteNonQuery();

                            // Eliminar registros de horastrabajadas
                            MySqlCommand deleteCmd = new MySqlCommand($"DELETE FROM horastrabajadas WHERE Id_Horas IN ({ids})", conn, transaction);
                            for (int i = 0; i < pagar.Ids.Count; i++)
                            {
                                deleteCmd.Parameters.AddWithValue($"@Id{i}", pagar.Ids[i]);
                            }
                            deleteCmd.ExecuteNonQuery();

                            //Confirmar la transacción
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Si hay un error, revertir la transacción
                            transaction.Rollback();
                            response.IsSuccess = false;
                            response.Message = "Error al insertar en la base de datos: " + ex.Message;
                            return response;
                        }
                    }

                    // Retornar mensaje de éxito si todo sale bien
                    response.IsSuccess = true;
                    response.Message = "Pago procesado exitosamente.";
                }
            }
            catch (MySqlException ex)
            {
                response.IsSuccess = false;
                response.Message = "Error al obtener datos: " + ex.Message;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response;
        }

        public ResponseModel ActualizarAsignacionHoras(EmpleadoHorasModel Horas)
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
                    string query = "UPDATE horastrabajadas SET iHorasTrabajadas = @iHorasTrabajadas WHERE Id_Horas = @Id_Horas";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@iHorasTrabajadas", Horas.iHorasTrabajadas);
                        cmd.Parameters.AddWithValue("@Id_Horas", Horas.Id_Horas);

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

        public ResponseModel EliminarAsignacionHorasByIdHoras(int id)
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
                    string query = "DELETE FROM horastrabajadas WHERE Id_Horas = @Id";

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

        public ResponseModel EliminarHorasPagadasByIdHorasP(int id)
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
                    string query = "DELETE FROM horaspagadas WHERE Id_HoraPagada  = @Id";

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

        public ResponseModel InsertarEmpleado(EmpleadosModel nuevoEmpleado)
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
                    string query = "INSERT INTO empleados (AppPat_Empleado, AppMat_Empleado, Nombre_Empleado, Tel_Empleado, correo_Empleado, CostHor_Empleado) " +
                                   "VALUES (@AppPat_Empleado, @AppMat_Empleado, @Nombre_Empleado, @Tel_Empleado, @correo_Empleado, @CostHor_Empleado)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@AppPat_Empleado", nuevoEmpleado.AppPat_Empleado);
                        cmd.Parameters.AddWithValue("@AppMat_Empleado", nuevoEmpleado.AppMat_Empleado);
                        cmd.Parameters.AddWithValue("@Nombre_Empleado", nuevoEmpleado.Nombre_Empleado);
                        cmd.Parameters.AddWithValue("@Tel_Empleado", nuevoEmpleado.Tel_Empleado);
                        cmd.Parameters.AddWithValue("@correo_Empleado", nuevoEmpleado.correo_Empleado);
                        cmd.Parameters.AddWithValue("@CostHor_Empleado", nuevoEmpleado.CostHor_Empleado);


                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();
                    }
                }
                response.IsSuccess = true; // Inserción exitosa
                response.Message = "Empleado insertado exitosamente.";
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al insertar el empleado: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response; // Retornar el resultado
        }

        public ResponseModel ActualizarEmpleado(EmpleadosModel empleadoActualizado)
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
                    string query = "UPDATE empleados SET AppPat_Empleado = @AppPat_Empleado, AppMat_Empleado = @AppMat_Empleado, " +
                                   "Nombre_Empleado = @Nombre_Empleado, Tel_Empleado = @Tel_Empleado, correo_Empleado = @correo_Empleado, " +
                                   "CostHor_Empleado = @CostHor_Empleado WHERE Id_Empleado = @Id_Empleado";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@AppPat_Empleado", empleadoActualizado.AppPat_Empleado);
                        cmd.Parameters.AddWithValue("@AppMat_Empleado", empleadoActualizado.AppMat_Empleado);
                        cmd.Parameters.AddWithValue("@Nombre_Empleado", empleadoActualizado.Nombre_Empleado);
                        cmd.Parameters.AddWithValue("@Tel_Empleado", empleadoActualizado.Tel_Empleado);
                        cmd.Parameters.AddWithValue("@correo_Empleado", empleadoActualizado.correo_Empleado);
                        cmd.Parameters.AddWithValue("@CostHor_Empleado", empleadoActualizado.CostHor_Empleado);
                        cmd.Parameters.AddWithValue("@Id_Empleado", empleadoActualizado.Id_Empleado);

                        // Ejecutar el comando
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response.IsSuccess = true;
                            response.Message = "Empleado actualizado exitosamente.";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "No se encontró un empleado con el ID proporcionado.";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al actualizar el empleado: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                response.IsSuccess = false;
                response.Message = "Se produjo un error: " + ex.Message;
            }

            return response; // Retornar el resultado
        }

        public ResponseModel EliminarEmpleado(int id)
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
                    string query = "DELETE FROM empleados WHERE Id_Empleado = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.AddWithValue("@Id", id);

                        // Ejecutar el comando
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response.IsSuccess = true;
                            response.Message = "Empleado eliminado correctamente.";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "No se encontró el empleado para eliminar.";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar excepciones de MySQL
                response.IsSuccess = false;
                response.Message = "Error al eliminar el empleado: " + ex.Message;
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
