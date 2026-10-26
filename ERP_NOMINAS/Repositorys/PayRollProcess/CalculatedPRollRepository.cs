using ERP_NOMINAS.Models.PayRollProcess.CalculatedPayRolls;
using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.PayRollProcess
{
    public class CalculatedPRollRepository : ICalculatedPRoll
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public List<CalculatedPRolls> FilterByPayWeek(int PayWeek)
        {
            List<CalculatedPRolls> list = new List<CalculatedPRolls>();

            string query = $@"SELECT * FROM calculos WHERE no_semana LIKE @param ORDER BY no_semana DESC";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@param", $"%{PayWeek}%");
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
                throw new Exception($"Error al obtener las nominas: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        public List<CalculatedPRolls> GetCalculatedPRolls()
        {
            List<CalculatedPRolls> list = new List<CalculatedPRolls>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT * FROM calculos ORDER BY no_semana DESC", Conex.nomi))
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

                throw new Exception("Error al obtener las nominas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        private CalculatedPRolls ShowDataGrid(SqlDataReader reader)
        {
            return new CalculatedPRolls {
                Id = Convert.ToInt32(reader["id"]),
                PayRollId = Convert.ToInt32(reader["nomina"]),
                PayWeek = Convert.ToInt32(reader["no_semana"]),
                Cycle = Convert.ToString(reader["ciclo"]),
                InitialDate = Convert.ToDateTime(reader["fecha_desde"]),
                EndDate = Convert.ToDateTime(reader["fecha_hasta"]),
                PayPFPP = Convert.ToBoolean(reader["pago_presencia_fyp"]),
                Confirmed = reader["nomina_confirmacion"] != DBNull.Value && Convert.ToBoolean(reader["nomina_confirmacion"])
            };
        }
    }
}
