using ERP_NOMINAS.Models.Deductions.DiscountGlobal;
using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Earnings
{
    public class DiscountGlobalRepository : IDGlobal,IISkipDGlobal
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateDGlobal(DGlobal d)
        {
            string query = @"INSERT INTO descuentos_globales(ciclo,no_periodo,id_concepto,eventual,temporal,permanente,importe,descripcion_descuento) 
                                                      VALUES(@ciclo,@no_periodo,@id_concepto,@eventual,@temporal,@permanente,@importe,@descripcion_descuento)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@ciclo", d.Cycle);
                cmd.Parameters.AddWithValue("@no_periodo", d.PayWeek);
                cmd.Parameters.AddWithValue("@id_concepto", d.IdConcept);
                cmd.Parameters.AddWithValue("@eventual", d.Occasional);
                cmd.Parameters.AddWithValue("@temporal", d.Temporary);
                cmd.Parameters.AddWithValue("@permanente", d.Permanent);
                cmd.Parameters.AddWithValue("@importe", d.Amount);
                cmd.Parameters.AddWithValue("@descripcion_descuento", d.Description);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteDGlobal(int Id)
        {
            string query = "DELETE FROM descuentos_globales WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteSkipDGlobaEmployee(int Id)
        {
            string query = "DELETE FROM omitir_descuento_global WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<DGlobal> FilterByConcept(int IdConcept)
        {
            List<DGlobal> list = new List<DGlobal>();

            string query = $@"SELECT * FROM descuentos_globales WHERE id_concepto LIKE @param  ORDER BY id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@param", $"%{IdConcept}%");
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
                throw new Exception($"Error al obtener los descuentos: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        public List<SkipDGlobal> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<SkipDGlobal> FilterByValue(int NumberEmployee)
        {
            string column = "s.numero_empleado";
            return ExecuteFilter(column, NumberEmployee.ToString());
        }

        private List<SkipDGlobal> ExecuteFilter(string column, string param)
        {
            List<SkipDGlobal> list = new List<SkipDGlobal>();

            string query = $@"SELECT s.id,s.numero_empleado,concat(e.nombre,' ',e.apellido_paterno,' ', e.apellido_materno) as n_completo  
                                            FROM empleados e
                                            INNER JOIN omitir_descuento_global s
                                            ON e.numero_empleado = s.numero_empleado
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
                            list.Add(ShowDataGridSkipGlobal(reader));
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

        public DGlobal GetDGlobal(int Id)
        {
            using (cmd = new SqlCommand($@"SELECT * FROM descuentos_globales WHERE id = @id", Conex.nomi))
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

        public List<DGlobal> GetDGlobals()
        {
            List<DGlobal> list = new List<DGlobal>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT * FROM descuentos_globales ORDER BY no_periodo DESC", Conex.nomi))
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

                throw new Exception("Error al obtener los descuentos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;

        }

        public List<SkipDGlobal> GetSkipDGlobals()
        {
            List<SkipDGlobal> list = new List<SkipDGlobal>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT s.id,s.numero_empleado,concat(e.nombre,' ',e.apellido_paterno,' ', e.apellido_materno) as n_completo  
                                            FROM empleados e
                                            INNER JOIN omitir_descuento_global s
                                            ON e.numero_empleado = s.numero_empleado order by s.numero_empleado", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowDataGridSkipGlobal(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los descuentos: " + ex.Message);
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
                    using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE omitir_descuento_global", Conex.nomi, trans))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlBulkCopy bulk = new SqlBulkCopy(Conex.nomi, SqlBulkCopyOptions.Default, trans))
                    {
                        bulk.DestinationTableName = "omitir_descuento_global";
                        // LÍMPIA CUALQUIER MAPEO PREVIO
                        bulk.ColumnMappings.Clear();

                        bulk.ColumnMappings.Add("c1", "numero_empleado");

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

        public int UpdateDGlobal(DGlobal d)
        {
            string query = @"UPDATE descuentos_globales SET ciclo=@ciclo,no_periodo=@no_periodo,id_concepto=@id_concepto,eventual=@eventual,temporal=@temporal,permanente=@permanente,importe=@importe,descripcion_descuento=@descripcion_descuento
                                                           WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", d.Id);
                cmd.Parameters.AddWithValue("@ciclo", d.Cycle);
                cmd.Parameters.AddWithValue("@no_periodo", d.PayWeek);
                cmd.Parameters.AddWithValue("@id_concepto", d.IdConcept);
                cmd.Parameters.AddWithValue("@eventual", d.Occasional);
                cmd.Parameters.AddWithValue("@temporal", d.Temporary);
                cmd.Parameters.AddWithValue("@permanente", d.Permanent);
                cmd.Parameters.AddWithValue("@importe", d.Amount);
                cmd.Parameters.AddWithValue("@descripcion_descuento", d.Description);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private DGlobal ShowDataGrid(SqlDataReader reader)
        {
            return new DGlobal
            {
                Id = Convert.ToInt32(reader["id"]),
                Cycle = Convert.ToString(reader["ciclo"]),
                PayWeek = Convert.ToInt32(reader["no_periodo"]),
                IdConcept = Convert.ToInt32(reader["id_concepto"]),
                Occasional = Convert.ToByte(reader["eventual"]),
                Temporary = Convert.ToByte(reader["temporal"]),
                Permanent = Convert.ToByte(reader["permanente"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                Description = Convert.ToString(reader["descripcion_descuento"])
            };
        }

        private SkipDGlobal ShowDataGridSkipGlobal(SqlDataReader reader)
        {
            return new SkipDGlobal
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"])
                
            };
        }
    }
}
