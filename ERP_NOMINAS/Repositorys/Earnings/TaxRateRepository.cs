using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.TaxRates;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class TaxRateRepository : ITaxRate
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();
        public string useTable;

        public int CreateTaxRate(TaxRate t)
        {
            string query =$" insert into {useTable} (limite_inferior,limite_superior,cuota_fija,porcentaje_excedente) "+
                                                     " values(@limite_inferior, @limite_superior, @cuota_fija, @porcentaje_excedente) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@limite_inferior", t.LowerLimit);
                cmd.Parameters.AddWithValue("@limite_superior", t.UpperLimit);
                cmd.Parameters.AddWithValue("@cuota_fija", t.FixedFee);
                cmd.Parameters.AddWithValue("@porcentaje_excedente", t.ExcessPercentage);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteTaxRate(int Id)
        {
            string query = $"DELETE FROM {useTable} WHERE id = {Id}";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }
  
        public TaxRate GetTaxRate(int Id)
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

        public List<TaxRate> GetTaxRates()
        {
            List<TaxRate> list = new List<TaxRate>();

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

        public int UpdateTaxRate(TaxRate t)
        {
            string query = $"UPDATE {useTable} SET limite_inferior=@limite_inferior,limite_superior=@limite_superior,cuota_fija=@cuota_fija,porcentaje_excedente=@porcentaje_excedente "+
                                       " WHERE id = @id ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", t.Id);
                cmd.Parameters.AddWithValue("@limite_inferior", t.LowerLimit);
                cmd.Parameters.AddWithValue("@limite_superior", t.UpperLimit);
                cmd.Parameters.AddWithValue("@cuota_fija", t.FixedFee);
                cmd.Parameters.AddWithValue("@porcentaje_excedente", t.ExcessPercentage);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }
        
        private static TaxRate ShowDataGrid(SqlDataReader reader)
        {
            return new TaxRate
            {
                Id = Convert.ToInt32(reader["id"]),
                LowerLimit = Convert.ToDecimal(reader["limite_inferior"]),
                UpperLimit = Convert.ToDecimal(reader["limite_superior"]),
                FixedFee = Convert.ToDecimal(reader["cuota_fija"]),
                ExcessPercentage = Convert.ToDecimal(reader["porcentaje_excedente"])
            };
        }
    }
}
