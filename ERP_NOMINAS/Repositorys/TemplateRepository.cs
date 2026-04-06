using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Templates;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    class TemplateRepository : ITemplate
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();
        public string Cycle = "";
       
        public int CreateTemplate(Template t)
        {
            string query = $" INSERT INTO {Cycle} (id_nomina,numero_empleado,categoria,categoria_requerida,turno_periodo,turno_trabajado,uso,estado) " +
                           " VALUES (@id_nomina, @numero_empleado, @categoria, @categoria_requerida, @turno_periodo, @turno_trabajado, @uso, @estado) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id_nomina",t.PayrollId);
                cmd.Parameters.AddWithValue("@numero_empleado", t.NumberEmployee);
                cmd.Parameters.AddWithValue("@categoria", t.Category);
                cmd.Parameters.AddWithValue("@categoria_requerida", t.CategoryRequired);
                cmd.Parameters.AddWithValue("@turno_periodo", t.ShiftPeriod);
                cmd.Parameters.AddWithValue("@turno_trabajado", t.ShiftWorked);
                cmd.Parameters.AddWithValue("@uso", t.Use);
                cmd.Parameters.AddWithValue("@estado", t.Status);
                       
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteTemplate(int Id)
        {
            string query = $"DELETE FROM {Cycle} WHERE id = {Id}";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public bool ExistsId(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Template> FilterByNameEmployee(string Name)
        {
            string filtro = (Name ?? "").Trim();
            List<Template> list = new List<Template>();

            try
            {

                using (cmd = new SqlCommand("SELECT p.id,p.id_nomina,p.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,p.categoria,p.categoria_requerida,p.turno_periodo, p.turno_trabajado, p.uso, p.estado " +
                                            $" FROM {Cycle} p INNER JOIN empleados e" +
                                            $" ON p.numero_empleado = e.numero_empleado WHERE CONCAT(nombre, ' ', apellido_paterno, ' ', apellido_materno) LIKE '%{filtro}%' ORDER BY p.id ASC ", Conex.nomi))
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

                throw new Exception("Error al obtener las plantillas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Template GetTemplate(int Id)
        {

            using (cmd = new SqlCommand("SELECT p.id,p.id_nomina,p.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,p.categoria,p.categoria_requerida,p.turno_periodo, p.turno_trabajado, p.uso, p.estado " +
                                           $" FROM {Cycle} p INNER JOIN empleados e" +
                                           $" ON p.numero_empleado = e.numero_empleado WHERE p.id=@id", Conex.nomi))
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

        public List<Template> GetTemplates()
        {
            List<Template> list = new List<Template>();
            

            try
            {
                
                using ( cmd = new SqlCommand("SELECT p.id,p.id_nomina,p.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,p.categoria,p.categoria_requerida,p.turno_periodo, p.turno_trabajado, p.uso, p.estado " +
                                            $" FROM {Cycle} p INNER JOIN empleados e" +
                                            " ON p.numero_empleado = e.numero_empleado ORDER BY p.id ASC ", Conex.nomi))
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
               
                throw new Exception("Error al obtener las plantillas: " + ex.Message);
            }
            finally
            {
            
                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateTemplate(Template t)
        {
            string query = $" UPDATE {Cycle} SET categoria=@categoria,categoria_requerida=@categoria_requerida,turno_periodo=@turno_periodo, " +
                                                                    " turno_trabajado = @turno_trabajado,uso = @uso,estado = @estado WHERE id = @id ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id", t.Id);
                //cmd.Parameters.AddWithValue("@id_nomina", t.PayrollId);
                //cmd.Parameters.AddWithValue("@numero_empleado", t.NumberEmployee);
                cmd.Parameters.AddWithValue("@categoria", t.Category);
                cmd.Parameters.AddWithValue("@categoria_requerida", t.CategoryRequired);
                cmd.Parameters.AddWithValue("@turno_periodo", t.ShiftPeriod);
                cmd.Parameters.AddWithValue("@turno_trabajado", t.ShiftWorked);
                cmd.Parameters.AddWithValue("@uso", t.Use);
                cmd.Parameters.AddWithValue("@estado", t.Status);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        private static Template ShowDataGrid(SqlDataReader reader)
        {
            return new Template
            {
               Id = Convert.ToInt32(reader["id"]),
               PayrollId = Convert.ToInt32(reader["id_nomina"]),
               NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
               NameEmployee = Convert.ToString(reader["n_completo"]),
               Category = Convert.ToInt32(reader["categoria"]),
               CategoryRequired = Convert.ToInt32(reader["categoria_requerida"]),
               ShiftPeriod = Convert.ToInt32(reader["turno_periodo"]),
               ShiftWorked = Convert.ToInt32(reader["turno_trabajado"]),
               Use = Convert.ToInt32(reader["uso"]),
               Status = Convert.ToString(reader["estado"])
               
            };
        }
    }
}
