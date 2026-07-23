using ERP_NOMINAS.Models.PayRollProcess.ApplyDeductions;
using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.PayRollProcess
{
    public class ApplyDeductionRepository : IAPDeduction
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public int CheckedAll()
        {
            string query = "UPDATE conceptos_deducciones_aplicados SET aplica = IIF((SELECT TOP 1 aplica FROM conceptos_deducciones_aplicados )  = 'Si' ,'No','Si')";

            using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();

                return cmd.ExecuteNonQuery();
            }
        }

        public int CheckedOne(int Id,string Stat)
        {
            string st = Stat == "Si" ? "No" : "Si";

            string query = "UPDATE conceptos_deducciones_aplicados SET aplica = @st WHERE id = @id";

            using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@st", st);             

                Conex.OpenNomina();

                return cmd.ExecuteNonQuery();
            }
        }

        public List<APDeduction> GetAPDeductions()
        {
            List<APDeduction> list = new List<APDeduction>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT cdp.id,cdp.id_deduccion,c.nombre_concepto,cdp.aplica
                                                            FROM conceptos_deducciones_aplicados cdp
                                                            INNER JOIN conceptos_pd c
                                                            ON cdp.id_deduccion = c.id_concepto
                                                            ORDER BY cdp.id ASC", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(new APDeduction {
                                Id = Convert.ToInt32(reader["id"]),
                                IdConcept = Convert.ToInt32(reader["id_deduccion"]),
                                NameConcept = Convert.ToString(reader["nombre_concepto"]),
                                Apply = Convert.ToString(reader["aplica"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener las deducciones: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }
    }
}
