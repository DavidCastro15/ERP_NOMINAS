using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Subsidies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class SubsidyRepository : ISubsidy
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateSubsidy(Subsidy s)
        {
            string query = "INSERT INTO subsidios(id_nomina,periodo,semana_pago,numero_empleado,importe,uso,id_percepcion,comentarios) "+
                                                           "VALUES(@id_nomina, @periodo, @semana_pago, @numero_empleado, @importe, @uso, @id_percepcion, @comentarios)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@id_nomina", s.PayrollId);
                cmd.Parameters.AddWithValue("@periodo",s.Cycle);
                cmd.Parameters.AddWithValue("@semana_pago", s.PayWeek);
                cmd.Parameters.AddWithValue("@numero_empleado", s.NumberEmployee);
                cmd.Parameters.AddWithValue("@importe", s.Amount);
                cmd.Parameters.AddWithValue("@uso", s.Use);
                cmd.Parameters.AddWithValue("@comentarios", s.Comments);
                cmd.Parameters.AddWithValue("@id_percepcion", s.ConceptId);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteSubidy(int Id)
        {

            Conex.OpenNomina();

            SqlTransaction tra = Conex.nomi.BeginTransaction();
            string query = "DELETE FROM subsidios WHERE id = @Id";
            try
            {
                using (cmd = new SqlCommand(query, Conex.nomi, tra))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }

                tra.Commit();
                return 1;
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

        public List<Subsidy> FilterByValue(int pw)
        {
            string column = "s.semana_pago";
            return ExecuteFilter(column, pw.ToString());
        }

        public List<Subsidy> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        private List<Subsidy> ExecuteFilter(string column, string param)
        {
            List<Subsidy> list = new List<Subsidy>();

            string query = $@"SELECT s.id,s.id_nomina,s.periodo,s.semana_pago,s.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,s.importe,s.uso,s.id_percepcion,s.comentarios
                                                            FROM subsidios s
                                                            INNER JOIN empleados e
                                                            ON e.numero_empleado = s.numero_empleado
                                                            WHERE {column} LIKE @param 
                                                            ORDER BY s.semana_pago,s.numero_empleado DESC";

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
                throw new Exception($"Error al obtener los subsidios: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        public List<Subsidy> GetSubsidies()
        {
            List<Subsidy> list = new List<Subsidy>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT s.id,s.id_nomina,s.periodo,s.semana_pago,s.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,s.importe,s.uso,s.id_percepcion,s.comentarios
                                                            FROM subsidios s
                                                            INNER JOIN empleados e
                                                            ON e.numero_empleado = s.numero_empleado
                                                            ORDER BY s.id DESC", Conex.nomi))
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

                throw new Exception($"Error al obtener los subsidios: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Subsidy GetSubsidy(int Id)
        {
            using (cmd = new SqlCommand(@"SELECT s.id,s.id_nomina,s.periodo,s.semana_pago,s.numero_empleado,CONCAT(e.nombre,e.apellido_paterno,e.apellido_materno) AS n_completo,s.importe,s.uso,s.id_percepcion,s.comentarios
                                        FROM subsidios s
                                        INNER JOIN empleados e
                                        ON s.numero_empleado = e.numero_empleado
                                        WHERE s.id = @id", Conex.nomi))
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

        public List<SubsidyReport> ShowDataReport(int pw)
        {
            List<SubsidyReport> list = new List<SubsidyReport>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT s.id,s.id_nomina,s.periodo,s.semana_pago,s.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,s.importe,s.uso,s.id_percepcion,s.comentarios
                                                            FROM subsidios s
                                                            INNER JOIN empleados e
                                                            ON e.numero_empleado = s.numero_empleado
                                                            WHERE s.semana_pago = @semana_pago
                                                            ORDER BY s.numero_empleado ASC", Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@semana_pago", pw);
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowDataReport(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al obtener los subsidios: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateSubsidy(Subsidy s)
        {
            string query = @"UPDATE subsidios SET periodo=@periodo,semana_pago=@semana_pago,importe=@importe,uso=@uso,comentarios=@comentarios
                             WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", s.Id);
                cmd.Parameters.AddWithValue("@id_nomina", s.PayrollId);
                cmd.Parameters.AddWithValue("@periodo", s.Cycle);
                cmd.Parameters.AddWithValue("@semana_pago", s.PayWeek);
                cmd.Parameters.AddWithValue("@importe", s.Amount);
                cmd.Parameters.AddWithValue("@uso", s.Use);
                cmd.Parameters.AddWithValue("@comentarios", s.Comments);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        private Subsidy ShowDataGrid(SqlDataReader reader)
        {
            return new Subsidy
            {
                Id = Convert.ToInt32(reader["id"]),
                PayrollId = Convert.ToInt32(reader["id_nomina"]),
                Cycle = Convert.ToString(reader["periodo"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                Use = reader["uso"] == DBNull.Value ? 0 : Convert.ToInt32(reader["uso"]),
                Comments = Convert.ToString(reader["comentarios"]),
                ConceptId = Convert.ToInt32(25)

            };
        }

        private SubsidyReport ShowDataReport(SqlDataReader reader)
        {
            return new SubsidyReport
            {

                Cycle = Convert.ToString(reader["periodo"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                Use = reader["uso"] == DBNull.Value ? 0 : Convert.ToInt32(reader["uso"]),
                Comments = Convert.ToString(reader["comentarios"]),
            };
        }
    }
}
