using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.HeightsTemperatures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class HeightTemperatureRepository : IHeightTemperature
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();

        public int CreateHeightTemperature(HeightTemperature data)
        {
            int rf = CheckNextReferenceNumber();
            Conex.OpenNomina();
            SqlTransaction transaction = Conex.nomi.BeginTransaction();

            try
            {
                // 1. Insertar el Encabezado
                string queryEnc = @"INSERT INTO alturas_temp_encabezado 
                            (folio, id_nomina, semana_pago, ciclo, fecha, id_percepcion)
                            VALUES (@folio, @id_nomina, @semana_pago, @ciclo, @fecha, @id_percepcion)";

                using (SqlCommand cmdEnc = new SqlCommand(queryEnc, Conex.nomi, transaction))
                {
                    cmdEnc.Parameters.AddWithValue("@folio", rf);
                    cmdEnc.Parameters.AddWithValue("@id_nomina", data.PayrollId);
                    cmdEnc.Parameters.AddWithValue("@semana_pago", data.PayWeek);
                    cmdEnc.Parameters.AddWithValue("@ciclo", data.Cycle);
                    cmdEnc.Parameters.AddWithValue("@fecha", data.Date);
                    cmdEnc.Parameters.AddWithValue("@id_percepcion", data.IdConcept);
                    cmdEnc.ExecuteNonQuery();
                }

                // 2. Insertar los Detalles
                string queryDet = @"INSERT INTO alturas_temp_detalle 
                            (folio, numero_empleado, id_categoria, horas, uso)
                            VALUES (@folio, @numero_empleado, @id_categoria, @horas, @uso)";

                foreach (var detail in data.Details)
                {
                    using (SqlCommand cmdDet = new SqlCommand(queryDet, Conex.nomi, transaction))
                    {
                        cmdDet.Parameters.AddWithValue("@folio", rf); // Se usa el folio del padre
                        cmdDet.Parameters.AddWithValue("@numero_empleado", detail.NumberEmployee);
                        cmdDet.Parameters.AddWithValue("@id_categoria", detail.CategoryId);
                        cmdDet.Parameters.AddWithValue("@horas", detail.Hours);
                        cmdDet.Parameters.AddWithValue("@uso", detail.Use);
                        cmdDet.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                return 1;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        public int DeleteHeightTemperature(int rf)
        {
            // 1. Queries
            string queryDeleteDetails = "DELETE FROM alturas_temp_detalle WHERE folio = @folio";
            string queryDeleteHeader = "DELETE FROM alturas_temp_encabezado WHERE folio = @folio";

            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                // 2. Eliminar Detalles primero (por integridad referencial)
                using (SqlCommand cmdDet = new SqlCommand(queryDeleteDetails, Conex.nomi, tra))
                {
                    cmdDet.Parameters.AddWithValue("@folio", rf);
                    cmdDet.ExecuteNonQuery();
                }

                // 3. Eliminar Encabezado
                using (SqlCommand cmdEnc = new SqlCommand(queryDeleteHeader, Conex.nomi, tra))
                {
                    cmdEnc.Parameters.AddWithValue("@folio", rf);
                    int rowsAffected = cmdEnc.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // Si no borró nada en el encabezado, algo anda mal
                        tra.Rollback();
                        return 0;
                    }
                }

                tra.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                tra.Rollback();
                
                return 0;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        public List<HeightTemperature> FilterByReferenceNumber(int rf)
        {
            List<HeightTemperature> list = new List<HeightTemperature>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM alturas_temp_encabezado WHERE folio LIKE '%" + rf + @"%' ORDER BY id DESC", Conex.nomi))
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

                throw new Exception("Error al obtener las alturas y temperaturas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public HeightTemperature GetHeightTemperature(int rf)
        {
            HeightTemperature record = null;

            string queryEnc = @"SELECT folio, id_nomina, semana_pago, ciclo, fecha, id_percepcion 
                        FROM alturas_temp_encabezado 
                        WHERE folio = @folio";

            using (var cmd = new SqlCommand(queryEnc, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@folio", rf);
                Conex.OpenNomina();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        record = new HeightTemperature
                        {
                            ReferenceNumber = Convert.ToInt32(reader["folio"]),
                            PayrollId = Convert.ToInt32(reader["id_nomina"]),
                            PayWeek = Convert.ToInt32(reader["semana_pago"]),
                            Cycle = reader["ciclo"].ToString(),
                            Date = Convert.ToDateTime(reader["fecha"]),
                            IdConcept = Convert.ToInt32(reader["id_percepcion"])
                        };
                    }
                }
            }

            if (record != null)
            {
                record.Details = GetHeightTemperatureDetails(rf);
            }

            return record;
        }

        public HeightTemperatureDetail GetDetailEmployee(int NumberEmployee)
        {
            using (cmd = new SqlCommand("SELECT numero_empleado, CONCAT(nombre,' ',apellido_paterno,' ', apellido_materno) as n_completo " +
                                       "FROM empleados WHERE numero_empleado = @numberEmployee ", Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@numberEmployee", NumberEmployee);

                Conex.OpenNomina();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        return ShowDetail(reader);
                    }
                }
            }

            return null;
        }

        private List<HeightTemperatureDetail> GetHeightTemperatureDetails(int folio)
        {
            var detalles = new List<HeightTemperatureDetail>();

            string queryDet = @"SELECT d.id, d.numero_empleado, d.id_categoria, d.horas, d.uso,
                               CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) AS nombre_completo
                        FROM alturas_temp_detalle d
                        INNER JOIN empleados e ON d.numero_empleado = e.numero_empleado
                        WHERE d.folio = @folio ORDER BY d.numero_empleado ASC";

            using (var cmd = new SqlCommand(queryDet, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@folio", folio);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detalles.Add(new HeightTemperatureDetail
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                            FullName = reader["nombre_completo"].ToString().Trim(),
                            CategoryId = Convert.ToInt32(reader["id_categoria"]),
                            Hours = Convert.ToDecimal(reader["horas"]),
                            Use = Convert.ToInt32(reader["uso"])
                        });
                    }
                }
            }
            return detalles;
        }

        public List<HeightTemperature> GetHeightTemperatureHeads()
        {
            List<HeightTemperature> list = new List<HeightTemperature>();

            try
            {

                using (cmd = new SqlCommand("SELECT * FROM alturas_temp_encabezado ORDER BY folio DESC", Conex.nomi))
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

                throw new Exception("Error al obtener las alturas y temperaturas: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int UpdateHeightTemperature(HeightTemperature data)
        {
            // 1. Queries
            string queryUpdateEnc = @"UPDATE alturas_temp_encabezado SET 
                                id_nomina = @id_nomina, 
                                semana_pago = @semana_pago, 
                                ciclo = @ciclo, 
                                fecha = @fecha, 
                                id_percepcion = @id_percepcion 
                             WHERE folio = @folio";

            string queryDeleteDet = "DELETE FROM alturas_temp_detalle WHERE folio = @folio";

            string queryInsertDet = @"INSERT INTO alturas_temp_detalle 
                             (folio, numero_empleado, id_categoria, horas, uso) 
                             VALUES (@folio, @numero_empleado, @id_categoria, @horas, @uso)";

            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {

                using (SqlCommand cmd = new SqlCommand(queryUpdateEnc, Conex.nomi, tra))
                {
                    cmd.Parameters.AddWithValue("@folio", data.ReferenceNumber);
                    cmd.Parameters.AddWithValue("@id_nomina", data.PayrollId);
                    cmd.Parameters.AddWithValue("@semana_pago", data.PayWeek);
                    cmd.Parameters.AddWithValue("@ciclo", data.Cycle);
                    cmd.Parameters.AddWithValue("@fecha", data.Date);
                    cmd.Parameters.AddWithValue("@id_percepcion", data.IdConcept);
                    cmd.ExecuteNonQuery();
                }


                using (SqlCommand cmdDel = new SqlCommand(queryDeleteDet, Conex.nomi, tra))
                {
                    cmdDel.Parameters.AddWithValue("@folio", data.ReferenceNumber);
                    cmdDel.ExecuteNonQuery();
                }


                foreach (var det in data.Details)
                {
                    using (SqlCommand cmdIns = new SqlCommand(queryInsertDet, Conex.nomi, tra))
                    {
                        cmdIns.Parameters.AddWithValue("@folio", data.ReferenceNumber);
                        cmdIns.Parameters.AddWithValue("@numero_empleado", det.NumberEmployee);
                        cmdIns.Parameters.AddWithValue("@id_categoria", det.CategoryId);
                        cmdIns.Parameters.AddWithValue("@horas", det.Hours);
                        cmdIns.Parameters.AddWithValue("@uso", det.Use);
                        cmdIns.ExecuteNonQuery();
                    }
                }

                tra.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                tra.Rollback();
                throw;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        public int CheckNextReferenceNumber()
        {
            using (cmd = new SqlCommand("SELECT ISNULL(MAX(folio), 0) + 1 FROM alturas_temp_encabezado", Conex.nomi))

            {
                Conex.OpenNomina();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private HeightTemperatureDetail ShowDetail(SqlDataReader reader)
        {
            return new HeightTemperatureDetail
            {
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
            };
        }

        private HeightTemperature ShowDataGrid(SqlDataReader reader)
        {
            return new HeightTemperature
            {
                ReferenceNumber = Convert.ToInt32(reader["folio"]),
                PayrollId = Convert.ToInt32(reader["id_nomina"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"]),
                Cycle = Convert.ToString(reader["ciclo"]),
                Date = Convert.ToDateTime(reader["fecha"]),
                IdConcept= Convert.ToInt32(reader["id_percepcion"])
            };
        }

      
    }
}
