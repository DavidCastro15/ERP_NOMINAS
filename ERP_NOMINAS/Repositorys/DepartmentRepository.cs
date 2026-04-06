using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Departments;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class DepartmentRepository : IDepartment
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CheckNextId()
        {
            using (cmd = new SqlCommand("SELECT ISNULL(MAX(id_deparamento), 0) + 1 FROM departamentos", Conex.nomi))

            {
                Conex.OpenNomina();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CreateDepartment(Department d)
        {
            string query = "INSERT INTO departamentos(id_gerencia,id_deparamento,nombre,responsable) " +
                           "VALUES(@id_gerencia,@id_deparamento,@nombre,@responsable)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id_gerencia", d.ManagmentId);
                cmd.Parameters.AddWithValue("@id_deparamento", d.DepartmentId);
                cmd.Parameters.AddWithValue("@nombre", d.NameDepartment);
                cmd.Parameters.AddWithValue("@responsable", d.Responsible);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteDepartment(int Id)
        {
            string query = "DELETE FROM departamentos WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Department> FilterByName(string Name)
        {
            List<Department> list = new List<Department>();

            try
            {

                using (cmd = new SqlCommand(" SELECT d.id,d.id_deparamento,d.nombre,d.responsable,d.id_gerencia,g.nombre AS nombre_g " +
                                                              " FROM departamentos d" +
                                                              " INNER JOIN gerencias g" +
                                                              " ON d.id_gerencia = g.gerencia " +
                                                              " WHERE d.nombre LIKE '%" + Name + @"%'", Conex.nomi))
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

                throw new Exception("Error al obtener los departamentos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Department GetDepartment(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM departamentos WHERE id = @id", Conex.nomi))
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

        public List<Department> GetDepartments()
        {
            List<Department> list = new List<Department>();

            try
            {

                using (cmd = new SqlCommand(" SELECT d.id,d.id_deparamento,d.nombre,d.responsable,d.id_gerencia,g.nombre AS nombre_g "+
                                                              " FROM departamentos d" +
                                                              " INNER JOIN gerencias g" +
                                                              " ON d.id_gerencia = g.gerencia ", Conex.nomi))
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

                throw new Exception("Error al obtener los departamentos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateDepartment(Department d)
        {
            string query = "UPDATE departamentos SET id_gerencia=@id_gerencia,nombre=@nombre,responsable=@responsable WHERE id_deparamento=@id_deparamento";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", d.Id);
                cmd.Parameters.AddWithValue("@id_gerencia", d.ManagmentId);
                cmd.Parameters.AddWithValue("@id_deparamento", d.DepartmentId);
                cmd.Parameters.AddWithValue("@nombre", d.NameDepartment);
                cmd.Parameters.AddWithValue("@responsable", d.Responsible);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private static Department ShowDataGrid(SqlDataReader reader)
        {
            bool exists = Enumerable.Range(0, reader.FieldCount).Any(i => reader.GetName(i) == "nombre_g");
            return new Department
            {
                Id = Convert.ToInt32(reader["id"]),
                DepartmentId = Convert.ToInt32(reader["id_deparamento"]),
                NameDepartment = Convert.ToString(reader["nombre"]),
                Responsible = Convert.ToString(reader["responsable"]),
                ManagmentId = Convert.ToInt32(reader["id_gerencia"]),
                NameManagment = Convert.ToString(exists && reader["nombre_g"] != DBNull.Value ? reader["nombre_g"] : "")            
                
            };
        }
    }
}
