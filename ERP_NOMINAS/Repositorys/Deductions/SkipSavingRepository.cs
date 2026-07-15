using ERP_NOMINAS.Models.Deductions.SkipSaving;
using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Deductions
{
    public class SkipSavingRepository : ISSaving
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateSaving(SSaving s)
        {
            string query = @"INSERT INTO omitir_ahorro_voluntario_10(numero_empleado) 
                                                      VALUES(@numero_empleado)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@numero_empleado", s.NumberEmployee);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteSaving(int Id)
        {
            string query = $"DELETE FROM omitir_ahorro_voluntario_10 WHERE id = {Id} ";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<SSaving> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<SSaving> FilterByValue(int NumberEmployee)
        {
            string column = "om.numero_empleado";
            return ExecuteFilter(column, NumberEmployee.ToString());
        }

        private List<SSaving> ExecuteFilter(string column, string param)
        {
            List<SSaving> list = new List<SSaving>();

            string query = $@"SELECT om.id,om.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',apellido_materno) AS n_completo
                                                            FROM omitir_ahorro_voluntario_10 om
                                                            INNER JOIN empleados e
                                                            ON e.numero_empleado = om.numero_empleado                                                           
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

        public SSaving GetSaving(int Id)
        {
            using (cmd = new SqlCommand(@"SELECT om.id,om.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',apellido_materno) AS n_completo
                                                            FROM omitir_ahorro_voluntario_10 om
                                                            INNER JOIN empleados e
                                                            ON e.numero_empleado = om.numero_empleado
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

        public List<SSaving> GetSavings()
        {
            List<SSaving> list = new List<SSaving>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT om.id,om.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',apellido_materno) AS n_completo
                                                            FROM omitir_ahorro_voluntario_10 om
                                                            INNER JOIN empleados e
                                                            ON e.numero_empleado = om.numero_empleado
                                                            WHERE e.nombre <> '' AND e.numero_empleado <> 42 AND e.estatus = 'Activo'
                                                            ORDER BY om.numero_empleado", Conex.nomi))
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

        private SSaving ShowDataGrid(SqlDataReader reader)
        {
            return new SSaving
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"])
            };
        }

        public int CheckExists(int Id)
        {
            using (cmd = new SqlCommand($"SELECT COUNT(numero_empleado) FROM omitir_ahorro_voluntario_10 WHERE numero_empleado ={Id}", Conex.nomi))             
            {
                Conex.OpenNomina();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
