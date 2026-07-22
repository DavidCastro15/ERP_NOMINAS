using ERP_NOMINAS.Models.Deductions.ReduceDayImss;
using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Deductions
{
    public class RDImssRepository : IRDImss
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public int CreateRDImss(RDImss rd)
        {
            string query = @"INSERT INTO dias_rest(numero_empleado,dias) VALUES(@numero_empleado,@dias)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@numero_empleado", rd.NumberEmployee);
                cmd.Parameters.AddWithValue("@dias", rd.Day);
 
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteRDImss(int Id)
        {
            string query = "DELETE FROM dias_rest WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<RDImss> FilterByValue(string Name)
        {
            string column = "CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno)";
            return ExecuteFilter(column, Name);
        }

        public List<RDImss> FilterByValue(int NumberEmployee)
        {
            string column = "dre.numero_empleado";
            return ExecuteFilter(column, NumberEmployee.ToString());
        }

        private List<RDImss> ExecuteFilter(string column, string param)
        {
            List<RDImss> list = new List<RDImss>();

            string query = $@"SELECT dre.id,dre.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,dre.dias
                                                              FROM dias_rest dre
                                                              INNER JOIN empleados e
                                                              ON dre.numero_empleado = e.numero_empleado
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

        public List<RDImss> GetRDImss()
        {
            List<RDImss> list = new List<RDImss>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT dre.id,dre.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,dre.dias
                                                              FROM dias_rest dre
                                                              INNER JOIN empleados e
                                                              ON dre.numero_empleado = e.numero_empleado
                                                              ORDER BY dre.numero_empleado", Conex.nomi))
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

        private RDImss ShowDataGrid(SqlDataReader reader)
        {
            return new RDImss
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Day = Convert.ToInt32(reader["dias"])
            };
        }
    }
}
