using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class EquipmentRepository : IEquipment
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateEquipment(Equipment e)
        {
            string query = " INSERT INTO equipos(equipo,descripcion,grupos) " +
                                     " VALUES(@equipo, @descripcion, @grupos) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@equipo", e.Equip);
                cmd.Parameters.AddWithValue("@descripcion", e.Description);
                cmd.Parameters.AddWithValue("@grupos", e.Group);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }
         
        public int DeleteEquipment(int Id)
        {
            string query = $"DELETE FROM equipos WHERE id = {Id}";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public bool ExistsEquipment(int e)
        {
            using (cmd = new SqlCommand("SELECT 1 equipo FROM equipos WHERE equipo = @equip", Conex.nomi))

            {
                cmd.Parameters.AddWithValue("@equip", e);
                Conex.OpenNomina();
                return cmd.ExecuteScalar() != null;
            }
        }

        public List<Equipment> FilterByEquipment(int e)
        {
            if (e == 0)
            {
                GetEquipments();
            }
            List<Equipment> list = new List<Equipment>();

            try
            {

                using (cmd = new SqlCommand($"SELECT * FROM equipos WHERE equipo LIKE '%{e}%' ORDER BY equipo", Conex.nomi))
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

                throw new Exception("Error al obtener los equipos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Equipment GetEquipment(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM equipos WHERE id = @id", Conex.nomi))
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

        public List<Equipment> GetEquipments()
        {
            List<Equipment> list = new List<Equipment>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM equipos", Conex.nomi))
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

                throw new Exception("Error al obtener los equipos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateEquipment(Equipment e)
        {
            string query = "UPDATE equipos SET equipo=@equipo,descripcion=@descripcion,grupos=@grupos WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", e.Id);
                cmd.Parameters.AddWithValue("@equipo", e.Equip);                  
                cmd.Parameters.AddWithValue("@descripcion", e.Description);
                cmd.Parameters.AddWithValue("@grupos", e.Group);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private Equipment ShowDataGrid(SqlDataReader reader)
        {
            return new Equipment
            {
                Id = Convert.ToInt32(reader["id"]),
                Equip = Convert.ToInt32(reader["equipo"]),
                Description = Convert.ToString(reader["descripcion"]),
                Group = Convert.ToInt32(reader["grupos"])
            };
        }
    }
}
