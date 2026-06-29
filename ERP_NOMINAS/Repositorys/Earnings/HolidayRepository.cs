using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Holidays;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class HolidayRepository : IHoliday
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateHoliday(Holiday h)
        {
            string query = " INSERT INTO festivos(fecha,festividad) VALUES(@fecha,@festividad) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@fecha", h.Day.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                cmd.Parameters.AddWithValue("@festividad", h._Holiday);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteHoliday(int Id)
        {
            string query = $" DELETE FROM festivos WHERE id = {Id} ";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Holiday> GetHolidays()
        {
            List<Holiday> list = new List<Holiday>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM festivos ORDER BY fecha DESC", Conex.nomi))
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

                throw new Exception("Error al obtener los dias festivos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        private static Holiday ShowDataGrid(SqlDataReader reader)
        {
            return new Holiday
            {
                Id = Convert.ToInt32(reader["id"]),
                Day = Convert.ToDateTime(reader["fecha"]),
                _Holiday = Convert.ToString(reader["festividad"])
            };
        }
    }
}
