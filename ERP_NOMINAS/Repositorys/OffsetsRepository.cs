using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Offsets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class OffsetsRepository : IOffset
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public List<Offset> FilterByValue(string name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, name);
        }

        public List<Offset> FilterByValue(int payWeek)
        {
            string column = "c.periodo";
            return ExecuteFilter(column, payWeek.ToString());
        }

        private List<Offset> ExecuteFilter(string column, string param)
        {
            List<Offset> list = new List<Offset>();

            string query = $@"SELECT c.id, c.id_nomina, c.numero_empleado, 
                    CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) as n_completo, 
                    c.importe, c.ciclo, c.periodo, c.uso, c.comentarios, c.id_concepto 
                    FROM compensaciones c 
                    INNER JOIN empleados e ON c.numero_empleado = e.numero_empleado 
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
                throw new Exception("Error al obtener las compensaciones: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        public List<Offset> GetOffsets()
        {
            List<Offset> list = new List<Offset>();

            try
            {

                using (cmd = new SqlCommand("SELECT TOP(500) c.id,c.id_nomina,c.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,c.importe,c.ciclo,c.periodo,c.uso,c.comentarios,c.id_concepto " +
                                                "FROM compensaciones c " +
                                                "INNER JOIN empleados e " +
                                                "ON c.numero_empleado = e.numero_empleado " +
                                                "ORDER BY c.periodo DESC", Conex.nomi))
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

                throw new Exception("Error al obtener las compensaciones: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Offset GetOffsetEmployeeDetail(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM compensaciones WHERE id = @id", Conex.nomi))
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

        public int CreateOffset(Offset o)
        {
            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();
            try
            {

                string query = "INSERT INTO compensaciones(id_nomina,numero_empleado,importe,ciclo,periodo,uso,comentarios,id_concepto)" +
                                        "VALUES(@id_nomina,@numero_empleado,@importe,@ciclo,@periodo,@uso,@comentarios,@id_concepto)";

                foreach (var det in o.Details)
                {
                    using (cmd = new SqlCommand(query, Conex.nomi, tra))
                    {
                        cmd.Parameters.AddWithValue("@id_nomina", o.PayrollId);
                        cmd.Parameters.AddWithValue("@numero_empleado", det.NumberEmployee);
                        cmd.Parameters.AddWithValue("@importe", det.Amount);
                        cmd.Parameters.AddWithValue("@ciclo", o.Cycle);
                        cmd.Parameters.AddWithValue("@periodo", o.PayWeek);
                        cmd.Parameters.AddWithValue("@uso", det.Use);
                        cmd.Parameters.AddWithValue("@comentarios", det.Comments);
                        cmd.Parameters.AddWithValue("@id_concepto", o.IdConcept);
                        cmd.ExecuteNonQuery();
                    }
                }

                tra.Commit();
                return 1; // Éxito
            }
            catch (Exception)
            {
                tra.Rollback();
                throw;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        public int UpdateOffsetEmployeeDetail(Offset o)
        {
            string query = "UPDATE compensaciones SET id_nomina=@id_nomina,numero_empleado=@numero_empleado,importe=@importe,ciclo=@ciclo,periodo=@periodo,uso=@uso,comentarios=@comentarios,id_concepto=@id_concepto "+
                                                          " WHERE id = @id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", o.Id);
                cmd.Parameters.AddWithValue("@id_nomina", o.PayrollId);
                cmd.Parameters.AddWithValue("@numero_empleado", o.NumberEmployee);
                cmd.Parameters.AddWithValue("@importe", o.Amount);
                cmd.Parameters.AddWithValue("@ciclo", o.Cycle);
                cmd.Parameters.AddWithValue("@periodo", o.PayWeek);
                cmd.Parameters.AddWithValue("@uso", o.Use);
                cmd.Parameters.AddWithValue("@comentarios", o.Comments);
                cmd.Parameters.AddWithValue("@id_concepto", o.IdConcept);
                cmd.ExecuteNonQuery();

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteOffset(int Id)
        {
            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                // 2. IMPORTANTE: Usa parámetros para evitar Inyección SQL
                string query = "DELETE FROM compensaciones WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi, tra))
                {
                    cmd.Parameters.AddWithValue("@id", Id);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // 3. Confirmar la operación
                    tra.Commit();
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                tra.Rollback();
                throw;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        private Offset ShowDataGrid(SqlDataReader reader)
        {
            bool HasColumn(string name)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).Equals(name, StringComparison.OrdinalIgnoreCase)) return true;
                }
                return false;
            }

            return new Offset
            {
                Id = reader["id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id"]),
                PayrollId = reader["id_nomina"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id_nomina"]),
                NumberEmployee = reader["numero_empleado"] == DBNull.Value ? 0 : Convert.ToInt32(reader["numero_empleado"]),
                FullName = HasColumn("n_completo") ? (reader["n_completo"]?.ToString() ?? "") : "Sin nombre",
                Amount = reader["importe"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["importe"]),
                Cycle = reader["ciclo"]?.ToString() ?? "",
                PayWeek = reader["periodo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["periodo"]),
                Use = reader["uso"] == DBNull.Value ? 0 : Convert.ToInt32(reader["uso"]),
                Comments = reader["comentarios"]?.ToString() ?? "",
                IdConcept = reader["id_concepto"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id_concepto"])
            };
        }


    }


}
