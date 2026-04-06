using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.IntegratedSalaries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class IntegratedSalaryRepository : IIntegratedSalary
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateIntegratedSalary(IntegratedSalary i)
        {
            string query = " INSERT INTO salarios_integrados(id_empleado,salario_integrado_imss,salario_integrado_infonavit) " +
                           " VALUES(@id_empleado, @salario_integrado_imss, @salario_integrado_infonavit) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id_empleado", i.NumberEmployee);
                cmd.Parameters.AddWithValue("@salario_integrado_imss", i.SalaryIntegratedImss);
                cmd.Parameters.AddWithValue("@salario_integrado_infonavit", i.SalaryIntegratedInfonavit);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteIntegratedSalary(int Id)
        {
            string query = $"DELETE FROM salarios_integrados WHERE id = {Id} ";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int StatusEmployee(bool State, int Id)
        {
            string origen = State ? "salarios_integrados" : "omitir_imss";
            string destino = State ? "omitir_imss" : "salarios_integrados";

            string query = $@"
    MERGE INTO [{destino}] AS destino 
    USING (SELECT * FROM [{origen}] WHERE id_empleado = @id_empleado) AS origen
    ON (destino.id_empleado = origen.id_empleado) 
    WHEN MATCHED THEN 
        UPDATE SET 
            destino.salario_integrado_imss = origen.salario_integrado_imss, 
            destino.salario_integrado_infonavit = origen.salario_integrado_infonavit 
    WHEN NOT MATCHED THEN 
        INSERT (id_empleado, salario_integrado_imss, salario_integrado_infonavit) 
        VALUES (origen.id_empleado, origen.salario_integrado_imss, origen.salario_integrado_infonavit); -- Punto y coma obligatorio aquí

    DELETE FROM [{origen}] WHERE id_empleado = @id_empleado;";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@origen", origen);
                cmd.Parameters.AddWithValue("@destino", destino);
                cmd.Parameters.AddWithValue("@id_empleado", Id);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<IntegratedSalary> FilterByNumberEmployee(int NumberEmployee)
        {
            List<IntegratedSalary> list = new List<IntegratedSalary>();

            try
            {

                using (cmd = new SqlCommand(" SELECT s.id, s.id_empleado, s.salario_integrado_imss, " +
                       " s.salario_integrado_infonavit, concat(e.nombre,' ',e.apellido_paterno,' ', e.apellido_materno) as n_completo " +
                       " FROM empleados e " +
                       " INNER JOIN salarios_integrados s ON e.numero_empleado = s.id_empleado " +
                       " WHERE id_empleado LIKE '%" + NumberEmployee + @"%'" +
                       " ORDER BY s.id_empleado", Conex.nomi))
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

                throw new Exception("Error al obtener los salarios integrados: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<IntegratedSalary> GetIntegratedSalaries()
        {
            List<IntegratedSalary> list = new List<IntegratedSalary>();

            try
            {

                using (cmd = new SqlCommand("SELECT s.id, s.id_empleado, s.salario_integrado_imss, " +
                       "s.salario_integrado_infonavit, concat(e.nombre,' ',e.apellido_paterno,' ', e.apellido_materno) as n_completo " +
                       "FROM empleados e " +
                       "INNER JOIN salarios_integrados s ON e.numero_empleado = s.id_empleado " +
                       "ORDER BY s.id_empleado", Conex.nomi))
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

                throw new Exception("Error al obtener los salarios integrados: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public void ImportEmployees(DataTable dtCsv)
        {
            ConnectorSql db = new ConnectorSql();
            db.OpenNomina(); // Abre la conexión 'nomi'


            // VALIDACIÓN PREVIA: Si el DataTable no tiene filas, abortamos antes de tocar la BD
            if (dtCsv == null || dtCsv.Rows.Count == 0)
                {
                    throw new Exception("El archivo CSV no contiene datos válidos para importar.");
                }

            
            using (SqlTransaction trans = db.nomi.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE salarios_integrados", db.nomi, trans))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlBulkCopy bulk = new SqlBulkCopy(db.nomi, SqlBulkCopyOptions.Default, trans))
                    {
                        bulk.DestinationTableName = "salarios_integrados";
                        // LÍMPIA CUALQUIER MAPEO PREVIO
                        bulk.ColumnMappings.Clear();

                        // MAPEO EXPLÍCITO: (Nombre en el DataTable, Nombre exacto en la Tabla SQL)
                        // El primer parámetro es el nombre que le diste en dt.Columns.Add("...")
                        // El segundo parámetro es el nombre real de la columna en tu base de datos
                        bulk.ColumnMappings.Add("c1", "id_empleado");
                        bulk.ColumnMappings.Add("c2", "salario_integrado_imss");
                        bulk.ColumnMappings.Add("c3", "salario_integrado_infonavit");

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
                    db.CloseNomina();
                }
            }


        }

        public int UpdateIntegratedSalary(IntegratedSalary i)
        {
            string query = "UPDATE salarios_integrados SET salario_integrado_imss=@salario_integrado_imss,salario_integrado_infonavit=@salario_integrado_infonavit " +
                                      "  WHERE id_empleado = @id_empleado";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id_empleado", i.NumberEmployee);
                cmd.Parameters.AddWithValue("@salario_integrado_imss", i.SalaryIntegratedImss);
                cmd.Parameters.AddWithValue("@salario_integrado_infonavit", i.SalaryIntegratedInfonavit);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private static IntegratedSalary ShowDataGrid(SqlDataReader reader)
        {
            bool exists = Enumerable.Range(0, reader.FieldCount).Any(i => reader.GetName(i) == "n_completo");
            return new IntegratedSalary
            {
                
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["id_empleado"]),
                NameEmployee = Convert.ToString(exists && reader["n_completo"] != DBNull.Value ? reader["n_completo"] : ""  ),
                SalaryIntegratedImss= Convert.ToDecimal(reader["salario_integrado_imss"]),
                SalaryIntegratedInfonavit = Convert.ToDecimal(reader["salario_integrado_infonavit"])
            };
        }

        public IntegratedSalary GetIntegratedSalary(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM salarios_integrados WHERE id = @id", Conex.nomi))
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

        public List<IntegratedSalary> GetSkipIntegratedSalaries()
        {
            List<IntegratedSalary> list = new List<IntegratedSalary>();

            try
            {

                using (cmd = new SqlCommand("SELECT s.id, s.id_empleado, s.salario_integrado_imss, " +
                       "s.salario_integrado_infonavit, concat(e.nombre,' ',e.apellido_paterno,' ', e.apellido_materno) as n_completo " +
                       "FROM empleados e " +
                       "INNER JOIN omitir_imss s ON e.numero_empleado = s.id_empleado " +
                       "ORDER BY s.id_empleado", Conex.nomi))
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

                throw new Exception("Error al obtener los salarios integrados: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }
    }
}
