using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.CategoriesTrained;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class CategoryTrainedRepository : ICategoryTrained
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int AddTrainedCategory(CategoryTrained ct)
        {

            string query = "INSERT INTO categorias_capacitadas(id_empleado,id_categoria,autorizacion,fecha_autorizacion) VALUES(@id_empleado, @id_categoria, @autorizacion, @fecha_autorizacion)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
                cmd.Parameters.AddWithValue("@id_empleado", ct.IdEmployee);
                cmd.Parameters.AddWithValue("@id_categoria", ct.IdCategory);
                cmd.Parameters.AddWithValue("@autorizacion", 1);
                cmd.Parameters.AddWithValue("@fecha_autorizacion",Convert.ToDateTime( ct.DateAuthorize).ToString("yyyy-MM-dd"));
             

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int DeleteTrainedCategory(int Id)
        {
            string query = "DELETE FROM categorias_capacitadas WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<CategoryTrained> GetTrainedCategories(int IdEmployee)
        {
            List<CategoryTrained> list = new List<CategoryTrained>();

            try
            {
                using (cmd = new SqlCommand(" SELECT CC.id,CC.id_categoria,C.nombre_categoria,E.numero_empleado, CONCAT(E.nombre,' ',E.apellido_paterno,' ', E.apellido_materno) as n_completo, CC.fecha_autorizacion "+
                                    " FROM categorias C "+
                                    " INNER JOIN categorias_capacitadas CC "+
                                    " ON "+
                                    " C.id_categoria = CC.id_categoria "+
                                    " INNER JOIN empleados E "+
                                    " ON "+
                                    $" e.numero_empleado = {IdEmployee} "+
                                    $" WHERE CC.id_empleado = {IdEmployee} ORDER BY cc.id_categoria ", Conex.nomi))
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

                throw new Exception("Error al obtener las categorias capacitadas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        private CategoryTrained ShowDataGrid(SqlDataReader reader)
        {
            return new CategoryTrained
            {
                Id = Convert.ToInt32(reader["id"]),
                IdCategory = Convert.ToInt32(reader["id_categoria"]),
                NameCategory = Convert.ToString(reader["nombre_categoria"]),
                IdEmployee = Convert.ToInt32(reader["numero_empleado"]),
                NameEmployee= Convert.ToString(reader["n_completo"]),
                DateAuthorize = Convert.ToDateTime(reader["fecha_autorizacion"])
            };
        }
    }
}
