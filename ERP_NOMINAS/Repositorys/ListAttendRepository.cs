using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Attendance.ListAttendance;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class ListAttendRepository : IListAttend
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int AddEmployeeDetails(ListAttendDetail l)
        {
            string query = "INSERT INTO listas_detalle(numero_control,asistencia,estatus,id_nomina,numero_empleado,uso,id_categoria,categoria_requerida,turno_periodo,turno_trabajado) " +
                           "VALUES (@numero_control,@asistencia,@estatus,@id_nomina,@numero_empleado,@uso,@id_categoria,@categoria_requerida,@turno_periodo,@turno_trabajado) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@numero_control", l.NumberControl);
                cmd.Parameters.AddWithValue("@asistencia", 1);
                cmd.Parameters.AddWithValue("@estatus", l.Status);
                cmd.Parameters.AddWithValue("@id_nomina", l.PayRoll);
                cmd.Parameters.AddWithValue("@numero_empleado", l.NumberEmployee);
                cmd.Parameters.AddWithValue("@uso", l.UseWorked);
                cmd.Parameters.AddWithValue("@id_categoria", l.CategoryWorked);
                cmd.Parameters.AddWithValue("@categoria_requerida", l.CategoryWorked);
                cmd.Parameters.AddWithValue("@turno_periodo", l.ShiftWorked);
                cmd.Parameters.AddWithValue("@turno_trabajado", l.ShiftWorked);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteEmployeeListAttend(int ControlNumber, int NumberEmployee)
        {
            string query = $"DELETE FROM listas_detalle WHERE numero_control = {ControlNumber} AND numero_empleado = {NumberEmployee} ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteListAttend(int NumberControl)
        {
            string query = @"
                            BEGIN TRANSACTION;
                            BEGIN TRY
                                DELETE FROM listas_detalle WHERE numero_control = @NumberControl;
                                DELETE FROM listas_encabezado WHERE numero_control = @NumberControl;
                                COMMIT TRANSACTION;
                            END TRY
                            BEGIN CATCH
                                ROLLBACK TRANSACTION;
                                THROW;
                            END CATCH";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@NumberControl", NumberControl);
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<ListAttendDetailReport> GetAttendDataReport(int ControlNumber)
        {
            List<ListAttendDetailReport> list = new List<ListAttendDetailReport>();

            try
            {

                using (cmd = new SqlCommand("SELECT ls.numero_control,ls.numero_empleado, "+
                                            "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) as n_completo, ls.uso, (SELECT TOP 1 d.nombre FROM departamentos d WHERE d.id_deparamento = u.departamento) AS departamento, "+
                                                "CONCAT(ls.categoria_requerida, '-', c.nombre_categoria)as categoria_requerida ,ls.turno_trabajado,ls.estatus "+
                                                  "FROM listas_detalle ls "+
                                            "INNER JOIN empleados e "+
                                            "ON ls.numero_empleado = e.numero_empleado "+
                                            "INNER JOIN categorias c "+
                                            "ON ls.categoria_requerida = c.id_categoria "+
                                            "INNER JOIN usos u "+
                                            "ON u.uso = ls.uso "+
                                            $"WHERE ls.numero_control = {ControlNumber} ORDER BY ls.numero_control DESC", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowReportDetails(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener las listas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<ListAttendDetail> GetDetailsAttend(int ControlNumber)
        {
            List<ListAttendDetail> list = new List<ListAttendDetail>();

            try
            {

                using (cmd = new SqlCommand($"SELECT * FROM listas_detalle WHERE numero_control = {ControlNumber} ORDER BY numero_control ASC", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                       
                        while (reader.Read())
                        {
                            list.Add(ShowDetails(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener las listas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public ListAttendDetail GetListAttendDetail(int ControlNumber,int NumberEmployee)
        {
            using (cmd = new SqlCommand($"SELECT * FROM listas_detalle WHERE numero_control = {ControlNumber} AND numero_empleado = {NumberEmployee} ORDER BY numero_control ASC", Conex.nomi))
            {

                Conex.OpenNomina();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        return ShowDetails(reader);
                    }
                }
            }
            return null;
        }

        public List<ListAttendC> GetListAttends()
        {
            List<ListAttendC> list = new List<ListAttendC>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM listas_encabezado ORDER BY numero_control DESC", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowDataGrid(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener las listas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int GetPwdDelete()
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployeeListAttend(ListAttendDetail l)
        {
            string query = "UPDATE listas_detalle SET categoria_requerida=@categoria_requerida,uso=@uso,turno_trabajado=@turno_trabajado "+
                                    "WHERE numero_empleado = @numero_empleado AND numero_control = @numero_control";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@numero_control", l.NumberControl);
                cmd.Parameters.AddWithValue("@numero_empleado", l.NumberEmployee);
                cmd.Parameters.AddWithValue("@categoria_requerida", l.CategoryWorked);
                cmd.Parameters.AddWithValue("@uso", l.UseWorked);
                cmd.Parameters.AddWithValue("@turno_trabajado", l.ShiftWorked);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private ListAttendC ShowDataGrid(SqlDataReader reader)
        {
            return new ListAttendC
            {
                PayrollId = Convert.ToInt32(reader["id_nomina"]),
                ControlNumber = Convert.ToInt32(reader["numero_control"]),
                Date = Convert.ToDateTime(reader["fecha"]),
                Shift = Convert.ToInt32(reader["turno"]),
                Period = Convert.ToString(reader["periodo"]),
                Status = Convert.ToString(reader["estatus"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"])
            };
        }

        private ListAttendDetail ShowDetails(SqlDataReader reader)
        {
            return new ListAttendDetail
            {
                NumberControl = Convert.ToInt32(reader["numero_control"]),
                PayRoll = Convert.ToInt32(reader["id_nomina"]),
                Status = Convert.ToString(reader["estatus"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                UseWorked = Convert.ToInt32(reader["uso"]),
                CategoryWorked = Convert.ToInt32(reader["categoria_requerida"]),
                ShiftWorked = Convert.ToInt32(reader["turno_trabajado"])
            };
        }

        private ListAttendDetailReport ShowReportDetails(SqlDataReader reader)
        {
            return new ListAttendDetailReport
            {
                NumberControl = Convert.ToInt32(reader["numero_control"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                UseWorked = Convert.ToInt32(reader["uso"]),
                Department = Convert.ToString(reader["departamento"]),
                CategoryWorked = Convert.ToString(reader["categoria_requerida"]),
                ShiftWorked = Convert.ToInt32(reader["turno_trabajado"]),
                Status = Convert.ToString(reader["estatus"])
            };
        }
    }
}
