using ERP_NOMINAS.Models.Deductions.DiscountGlobal;
using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.Earnings
{
    public class DiscountGlobalRepository : IDGlobal
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateDGlobal(DGlobal d)
        {
            string query = @"INSERT INTO descuentos_globales(ciclo,no_periodo,id_concepto,eventual,temporal,permanente,importe,descripcion_descuento) 
                                                      VALUES(@ciclo,@no_periodo,@id_concepto,@eventual,@temporal,@permanente,@importe,@descripcion_descuento)";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@ciclo", d.Cycle);
                cmd.Parameters.AddWithValue("@no_periodo", d.PayWeek);
                cmd.Parameters.AddWithValue("@id_concepto", d.IdConcept);
                cmd.Parameters.AddWithValue("@eventual", d.Occasional);
                cmd.Parameters.AddWithValue("@temporal", d.Temporary);
                cmd.Parameters.AddWithValue("@permanente", d.Permanent);
                cmd.Parameters.AddWithValue("@importe", d.Amount);
                cmd.Parameters.AddWithValue("@descripcion_descuento", d.Description);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteDGlobal(int Id)
        {
            string query = "DELETE FROM descuentos_globales WHERE id = '" + Id + @"'";
            using (cmd = new SqlCommand(query, Conex.nomi))
            {
                Conex.OpenNomina();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<DGlobal> FilterByConcept(int IdConcept)
        {
            List<DGlobal> list = new List<DGlobal>();

            string query = $@"SELECT * FROM descuentos_globales WHERE id_concepto LIKE @param  ORDER BY id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@param", $"%{IdConcept}%");
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
                throw new Exception($"Error al obtener los descuentos: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        public DGlobal GetDGlobal(int Id)
        {
            using (cmd = new SqlCommand($@"SELECT * FROM descuentos_globales WHERE id = @id", Conex.nomi))
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

        public List<DGlobal> GetDGlobals()
        {
            List<DGlobal> list = new List<DGlobal>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT * FROM descuentos_globales ORDER BY no_periodo DESC", Conex.nomi))
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

                throw new Exception("Error al obtener los descuentos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;

        }

        public int UpdateDGlobal(DGlobal d)
        {
            string query = @"UPDATE descuentos_globales SET ciclo=@ciclo,no_periodo=@no_periodo,id_concepto=@id_concepto,eventual=@eventual,temporal=@temporal,permanente=@permanente,importe=@importe,descripcion_descuento=@descripcion_descuento
                                                           WHERE id=@id";

            using (cmd = new SqlCommand(query, Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@id", d.Id);
                cmd.Parameters.AddWithValue("@ciclo", d.Cycle);
                cmd.Parameters.AddWithValue("@no_periodo", d.PayWeek);
                cmd.Parameters.AddWithValue("@id_concepto", d.IdConcept);
                cmd.Parameters.AddWithValue("@eventual", d.Occasional);
                cmd.Parameters.AddWithValue("@temporal", d.Temporary);
                cmd.Parameters.AddWithValue("@permanente", d.Permanent);
                cmd.Parameters.AddWithValue("@importe", d.Amount);
                cmd.Parameters.AddWithValue("@descripcion_descuento", d.Description);

                Conex.OpenNomina();
                return cmd.ExecuteNonQuery(); // Devuelve 1 si fue exitoso
            }
        }

        private DGlobal ShowDataGrid(SqlDataReader reader)
        {
            return new DGlobal
            {
                Id = Convert.ToInt32(reader["id"]),
                Cycle = Convert.ToString(reader["ciclo"]),
                PayWeek = Convert.ToInt32(reader["no_periodo"]),
                IdConcept = Convert.ToInt32(reader["id_concepto"]),
                Occasional = Convert.ToByte(reader["eventual"]),
                Temporary = Convert.ToByte(reader["temporal"]),
                Permanent = Convert.ToByte(reader["permanente"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                Description = Convert.ToString(reader["descripcion_descuento"])
            };
        }
    }
}
