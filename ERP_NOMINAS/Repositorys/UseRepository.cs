using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Uses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class UseRepository : IUse
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateUse(Use u)
        {
            string query = "INSERT INTO usos(uso,descripcion,gerencia,departamento,grupos,equipo,cuenta_contable) " +
                                     "VALUES(@uso, @descripcion, @gerencia, @departamento, @grupos, @equipo, @cuenta_contable)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@uso", u._Use);
                cmd.Parameters.AddWithValue("@descripcion", u.Description);
                cmd.Parameters.AddWithValue("@gerencia", u.IdManagment);
                cmd.Parameters.AddWithValue("@departamento", u.IdDepartment);
                cmd.Parameters.AddWithValue("@grupos", u.Group);
                cmd.Parameters.AddWithValue("@equipo", u.Equipment);
                cmd.Parameters.AddWithValue("@cuenta_contable", u.AccountingAccount);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteUse(int Id)
        {
            string query = $"DELETE FROM usos WHERE id = {Id}";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Use> FilterByUse(int U)
        {
            List<Use> list = new List<Use>();

            try
            {

                using (cmd = new SqlCommand("SELECT u.id,u.uso,u.descripcion,u.gerencia AS id_gerencia,g.nombre AS gerencia,u.departamento AS id_departamento,d.nombre AS departamento,grupos,equipo,cuenta_contable "+
                                                              " FROM usos u " +
                                                              " INNER JOIN gerencias g " +
                                                              " ON g.gerencia = u.gerencia " +
                                                              " INNER JOIN departamentos d " +
                                                              " ON d.id_deparamento = u.departamento " +
                                                              $" WHERE u.uso LIKE '%{U}%'" +
                                                              " ORDER BY u.id", Conex.nomi))
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

                throw new Exception("Error al obtener el uso: " + ex.Message);
            }
            finally
            {
                
                Conex.CloseNomina();
            }

            return list;
        }

        public Use GetUse(int Id)
        {
            using (cmd = new SqlCommand(" SELECT u.id,u.uso,u.descripcion,u.gerencia AS id_gerencia,g.nombre AS gerencia,u.departamento AS id_departamento,d.nombre AS departamento,u.grupos,u.equipo,u.cuenta_contable " +
                                        " FROM usos u " +
                                        " INNER JOIN gerencias g " +
                                        " ON g.gerencia = u.gerencia " +
                                        " INNER JOIN departamentos d " +
                                        " ON d.id_deparamento = u.departamento " +
                                        " WHERE u.id = @id", Conex.nomi))
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

        public List<Use> GetUses()
        {
            List<Use> list = new List<Use>();

            try
            {

                using (cmd = new SqlCommand("SELECT u.id,u.uso,u.descripcion,u.gerencia AS id_gerencia,g.nombre AS gerencia,u.departamento AS id_departamento,d.nombre AS departamento,grupos,equipo,cuenta_contable " +
                                                              " FROM usos u " +
                                                              " INNER JOIN gerencias g " +
                                                              " ON g.gerencia = u.gerencia " +
                                                              " INNER JOIN departamentos d " +
                                                              " ON d.id_deparamento = u.departamento " +                                                            
                                                              " ORDER BY u.id", Conex.nomi))
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

                throw new Exception("Error al obtener el uso: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateUse(Use u)
        {
            string query = "UPDATE usos SET uso=@uso,descripcion=@descripcion,gerencia=@gerencia," +
                           "departamento=@departamento,grupos=@grupos,equipo=@equipo,cuenta_contable=@cuenta_contable " +
                           "WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", u.Id);
                cmd.Parameters.AddWithValue("@uso", u._Use);
                cmd.Parameters.AddWithValue("@descripcion", u.Description);
                cmd.Parameters.AddWithValue("@gerencia", u.IdManagment);
                cmd.Parameters.AddWithValue("@departamento", u.IdDepartment);
                cmd.Parameters.AddWithValue("@grupos", u.Group);
                cmd.Parameters.AddWithValue("@equipo", u.Equipment);
                cmd.Parameters.AddWithValue("@cuenta_contable", u.AccountingAccount);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private Use ShowDataGrid(SqlDataReader reader)
        {
            return new Use
            {
                Id = Convert.ToInt32(reader["id"]),
                _Use = Convert.ToInt32(reader["uso"]),
                Description = Convert.ToString(reader["descripcion"]),
                IdManagment = Convert.ToInt32(reader["id_gerencia"]),
                NameManagement = Convert.ToString(reader["gerencia"]),
                IdDepartment= Convert.ToInt32(reader["id_departamento"]),
                NameDepartment = Convert.ToString(reader["departamento"]),
                Group = Convert.ToInt32(reader["grupos"]),
                Equipment = Convert.ToInt32(reader["equipo"]),
                AccountingAccount = Convert.ToString(reader["cuenta_contable"]),
              
            };
        }
    }
}
