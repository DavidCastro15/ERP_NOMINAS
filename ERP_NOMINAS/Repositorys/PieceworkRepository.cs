using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Cycles;
using ERP_NOMINAS.Models.Pieceworks;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class PieceworkRepository : IPiecework
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();
        CycleRepository _repoCycle = new CycleRepository();

        public int CreatePiecework(Piecework p)
        {
            int rf = CheckNextReferenceNumber();
            Cycle getCycl = _repoCycle.GetCycle();

            string queryHeader = @"INSERT INTO destajos (folio, id_nomina, semana_pago, ciclo, fecha, turno, material, 
                               tarifa_tonelada, toneladas_cargadas, cantidad_cargadores, uso, id_percepcion) 
                               VALUES (@folio, @id_nomina, @semana_pago, @ciclo, @fecha, @turno, @material, 
                               @tarifa_tonelada, @toneladas_cargadas, @cantidad_cargadores, @uso, @id_percepcion)";

            Conex.OpenNomina();


            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                using (cmd = new SqlCommand(queryHeader, Conex.nomi, tra))
                {
                    cmd.Parameters.AddWithValue("@folio", rf);
                    cmd.Parameters.AddWithValue("@id_nomina", p.PayRollId);
                    cmd.Parameters.AddWithValue("@semana_pago", p.PayWeek);
                    cmd.Parameters.AddWithValue("@ciclo", getCycl._Cycle.ToString());
                    cmd.Parameters.AddWithValue("@fecha", p.Date);
                    cmd.Parameters.AddWithValue("@turno", p.Shift);
                    cmd.Parameters.AddWithValue("@material", p.Material);
                    cmd.Parameters.AddWithValue("@tarifa_tonelada", p.TonRate);
                    cmd.Parameters.AddWithValue("@toneladas_cargadas", p.TonCharged);
                    cmd.Parameters.AddWithValue("@cantidad_cargadores", p.QuantityCharged);
                    cmd.Parameters.AddWithValue("@uso", p.Use);
                    cmd.Parameters.AddWithValue("@id_percepcion", p.PercepctionId);

                    cmd.ExecuteNonQuery();
                }


                string queryDettail = "INSERT INTO destajos_detalle (folio, numero_empleado) VALUES (@folio, @numero_empleado)";

                foreach (var det in p.Details)
                {
                    using (cmd = new SqlCommand(queryDettail, Conex.nomi, tra))
                    {
                        cmd.Parameters.AddWithValue("@folio", rf);
                        cmd.Parameters.AddWithValue("@numero_empleado", det.NumberEmployee);
                        cmd.ExecuteNonQuery();
                    }
                }

                tra.Commit();
                return 1;
            }
            catch (Exception)
            {
                tra.Rollback();
                throw;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        public int UpdatePiecework(Piecework p)
        {
            // 1. Query para actualizar el encabezado
            string queryUpdateEncabezado = @"UPDATE destajos SET 
                                    id_nomina = @id_nomina, 
                                    semana_pago = @semana_pago, 
                                    ciclo = @ciclo, 
                                    fecha = @fecha, 
                                    turno = @turno, 
                                    material = @material, 
                                    tarifa_tonelada = @tarifa_tonelada, 
                                    toneladas_cargadas = @toneladas_cargadas, 
                                    cantidad_cargadores = @cantidad_cargadores, 
                                    uso = @uso, 
                                    id_percepcion = @id_percepcion 
                                    WHERE folio = @folio";

            // 2. Queries para manejar el detalle
            string queryDeleteDetail = "DELETE FROM destajos_detalle WHERE folio = @folio";
            string queryInsertDetail = "INSERT INTO destajos_detalle (folio, numero_empleado) VALUES (@folio, @numero_empleado)";

            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                // Actualizar Encabezado
                using (SqlCommand cmd = new SqlCommand(queryUpdateEncabezado, Conex.nomi, tra))
                {
                    cmd.Parameters.AddWithValue("@folio", p.ReferenceNumber); // Asegúrate que el objeto p tenga el Folio original
                    cmd.Parameters.AddWithValue("@id_nomina", p.PayRollId);
                    cmd.Parameters.AddWithValue("@semana_pago", p.PayWeek);
                    cmd.Parameters.AddWithValue("@ciclo", p.Cycle); // O usar el método de repo si es necesario
                    cmd.Parameters.AddWithValue("@fecha", p.Date);
                    cmd.Parameters.AddWithValue("@turno", p.Shift);
                    cmd.Parameters.AddWithValue("@material", p.Material);
                    cmd.Parameters.AddWithValue("@tarifa_tonelada", p.TonRate);
                    cmd.Parameters.AddWithValue("@toneladas_cargadas", p.TonCharged);
                    cmd.Parameters.AddWithValue("@cantidad_cargadores", p.QuantityCharged);
                    cmd.Parameters.AddWithValue("@uso", p.Use);
                    cmd.Parameters.AddWithValue("@id_percepcion", p.PercepctionId);

                    cmd.ExecuteNonQuery();
                }

                // Eliminar detalles anteriores
                using (SqlCommand cmdDel = new SqlCommand(queryDeleteDetail, Conex.nomi, tra))
                {
                    cmdDel.Parameters.AddWithValue("@folio", p.ReferenceNumber);
                    cmdDel.ExecuteNonQuery();
                }

                // Insertar los nuevos detalles
                foreach (var det in p.Details)
                {
                    using (SqlCommand cmdIns = new SqlCommand(queryInsertDetail, Conex.nomi, tra))
                    {
                        cmdIns.Parameters.AddWithValue("@folio", p.ReferenceNumber);
                        cmdIns.Parameters.AddWithValue("@numero_empleado", det.NumberEmployee);
                        cmdIns.ExecuteNonQuery();
                    }
                }

                tra.Commit();
                return 1; // Éxito
            }
            catch (Exception)
            {
                tra.Rollback();
                throw;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        public int DeletePieceWork(int rf)
        {
            Conex.OpenNomina();

            using (SqlTransaction tra = Conex.nomi.BeginTransaction())
            {
                try
                {
                    string queryDetail = "DELETE FROM destajos_detalle WHERE folio = @folio";
                    using (var cmdDetail = new SqlCommand(queryDetail, Conex.nomi, tra))
                    {
                        cmdDetail.Parameters.AddWithValue("@folio", rf);
                        cmdDetail.ExecuteNonQuery();
                    }

                    string queryHeader = "DELETE FROM destajos WHERE folio = @folio";
                    using (var cmdHeader = new SqlCommand(queryHeader, Conex.nomi, tra))
                    {
                        cmdHeader.Parameters.AddWithValue("@folio", rf);
                        int rowsAffected = cmdHeader.ExecuteNonQuery();

                        tra.Commit();
                        return rowsAffected; 
                    }
                }
                catch (Exception)
                {
                    tra.Rollback();
                    throw;
                }
                finally
                {
                    Conex.nomi.Close();
                }
            }
        }

        public List<Piecework> FilterByRefenceNumber(int rf)
        {
            List<Piecework> list = new List<Piecework>();
            try
            {

                using (cmd = new SqlCommand("SELECT * FROM destajos WHERE folio LIKE '%" + rf + @"%'", Conex.nomi))
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

                throw new Exception("Error al obtener los destajos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public Piecework GetPiecework(int rf)
        {
            Piecework piecework = null;

            // 1. Obtener el encabezado
            using (var cmd = new SqlCommand("SELECT * FROM destajos WHERE folio = @folio", Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@folio", rf);
                Conex.OpenNomina();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        piecework = ShowDataGrid(reader); // Tu método que mapea el encabezado
                    }
                }
            }

            // 2. Si el encabezado existe, cargar sus detalles (Empleados)
            if (piecework != null)
            {
                piecework.Details = GetPieceworkDetails(rf);
            }

            return piecework;
        }

        private List<PieceworkDetail> GetPieceworkDetails(int folio)
        {
            var detalles = new List<PieceworkDetail>();

            // Consulta que une los detalles del destajo con la info de empleados y categorías
            string query = @"SELECT dd.numero_empleado, CONCAT( e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,
                            e.categoria_zafra, c.nombre_categoria 
                     FROM destajos_detalle dd
                     INNER JOIN empleados e ON dd.numero_empleado = e.numero_empleado
                     INNER JOIN categorias c ON e.categoria_zafra = c.id_categoria
                     WHERE dd.folio = @folio";

            using (var cmd = new SqlCommand(query, Conex.nomi))
            {
                cmd.Parameters.AddWithValue("@folio", folio);
                // Conexión ya debería estar abierta o abrirla aquí

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int categoriaActual = Convert.ToInt32(reader["categoria_zafra"]);
                        detalles.Add(new PieceworkDetail
                        {
                            NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                            FullName = $"{reader["n_completo"]}".Trim(),
                            Category = Convert.ToInt32(reader["categoria_zafra"]),
                            Foreman = (categoriaActual == 201) ? 1 : 0
                        });
                    }
                }
            }
            return detalles;
        }

        public List<Piecework> GetPieceworks()
        {
            List<Piecework> list = new List<Piecework>();
            try
            {

                using (cmd = new SqlCommand("SELECT * FROM destajos ORDER BY folio DESC", Conex.nomi))
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

                throw new Exception("Error al obtener los destajos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public PieceworkDetail GetDetailEmployee(int NumberEmployee)
        {
            using (cmd = new SqlCommand("SELECT numero_empleado, CONCAT(nombre,' ',apellido_paterno,' ', apellido_materno) as n_completo, categoria_zafra "+
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

        public int CheckNextReferenceNumber()
        {
            using (cmd = new SqlCommand("SELECT ISNULL(MAX(folio), 0) + 1 FROM destajos", Conex.nomi))

            {
                Conex.OpenNomina();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public decimal CheckRateTon(string m)
        {
            using (cmd = new SqlCommand("SELECT pago_tonelada FROM materiales WHERE material = @material", Conex.nomi))

            {
                cmd.Parameters.AddWithValue("@material", m);
                Conex.OpenNomina();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public List<PieceWorkDetailReport> ShowReportDetail(DateTime d1,DateTime d2)
        {
            List<PieceWorkDetailReport> list = new List<PieceWorkDetailReport>();
            try
            {

                using (cmd = new SqlCommand("SELECT d.folio,dd.numero_empleado,d.semana_pago,d.ciclo,d.fecha,d.turno,d.material,d.tarifa_tonelada,d.toneladas_cargadas,d.cantidad_cargadores,d.uso,d.id_percepcion "+
                                            "FROM destajos d " +
                                            "INNER JOIN destajos_detalle dd " +
                                            "ON d.folio = dd.folio " +
                                            "WHERE d.fecha BETWEEN @before AND @after", Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@before", d1);
                    cmd.Parameters.AddWithValue("@after", d2);
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ReportDataDetails(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los destajos: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        private Piecework ShowDataGrid(SqlDataReader reader)
        {
            return new Piecework
            {
                ReferenceNumber = Convert.ToInt32(reader["folio"]),
                PayRollId = Convert.ToInt32(1),
                PayWeek = Convert.ToInt32(reader["semana_pago"]),
                Cycle = Convert.ToString(reader["ciclo"]),
                Date = Convert.ToDateTime(reader["fecha"]),
                Shift = Convert.ToInt32(reader["turno"]),
                Material = Convert.ToString(reader["material"]),
                TonRate = Convert.ToDecimal(reader["tarifa_tonelada"]),
                TonCharged = Convert.ToDecimal(reader["toneladas_cargadas"]),
                QuantityCharged = Convert.ToInt32(reader["cantidad_cargadores"]),
                Use = Convert.ToInt32(reader["uso"]),
                PercepctionId = Convert.ToInt32(20)
            };
        }

        private PieceworkDetail ShowDetail(SqlDataReader reader)
        {
            return new PieceworkDetail
            {
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Foreman = Convert.ToInt32(reader["categoria_zafra"]) == 201 ? 1 : Convert.ToInt32(0),
                Category = Convert.ToInt32(reader["categoria_zafra"])
            };
        }

        private PieceWorkDetailReport ReportDataDetails(SqlDataReader reader)
        {
            return new PieceWorkDetailReport
            {
                ReferenceNumber = Convert.ToInt32(reader["folio"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"]),
                Cycle = Convert.ToString(reader["ciclo"]),
                Date = Convert.ToDateTime(reader["fecha"]),
                Shift = Convert.ToInt32(reader["turno"]),
                Material = Convert.ToString(reader["material"]),
                TonRate = Convert.ToDecimal(reader["tarifa_tonelada"]),
                TonCharged = Convert.ToDecimal(reader["toneladas_cargadas"]),
                QuantityCharged = Convert.ToInt32(reader["cantidad_cargadores"]),
                Use = Convert.ToInt32(reader["uso"])
            };
        }


    }
}
