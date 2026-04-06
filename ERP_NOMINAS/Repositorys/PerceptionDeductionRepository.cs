using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models;
using ERP_NOMINAS.Models.PerceptionDeduction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class PerceptionDeductionRepository : IPerceptionDeduction
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand(); 

        public int CreatePerceptionDeduction(PerceptDeduction pc)
        {
  
            string query = "INSERT INTO conceptos_pd(tipo,id_concepto,estado,nombre_concepto,acumula,aplica1,aplica2,aplica3,aplica4,aplica5,aplica6,aplica7,aplica8,aplica9,aplica10,orden,gra_parc_exe) "+
                                                    "VALUES(@tipo, @id_concepto, @estado, @nombre_concepto, @acumula, @aplica1, @aplica2, @aplica3, @aplica4, @aplica5, @aplica6, @aplica7, @aplica8, @aplica9, @aplica10, @orden, @gra_parc_exe)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                // Uso de parámetros para evitar inyección SQL
              
                cmd.Parameters.AddWithValue("@tipo", pc.Type);
                cmd.Parameters.AddWithValue("@id_concepto",pc.IdConcept);
                cmd.Parameters.AddWithValue("@estado",pc.Status);
                cmd.Parameters.AddWithValue("@nombre_concepto",pc.NameConcept);
                cmd.Parameters.AddWithValue("@acumula",pc.Accumulate);
                cmd.Parameters.AddWithValue("@aplica1",pc.Apply1);
                cmd.Parameters.AddWithValue("@aplica2",pc.Apply2);
                cmd.Parameters.AddWithValue("@aplica3",pc.Apply3);
                cmd.Parameters.AddWithValue("@aplica4",pc.Apply4);
                cmd.Parameters.AddWithValue("@aplica5",pc.Apply5);
                cmd.Parameters.AddWithValue("@aplica6",pc.Apply6);
                cmd.Parameters.AddWithValue("@aplica7",pc.Apply7);
                cmd.Parameters.AddWithValue("@aplica8",pc.Apply8);
                cmd.Parameters.AddWithValue("@aplica9",pc.Apply9);
                cmd.Parameters.AddWithValue("@aplica10",pc.Apply10);
                cmd.Parameters.AddWithValue("@orden", pc.Order);
                cmd.Parameters.AddWithValue("@gra_parc_exe", pc.GraParcExe);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeletePerceptionDeduction(int id)
        {
            string query = "DELETE FROM conceptos_pd WHERE id = '" + id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public bool ExistsId(int Id)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM conceptos_pd WHERE id_concepto = @id", Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@id", Id);

                Conex.OpenNomina();

                // ExecuteScalar devuelve la primera columna de la primera fila
                int existe = Convert.ToInt32(cmd.ExecuteScalar());

                return existe > 0 ? true : false;
            }
        }

        public List<PerceptDeduction> FilterById(int Id)
        {
            List<PerceptDeduction> list = new List<PerceptDeduction>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM conceptos_pd WHERE id_concepto LIKE '%" + Id + @"%'", Conex.nomi))
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

                throw new Exception("Error al obtener los conceptos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public PerceptDeduction GetPerceptionDeduction(int Id)
        {
            using (cmd = new SqlCommand("SELECT * FROM conceptos_pd WHERE id = @id", Conex.nomi))
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

        public List<PerceptDeduction> GetPerceptionDeductions()
        {
            List<PerceptDeduction> list = new List<PerceptDeduction>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM conceptos_pd ORDER BY id_concepto ASC", Conex.nomi))
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

                throw new Exception("Error al obtener los conceptos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdatePerceptionDeduction(PerceptDeduction pc)
        {
            string query = "UPDATE conceptos_pd SET tipo=@tipo,estado=@estado,nombre_concepto=@nombre_concepto,acumula=@acumula,aplica1=@aplica1,aplica2=@aplica2," +
                                       "aplica3 = @aplica3,aplica4 = @aplica4,aplica5 = @aplica5,aplica6 = @aplica6,aplica7 = @aplica7,aplica8 = @aplica8,aplica9 = @aplica9,aplica10 = @aplica10 WHERE id = @id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", pc.Id);
                cmd.Parameters.AddWithValue("@tipo", pc.Type);
                cmd.Parameters.AddWithValue("@id_concepto", pc.IdConcept);
                cmd.Parameters.AddWithValue("@estado", pc.Status);
                cmd.Parameters.AddWithValue("@nombre_concepto", pc.NameConcept);
                cmd.Parameters.AddWithValue("@acumula", pc.Accumulate);
                cmd.Parameters.AddWithValue("@aplica1", pc.Apply1);
                cmd.Parameters.AddWithValue("@aplica2", pc.Apply2);
                cmd.Parameters.AddWithValue("@aplica3", pc.Apply3);
                cmd.Parameters.AddWithValue("@aplica4", pc.Apply4);
                cmd.Parameters.AddWithValue("@aplica5", pc.Apply5);
                cmd.Parameters.AddWithValue("@aplica6", pc.Apply6);
                cmd.Parameters.AddWithValue("@aplica7", pc.Apply7);
                cmd.Parameters.AddWithValue("@aplica8", pc.Apply8);
                cmd.Parameters.AddWithValue("@aplica9", pc.Apply9);
                cmd.Parameters.AddWithValue("@aplica10", pc.Apply10);
                cmd.Parameters.AddWithValue("@orden", pc.Order);
                cmd.Parameters.AddWithValue("@gra_parc_exe", pc.GraParcExe);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        public int CheckNextId()
        {
            using (cmd = new SqlCommand("SELECT ISNULL(MAX(id_concepto), 0) + 1 FROM conceptos_pd", Conex.nomi))

            {
                Conex.OpenNomina();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static PerceptDeduction ShowDataGrid(SqlDataReader reader)
        {
            return new PerceptDeduction
            {
                Id = Convert.ToInt32(reader["id"]),
                Type = Convert.ToString(reader["tipo"]),
                IdConcept = Convert.ToInt32(reader["id_concepto"]),
                Status = Convert.ToString(reader["estado"]),
                NameConcept = Convert.ToString(reader["nombre_concepto"]),
                Accumulate = Convert.ToString(reader["acumula"]),
                Apply1 = Convert.ToInt32(reader["aplica1"]),
                Apply2 = Convert.ToInt32(reader["aplica2"]),
                Apply3 = Convert.ToInt32(reader["aplica3"]),
                Apply4 = Convert.ToInt32(reader["aplica4"]),
                Apply5 = Convert.ToInt32(reader["aplica5"]),
                Apply6 = Convert.ToInt32(reader["aplica6"]),
                Apply7 = Convert.ToInt32(reader["aplica7"]),
                Apply8 = Convert.ToInt32(reader["aplica8"]),
                Apply9 = Convert.ToInt32(reader["aplica9"]),
                Apply10 = Convert.ToInt32(reader["aplica10"]),
                Order = Convert.ToInt32(reader["orden"]),
                GraParcExe = Convert.ToInt32(reader["gra_parc_exe"])
            };
        }
    }
}
