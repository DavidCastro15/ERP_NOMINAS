using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.UnionCommittee;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
     public class UnionCommitteeRepository : IUnionCom
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateUnionCommitte(UnionCom u)
        {
            string query = "INSERT INTO comite_sindical(numero_empleado,porcentaje,categoria_zafra,categoria_reparacion,id_concepto,uso) " +
                                               " VALUES(@numero_empleado, @porcentaje, @categoria_zafra, @categoria_reparacion, @id_concepto, @uso)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@numero_empleado", u.NumberEmployee);
                cmd.Parameters.AddWithValue("@porcentaje", u.Percentage);
                cmd.Parameters.AddWithValue("@categoria_zafra", u.CategoryHarvest);
                cmd.Parameters.AddWithValue("@categoria_reparacion", u.CategoryRepair);
                cmd.Parameters.AddWithValue("@id_concepto", u.IdConcept);
                cmd.Parameters.AddWithValue("@uso", u.Use);
         
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteUnionCommitte(int Id)
        {
            string query = $"DELETE FROM comite_sindical WHERE id = {Id}";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<UnionCom> FilterByName(string Name)
        {
            List<UnionCom> list = new List<UnionCom>();

            try
            {

                using (cmd = new SqlCommand(" SELECT c.id, c.numero_empleado, CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo, c.porcentaje,c.categoria_reparacion,c.categoria_zafra,e.tipo,c.id_concepto,c.uso " +
                                     " FROM comite_sindical c " +
                                     " INNER JOIN empleados e " +
                                     $" ON c.numero_empleado = e.numero_empleado WHERE CONCAT(nombre, ' ', apellido_paterno, ' ', apellido_materno) LIKE '%{Name}%' " +
                                     " ORDER BY c.numero_empleado ASC", Conex.nomi))
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

                throw new Exception("Error al obtener el comite sindical: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public UnionCom GetGetUnionCommittee(int Id)
        {
            using (cmd = new SqlCommand(" SELECT c.id, c.numero_empleado, CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo, c.porcentaje,c.categoria_reparacion,c.categoria_zafra,e.tipo,c.id_concepto,c.uso " +
                                     " FROM comite_sindical c " +
                                     " INNER JOIN empleados e " +
                                     " ON c.numero_empleado = e.numero_empleado " +
                                     " WHERE c.id = @id"+
                                     " ORDER BY c.numero_empleado ASC", Conex.nomi))
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

        public List<UnionCom> GetUnionCommittees()
        {
            List<UnionCom> list = new List<UnionCom>();

            try
            {

                using (cmd = new SqlCommand(" SELECT c.id, c.numero_empleado, CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo, c.porcentaje,c.categoria_reparacion,c.categoria_zafra,e.tipo,c.id_concepto,c.uso "+
                                     " FROM comite_sindical c "+
                                     " INNER JOIN empleados e "+
                                     " ON c.numero_empleado = e.numero_empleado "+
                                     " ORDER BY c.numero_empleado ASC", Conex.nomi))
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

                throw new Exception("Error al obtener el comite sindical: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateUnionCommitte(UnionCom u)
        {
            string query = "UPDATE comite_sindical SET porcentaje=@porcentaje,categoria_zafra=@categoria_zafra,categoria_reparacion=@categoria_reparacion,id_concepto=@id_concepto,uso=@uso " +
                                        " WHERE id = @id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", u.Id);
                cmd.Parameters.AddWithValue("@numero_empleado", u.NumberEmployee);
                cmd.Parameters.AddWithValue("@porcentaje", u.Percentage);
                cmd.Parameters.AddWithValue("@categoria_zafra", u.CategoryHarvest);
                cmd.Parameters.AddWithValue("@categoria_reparacion", u.CategoryRepair);
                cmd.Parameters.AddWithValue("@id_concepto", u.IdConcept);
                cmd.Parameters.AddWithValue("@uso", u.Use);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private UnionCom ShowDataGrid(SqlDataReader reader)
        {
            return new UnionCom
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee= Convert.ToInt32(reader["numero_empleado"]),
                NameEmployee = Convert.ToString(reader["n_completo"]),
                Percentage = Convert.ToDecimal(reader["porcentaje"]),
                CategoryHarvest = Convert.ToInt32(reader["categoria_zafra"]),
                CategoryRepair = Convert.ToInt32(reader["categoria_reparacion"]),
                Type = Convert.ToString(reader["tipo"]),
                IdConcept = Convert.ToInt32(reader["id_concepto"]),
                Use = Convert.ToInt32(reader["uso"])
            };
        }
    }
}
