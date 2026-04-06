using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Materials;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class MaterialRepository : IMaterial
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateMaterial(Material m)
        {
            string query = " INSERT INTO materiales(material,pago_tonelada) VALUES(@material,@pago_tonelada) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                             
                cmd.Parameters.AddWithValue("@material", m._Material);
                cmd.Parameters.AddWithValue("@pago_tonelada", m.PayTon);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteMaterial(int Id)
        {
            string query = $" DELETE FROM materiales WHERE id = {Id} ";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Material> FilterByName(string Name)
        {
            List<Material> list = new List<Material>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM materiales WHERE material LIKE '%" + Name + @"%'", Conex.nomi))
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

                throw new Exception("Error al obtener los materiales: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<Material> GetMaterials()
        {
            List<Material> list = new List<Material>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM materiales ORDER BY id ASC", Conex.nomi))
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

                throw new Exception("Error al obtener los materiales: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Material GetMaterial(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM materiales WHERE id = @id", Conex.nomi))
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

        public int UpdateMaterial(Material m)
        {
            string query = "UPDATE materiales SET material=@material,pago_tonelada=@pago_tonelada WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", m.Id);
                cmd.Parameters.AddWithValue("@material", m._Material);
                cmd.Parameters.AddWithValue("@pago_tonelada", m.PayTon);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private static Material ShowDataGrid(SqlDataReader reader)
        {
            return new Material
            {
                Id = Convert.ToInt32(reader["id"]),
                _Material = Convert.ToString(reader["material"]),
                PayTon = Convert.ToDecimal(reader["pago_tonelada"])
            };
        }

        public int IncreasePrice(decimal Price)
        {
            string query = " UPDATE materiales SET pago_tonelada = pago_tonelada + (pago_tonelada * @porcentaje/100) ";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@porcentaje", Price);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }
    }
}
