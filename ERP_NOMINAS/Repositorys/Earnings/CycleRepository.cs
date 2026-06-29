using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Cycles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class CycleRepository : ICycle
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int ChangeCycle(string c)
        {
            string query = "UPDATE ciclo SET ciclo=@ciclo";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@ciclo", c.ToString());

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public Cycle GetCycle()
        {
            using (cmd = new SqlCommand(" SELECT TOP(1) * FROM ciclo ", Conex.nomi))
            {

                Conex.OpenNomina();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        return ShowDataGrid(reader);
                    }
                }
            }

            return null;
        }

        private static Cycle ShowDataGrid(SqlDataReader reader)
        {
            return new Cycle
            {
                Id = Convert.ToInt32(reader["id"]),
                _Cycle = Convert.ToString(reader["ciclo"])
            };
        }
    }
}
