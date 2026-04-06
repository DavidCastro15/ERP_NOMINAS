using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Groups;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class GroupRepository : IGroup
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateGroup(Group g)
        {
            string query = " INSERT INTO grupos(grupo,descripcion,gerencia,departamento) " +
                                      " VALUES(@grupo, @descripcion, @gerencia, @departamento) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@grupo", g._Group);
                cmd.Parameters.AddWithValue("@descripcion", g.Description);
                cmd.Parameters.AddWithValue("@gerencia", g.ManagmentId);
                cmd.Parameters.AddWithValue("@departamento", g.DepartmentId);


                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteGroup(int Id)
        {
            string query = $"DELETE FROM grupos WHERE id = {Id}";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Group> FilterByGroup(int g=0)
        {

            if (g==0)
            {
                GetGroups();
            }
            List<Group> list = new List<Group>();

            try
            {

                using (cmd = new SqlCommand("SELECT g.id,g.grupo,g.descripcion,g.gerencia AS id_gerencia,ge.nombre AS gerencia,g.departamento AS id_departamento,d.nombre AS departamento " +
                                                            " FROM grupos g " +
                                                            " INNER JOIN gerencias ge " +
                                                            " ON g.gerencia = ge.gerencia " +
                                                            " INNER JOIN departamentos d " +
                                                            " ON g.departamento = d.id_deparamento" +
                                                           $" WHERE g.grupo LIKE '%{g}%'" +
                                                            " ORDER BY g.grupo", Conex.nomi))
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

                throw new Exception("Error al obtener el grupo: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Group GetGroup(int Id)
        {
            using (cmd = new SqlCommand("SELECT g.id,g.grupo,g.descripcion,g.gerencia AS id_gerencia,ge.nombre AS gerencia,g.departamento AS id_departamento,d.nombre AS departamento " +
                                                            " FROM grupos g " +
                                                            " INNER JOIN gerencias ge " +
                                                            " ON g.gerencia = ge.id " +
                                                            " INNER JOIN departamentos d " +
                                                            " ON g.departamento = d.id_deparamento" +
                                                            " WHERE g.id = @id", Conex.nomi))
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

        public List<Group> GetGroups()
        {
            List<Group> list = new List<Group>();

            try
            {

                using (cmd = new SqlCommand("SELECT g.id,g.grupo,g.descripcion,g.gerencia AS id_gerencia,ge.nombre AS gerencia,g.departamento AS id_departamento,d.nombre AS departamento " +
                                                            " FROM grupos g " +
                                                            " INNER JOIN gerencias ge " +
                                                            " ON g.gerencia = ge.gerencia " +
                                                            " INNER JOIN departamentos d " +
                                                            " ON g.departamento = d.id_deparamento" +
                                                            " ORDER BY g.grupo", Conex.nomi))
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

                throw new Exception("Error al obtener el grupo: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateGroup(Group g)
        {
            {
                string query = "UPDATE grupos SET descripcion=@descripcion,gerencia=@gerencia,departamento=@departamento WHERE id=@id";

                using (cmd = new SqlCommand(query, Conex.nomi))
                {

                    cmd.Parameters.AddWithValue("@id", g.Id);                  
                    //cmd.Parameters.AddWithValue("@grupo", g._Group);                  
                    cmd.Parameters.AddWithValue("@descripcion", g.Description);
                    cmd.Parameters.AddWithValue("@gerencia", g.ManagmentId);
                    cmd.Parameters.AddWithValue("@departamento", g.DepartmentId);

                    Conex.OpenNomina();
                    return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
                }
            }
        }

        public bool ExistsGroup(int g)
        {
            using (cmd = new SqlCommand("SELECT 1 grupo FROM grupos WHERE grupo = @gru", Conex.nomi))

            {
                cmd.Parameters.AddWithValue("@gru",g);
                Conex.OpenNomina();
                return cmd.ExecuteScalar() != null;
            }
        }

        private Group ShowDataGrid(SqlDataReader reader)
        {
            return new Group
            {
                Id = Convert.ToInt32(reader["id"]),
                _Group = Convert.ToInt32(reader["grupo"]),
                Description = Convert.ToString(reader["descripcion"]),
                ManagmentId = Convert.ToInt32(reader["id_gerencia"]),
                NameManagment = Convert.ToString(reader["gerencia"]),
                DepartmentId = Convert.ToInt32(reader["id_departamento"]),
                NameDepartment = Convert.ToString(reader["departamento"])

            };
        }
    }
}
