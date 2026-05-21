using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.ToolWears;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class ToolWearRepository : IToolWear
    {
        ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();
        private readonly CycleRepository _cycleRepository;
        private readonly string _Cycle;

        public ToolWearRepository()
        {
            _cycleRepository = new CycleRepository();
            _Cycle = _cycleRepository.GetCycle()._Cycle;
        }

        public decimal GetImportToolWear()
        {
            using (cmd = new SqlCommand("SELECT valor_desgaste_herramienta FROM parametros", Conex.nomi))
            {
                Conex.OpenNomina();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public List<ToolWearReport> GetDataReport(int Payweek, bool Temp)
        {
            List<ToolWearReport> list = new List<ToolWearReport>();
            string query = "";
            try
            {
                if (Temp)
                {
                    query = $@"SELECT * FROM desgaste_herramienta_t2";
                }
                else
                {
                    query = $@"SELECT * FROM desgaste_herramienta_i WHERE semana_pago = {Payweek}";

                }
                using (cmd = new SqlCommand(query, Conex.nomi))

                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowDataReport(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los empleados: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<ToolWear> GetToolWears(DateTime Start, DateTime End)
        {
            List<ToolWear> list = new List<ToolWear>();

            try
            {

                using (cmd = new SqlCommand($@"WITH DatosIniciales AS (
                                         
                                            SELECT 
                                                rsi.numero_empleado AS n_empleado,
                                                CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) AS n_completo,
                                                rsi.id_categoria,
                                                CASE 
                                                    WHEN cm.id_categoria IS NOT NULL THEN 1 
                                                    ELSE 0 
                                                END AS aplica
                                            FROM resultado_sind_indiv_det_final rsi 
                                            INNER JOIN empleados e ON e.numero_empleado = rsi.numero_empleado
                                            LEFT JOIN categorias_auto_transporte cm ON rsi.id_categoria = cm.id_categoria
                                            WHERE rsi.id_concepto = 1
                                              AND rsi.fecha BETWEEN '{Start}' AND '{End}'
                                        ),
                                        DatosAgrupados AS (
                                          
                                            SELECT 
                                                n_empleado,
                                                n_completo,
                                                MIN(id_categoria) AS id_categoria_min,
                                                SUM(aplica) AS dias
                                            FROM DatosIniciales
                                            WHERE aplica = 1
                                            GROUP BY n_empleado, n_completo
                                        )
                                      
                                        SELECT 
                                            da.n_empleado,
                                            da.n_completo,
                                            da.dias,
                                            da.id_categoria_min AS id_categoria,
                                            ct.nombre_categoria,
                                        
                                            CASE 
                                                WHEN da.dias <= 11 THEN 0.00
                                                ELSE (SELECT TOP 1 valor_desgaste_herramienta FROM parametros)
                                            END AS importe,
                                            (SELECT TOP 1 rsi.uso 
                                             FROM resultado_sind_indiv_det_final rsi 
                                             WHERE rsi.numero_empleado = da.n_empleado) AS uso
                                        FROM DatosAgrupados da
                                        LEFT JOIN categorias ct ON da.id_categoria_min = ct.id_categoria
                                        ORDER BY n_empleado;", Conex.nomi))

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

                throw new Exception("Error al obtener las asistencias: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public int SaveToolWears(List<ToolWear> list,int PayWeek)
        {
            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                string queryTools = @"INSERT INTO desgaste_herramienta_i(numero_empleado,nombre,dias,id_categoria,categoria,importe,semana_pago) 
                                 VALUES(@numero_empleado, @nombre, @dias, @id_categoria, @categoria, @importe,@semana_pago)";

                foreach (var det in list)
                {
                    using (cmd = new SqlCommand(queryTools, Conex.nomi, tra))
                    {
                        cmd.Parameters.AddWithValue("@numero_empleado", det.NumberEmployee);
                        cmd.Parameters.AddWithValue("@nombre", det.FullName);
                        cmd.Parameters.AddWithValue("@dias", det.Days);
                        cmd.Parameters.AddWithValue("@id_categoria", det.CategoryId);
                        cmd.Parameters.AddWithValue("@categoria", det.CategoryName);
                        cmd.Parameters.AddWithValue("@importe", det.Amount);
                        cmd.Parameters.AddWithValue("@semana_pago", PayWeek);
                        cmd.ExecuteNonQuery();
                    }
                }

                string queryGratuities = @"INSERT INTO gratificaciones(id_nomina,numero_empleado,importe,ciclo,periodo,uso,id_concepto) 
                                        VALUES(@id_nomina,@numero_empleado,@importe,@ciclo,@periodo,@uso,@id_concepto)";

                foreach (var det in list)
                {
                    using (cmd = new SqlCommand(queryGratuities, Conex.nomi, tra))
                    {
                        cmd.Parameters.AddWithValue("@id_nomina", 1);
                        cmd.Parameters.AddWithValue("@numero_empleado", det.NumberEmployee);
                        cmd.Parameters.AddWithValue("@importe", det.Amount);
                        cmd.Parameters.AddWithValue("@ciclo", _Cycle);
                        cmd.Parameters.AddWithValue("@periodo", PayWeek);
                        cmd.Parameters.AddWithValue("@uso", det.Use);
                        cmd.Parameters.AddWithValue("@comentarios", "");
                        cmd.Parameters.AddWithValue("@id_concepto", 26);
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

        public int SaveToolWearsTemp(List<ToolWearReport> list)
        {
            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                string query = @"TRUNCATE TABLE desgaste_herramienta_t2";

                using (cmd = new SqlCommand(query, Conex.nomi, tra))
                {
                    cmd.ExecuteNonQuery();
                }

                string queryDetail = @"INSERT INTO desgaste_herramienta_t2(numero_empleado,nombre,dias,id_categoria,categoria,importe) 
                                 VALUES(@numero_empleado, @nombre, @dias, @id_categoria, @categoria, @importe)";

                foreach (var det in list)
                {
                    using (cmd = new SqlCommand(queryDetail, Conex.nomi, tra))
                    {
                        cmd.Parameters.AddWithValue("@numero_empleado", det.NumberEmployee);
                        cmd.Parameters.AddWithValue("@nombre", det.FullName);
                        cmd.Parameters.AddWithValue("@dias", det.Days);
                        cmd.Parameters.AddWithValue("@id_categoria", det.CategoryId);
                        cmd.Parameters.AddWithValue("@categoria", det.CategoryName);
                        cmd.Parameters.AddWithValue("@importe", det.Amount);
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

        private ToolWear ShowDataGrid(SqlDataReader reader)
        {
            return new ToolWear
            {
                NumberEmployee = Convert.ToInt32(reader["n_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                Days = Convert.ToInt32(reader["dias"]),
                CategoryId = Convert.ToInt32(reader["id_categoria"]),
                CategoryName = Convert.ToString(reader["nombre_categoria"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                Use = Convert.ToInt32(reader["uso"])
            };
        }

        private ToolWearReport ShowDataReport(SqlDataReader reader)
        {
            return new ToolWearReport
            {
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["nombre"]),
                Days = Convert.ToInt32(reader["dias"]),
                CategoryId = Convert.ToInt32(reader["id_categoria"]),
                CategoryName = Convert.ToString(reader["categoria"]),
                Amount = Convert.ToDecimal(reader["importe"]),
                PayWeek = Util.GetValueSafe<int>(reader, "semana_pago")

            };
        }    

    }
}
