using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Incentives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class IncentiveRepository : IIncentive
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int DeleteIncentiveEmployee(int Id)
        {
            string query = $"DELETE FROM estimulo_61 WHERE id = {Id} ";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteIncentivePayWeek(int PayWeek)
        {
            string query = $"DELETE FROM estimulo_61 WHERE semana_pago = {PayWeek} ";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Incentive> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<Incentive> FilterByValue(int PayWeek)
        {
            string column = "es.numero_empleado";
            return ExecuteFilter(column, PayWeek.ToString());
        }

        private List<Incentive> ExecuteFilter(string column, string param)
        {
            List<Incentive> list = new List<Incentive>();

            string query = $@"SELECT es.id,es.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,es.importe,es.semana_pago
                            FROM estimulo_61 es
                            INNER JOIN empleados e ON es.numero_empleado = e.numero_empleado 
                            WHERE {column} LIKE @param";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@param", $"%{param}%");
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
                throw new Exception($"Error al obtener los estimulos: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        public List<Incentive> GetIncentives()
        {
            List<Incentive> list = new List<Incentive>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT es.id,es.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,es.importe,es.semana_pago
                                                FROM estimulo_61 es
                                                INNER JOIN empleados e
                                                ON es.numero_empleado = e.numero_empleado
                                                ORDER BY semana_pago DESC, numero_empleado", Conex.nomi))
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

                throw new Exception("Error al obtener los estimulos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public void ImportEmployees(DataTable dtCsv)
        {
            DataColumn colIdConcept = new DataColumn("id_concepto", typeof(int));
            colIdConcept.DefaultValue = 28;
            dtCsv.Columns.Add(colIdConcept);

            Conex.OpenNomina();

            if (dtCsv == null || dtCsv.Rows.Count == 0)
            {
                throw new Exception("El archivo CSV no contiene datos válidos para importar.");
            }

            using (SqlTransaction trans = Conex.nomi.BeginTransaction())
            {
                try
                {

                    using (SqlBulkCopy bulk = new SqlBulkCopy(Conex.nomi, SqlBulkCopyOptions.Default, trans))
                    {
                        bulk.DestinationTableName = "estimulo_61";
                        bulk.ColumnMappings.Clear();

                        bulk.ColumnMappings.Add("c1", "numero_empleado");
                        bulk.ColumnMappings.Add("c2", "importe");
                        bulk.ColumnMappings.Add("c3", "semana_pago");
                        bulk.ColumnMappings.Add("id_concepto", "id_concepto");

                        bulk.WriteToServer(dtCsv);
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    Conex.CloseNomina();
                }
            }
        }

        private Incentive ShowDataGrid(SqlDataReader reader)
        {
            return new Incentive
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"])
            };
        }
    }
}
