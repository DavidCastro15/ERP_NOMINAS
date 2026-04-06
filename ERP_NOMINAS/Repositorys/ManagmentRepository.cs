using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Managments;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class ManagmentRepository : IManagment
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CheckNextId()
        {
            using (cmd = new SqlCommand("SELECT ISNULL(MAX(gerencia), 0) + 1 FROM gerencias", Conex.nomi))

            {
                Conex.OpenNomina();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CreateManagment(Managment m)
        {
            string query = "INSERT INTO gerencias(gerencia,nombre,responsable) " +
                           "VALUES(@gerencia, @nombre, @responsable)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@gerencia", m.ManagmentId);
                cmd.Parameters.AddWithValue("@nombre", m.Name);
                cmd.Parameters.AddWithValue("@responsable", m.Responsible);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteManagment(int Id)
        {
            string query = "DELETE FROM gerencias WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Managment> FilterByManagment(string Name)
        {
            List<Managment> list = new List<Managment>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM gerencias WHERE nombre LIKE '%" + Name + @"%'", Conex.nomi))
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

                throw new Exception("Error al obtener las gerencias: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Managment GetManagment(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM gerencias WHERE id = @id", Conex.nomi))
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

        public List<Managment> GetManagments()
        {
            List<Managment> list = new List<Managment>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM gerencias ORDER BY gerencia ASC", Conex.nomi))
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

                throw new Exception("Error al obtener las gerencias: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateManagment(Managment m)
        {
            string query = "UPDATE gerencias SET nombre=@nombre,responsable=@responsable WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", m.ManagmentId);
                cmd.Parameters.AddWithValue("@gerencia", m.ManagmentId);
                cmd.Parameters.AddWithValue("@nombre", m.Name);
                cmd.Parameters.AddWithValue("@responsable", m.Responsible);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private static Managment ShowDataGrid(SqlDataReader reader)
        {
            return new Managment
            {
                Id = Convert.ToInt32(reader["id"]),
                ManagmentId = Convert.ToInt32(reader["gerencia"]),
                Name = Convert.ToString(reader["nombre"]),
                Responsible = Convert.ToString(reader["responsable"])
            };
        }
    }
}
