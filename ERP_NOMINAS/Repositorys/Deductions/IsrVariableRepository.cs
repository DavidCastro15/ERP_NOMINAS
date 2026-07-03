using ERP_NOMINAS.Models.Deductions.IsrVariable;
using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Deductions
{
    public class IsrVariableRepository : IIsrV
    {

        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateIsrV(IsrV isrV)
        {
            string query = @"INSERT INTO isr_adicional(id_empleado,importe,subsidio,semana_pago) 
                                                        VALUES(@id_empleado, @importe, @subsidio, @semana_pago)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id_empleado", isrV.NumberEmployee);
                cmd.Parameters.AddWithValue("@importe", isrV.Amount);
                cmd.Parameters.AddWithValue("@subsidio", isrV.Subsidy);
                cmd.Parameters.AddWithValue("@semana_pago", isrV.PayWeek);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteIsrV(int Id)
        {
            string query = $"DELETE FROM isr_adicional WHERE id = {Id} ";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<IsrV> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<IsrV> FilterByValue(int NumberEmployee)
        {
            string column = "isr.id_empleado";
            return ExecuteFilter(column, NumberEmployee.ToString());
        }

        private List<IsrV> ExecuteFilter(string column, string param)
        {
            List<IsrV> list = new List<IsrV>();

            string query = $@"SELECT isr.id,isr.id_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,isr.importe,isr.subsidio,isr.semana_pago
                              FROM isr_adicional isr
                              INNER JOIN empleados e
                              ON isr.id_empleado = e.numero_empleado                                                            
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
                throw new Exception($"Error al obtener los empleados: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        public IsrV GetIsr(int Id)
        {
            using (cmd = new SqlCommand(@"SELECT isr.id,isr.id_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,isr.importe,isr.subsidio,isr.semana_pago
                                                              FROM isr_adicional isr
                                                              INNER JOIN empleados e
                                                              ON isr.id_empleado = e.numero_empleado
                                                              WHERE isr.id = @id", Conex.nomi))
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

        public List<IsrV> GetIsrVs()
        {
            List<IsrV> list = new List<IsrV>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT isr.id,isr.id_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,isr.importe,isr.subsidio,isr.semana_pago
                                                              FROM isr_adicional isr
                                                              INNER JOIN empleados e
                                                              ON isr.id_empleado = e.numero_empleado
                                                              ORDER BY isr.semana_pago DESC", Conex.nomi))
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

                throw new Exception("Error al obtener los empleados: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public void ImportEmployees(DataTable dtCsv)
        {
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
                        bulk.DestinationTableName = "isr_adicional";
                        // LÍMPIA CUALQUIER MAPEO PREVIO
                        bulk.ColumnMappings.Clear();

                        // MAPEO EXPLÍCITO: (Nombre en el DataTable, Nombre exacto en la Tabla SQL)
                        // El primer parámetro es el nombre que le diste en dt.Columns.Add("...")
                        // El segundo parámetro es el nombre real de la columna en tu base de datos
                        bulk.ColumnMappings.Add("c1", "id_empleado");
                        bulk.ColumnMappings.Add("c2", "importe");
                        bulk.ColumnMappings.Add("c3", "subsidio");
                        bulk.ColumnMappings.Add("c4", "semana_pago");

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

        public int UpdateIsrV(IsrV isrV)
        {
            string query = @"UPDATE isr_adicional SET importe=@importe,subsidio=@subsidio,semana_pago=@semana_pago WHERE id = @id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", isrV.Id);
                cmd.Parameters.AddWithValue("@importe", isrV.Amount);
                cmd.Parameters.AddWithValue("@subsidio", isrV.Subsidy);
                cmd.Parameters.AddWithValue("@semana_pago", isrV.PayWeek);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private IsrV ShowDataGrid(SqlDataReader reader)
        {
            return new IsrV
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["id_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                Subsidy = Convert.ToDecimal(reader["subsidio"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"])
            };
        }
    }
}
