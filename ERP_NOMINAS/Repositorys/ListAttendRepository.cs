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

        public int DeleteListAttend(int NumberControl)
        {
            string query = $"DELETE FROM listas_encabezado WHERE numero_control = {NumberControl} " +
                           $"DELETE FROM listas_detalle WHERE numero_control = {NumberControl}";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
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

        public int UpdateListAttend(ListAttendC listAttend)
        {
            throw new NotImplementedException();
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
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                UseWorked = Convert.ToInt32(reader["uso"]),
                CategoryWorked = Convert.ToInt32(reader["categoria_requerida"]),
                ShiftWorked = Convert.ToInt32(reader["turno_trabajado"])
            };
        }
    }
}
