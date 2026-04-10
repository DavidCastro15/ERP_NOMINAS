using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.Models.ExtraShifts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class ExtraShiftRepository : IExtraShift
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public List<ExtraShift> GetExtraShifts(DateTime T1, DateTime T2)
        {
            List<ExtraShift> list = new List<ExtraShift>();

            try
            {

                using (cmd = new SqlCommand("SELECT * "+
                        "FROM( "+
                            "SELECT " +
                                "ld.numero_control, " +
                                "le.semana_pago, " +
                                "le.fecha, " +
                                "CASE " +
                                    "WHEN ROW_NUMBER() OVER(PARTITION BY ld.numero_empleado, le.fecha ORDER BY ld.id) >= 2 " +
                                    "THEN 2 " +
                                    "ELSE 1 " +
                                "END AS asistencia, " +
                                "ld.numero_empleado, " +
                                "ld.uso, " +
                                "le.turno, " +
                                "ld.id " +
                            "FROM listas_detalle ld " +
                            "INNER JOIN listas_encabezado le ON ld.numero_control = le.numero_control " +
                            $"WHERE le.fecha BETWEEN '{T1:yyyy-MM-dd}' AND '{T2:yyyy-MM-dd}' " +
                        ") AS Resultados " +
                        "WHERE asistencia = 2 " +
                        "ORDER BY numero_empleado, fecha ", Conex.nomi))
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

                throw new Exception("Error al obtener las asistencias: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateAttend(List<ExtraShift> list, IProgress<int> progress)
        {
            int totalRowsAffected = 0;
            int contador = 0;
            string query = "UPDATE listas_detalle SET asistencia = @asistencia WHERE id = @id";

            try
            {
                Conex.OpenNomina(); // Abrimos una sola vez

                foreach (var item in list)
                {
                    using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                    {
                        // Usamos el objeto "item" del foreach para acceder a la prop
                        cmd.Parameters.AddWithValue("@id", item.AttendListDetailId);
                        cmd.Parameters.AddWithValue("@asistencia", 2);

                        totalRowsAffected += cmd.ExecuteNonQuery();
                    }

                    contador++;
                    int porcentaje = (contador * 100) / list.Count;
                    progress?.Report(porcentaje);
                }
            }
            finally
            {
                Conex.CloseNomina(); // Aseguramos el cierre
            }

            return totalRowsAffected; // Retorna el total de actualizaciones exitosas
        }

        private ExtraShift ShowDataGrid(SqlDataReader reader)
        {
            return new ExtraShift
            {
                NumberControl = Convert.ToInt32(reader["numero_control"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"]),
                Date = Convert.ToDateTime(reader["fecha"]),
                Attend = Convert.ToInt32(reader["asistencia"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                Use = Convert.ToInt32(reader["uso"]),
                Shift = Convert.ToInt32(reader["turno"]),
                AttendListDetailId = Convert.ToInt32(reader["id"])               
            };
        }
    }
}
