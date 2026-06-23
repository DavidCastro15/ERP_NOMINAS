using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.SubsidyFees;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class SubsidyFeeRepository : ISubsidyFee
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();
        public string useTable;

        public int CreateSubsidyFee(SubsidyFee s)
        {
            string query = $"insert into {useTable} (limite_inferior,limite_superior,subsidio) " +
                                                     " values(@limite_inferior, @limite_superior, @subsidio)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@limite_inferior", s.LowerLimit);
                cmd.Parameters.AddWithValue("@limite_superior", s.UpperLimit);
                cmd.Parameters.AddWithValue("@subsidio", s.Subsidy);
             
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteSubsidyFee(int Id)
        {
            string query = $"DELETE FROM {useTable} WHERE id = {Id}";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public SubsidyFee GetSubsidyFee(int Id)
        {
            using (cmd = new SqlCommand($"SELECT * FROM {useTable} WHERE id = @id", Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", Id);

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

        public List<SubsidyFee> GetSubsidyFees()
        {
            List<SubsidyFee> list = new List<SubsidyFee>();

            try
            {

                using (cmd = new SqlCommand($"SELECT * FROM {useTable} ", Conex.nomi))
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

                throw new Exception("Error al obtener las tarifas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateSubsidyFee(SubsidyFee s)
        {
            string query = $"UPDATE {useTable} SET limite_inferior=@limite_inferior,limite_superior=@limite_superior,subsidio=@subsidio " +
                                       " WHERE id = @id ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", s.Id);
                cmd.Parameters.AddWithValue("@limite_inferior", s.LowerLimit);
                cmd.Parameters.AddWithValue("@limite_superior", s.UpperLimit);
                cmd.Parameters.AddWithValue("@subsidio", s.Subsidy);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private static SubsidyFee ShowDataGrid(SqlDataReader reader)
        {
            return new SubsidyFee
            {
                Id = Convert.ToInt32(reader["id"]),
                LowerLimit = Convert.ToDecimal(reader["limite_inferior"]),
                UpperLimit = Convert.ToDecimal(reader["limite_superior"]),
                Subsidy = Convert.ToDecimal(reader["subsidio"]),

            };
        }
    }
}
