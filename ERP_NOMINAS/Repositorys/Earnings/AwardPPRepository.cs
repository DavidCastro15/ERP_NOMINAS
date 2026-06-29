using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.AwardPunctPre;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys
{
    public class AwardPPRepository : IAwardPP
    {
        private ConnectorSql Conex = new ConnectorSql();
        Utilities Util = new Utilities();
        SqlCommand cmd = new SqlCommand();
        private readonly CycleRepository _cycleRepository;
        private readonly string _Cycle;

        public AwardPPRepository()
        {
            _cycleRepository = new CycleRepository();
            _Cycle = _cycleRepository.GetCycle()._Cycle;
        }

        public List<ListAttend> GetDetailAttend(DateTime StartDate, DateTime EndDate)
        {
            AddListAttend(StartDate, EndDate);

            List<ListAttend> list = new List<ListAttend>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT pt.id,pt.numero_empleado,pt.fecha,pt.aplica_pp
                                                            FROM puntualidad_temporal pt
                                                            INNER JOIN empleados e
                                                            ON pt.numero_empleado = e.numero_empleado
                                                            WHERE e.tipo = 'Permanente' 
                                                            ORDER BY pt.numero_empleado", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowListAttend(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los premios: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        private int AddListAttend(DateTime StartDate, DateTime EndDate)
        {
            var awardsList = new List<AwardPPF>();

            string filtroDiasPuntualidad = (_Cycle == "Zafra")
                ? "DATEPART(WEEKDAY, p.fecha) <> 1"
                : "DATEPART(WEEKDAY, p.fecha) NOT IN (1, 7)";

            string filtroTipoPuntualidad = (_Cycle == "Zafra")
                ? "e.tipo IN ('Permanente', 'Temporal')"
                : "e.tipo = 'Permanente'";

            string query = $@"
        DECLARE @ciclo_val VARCHAR(50) = @ciclo;

        SELECT 
            -- Priorizamos el número de empleado de cualquiera de las dos tablas
            ISNULL(pt.numero_empleado, p_agg.numero_empleado) AS numero_empleado,
            ISNULL(pd.destajo, c.salario) AS salary_final,
            ISNULL(pt.pf, 0) AS pf_base,
            ISNULL(pt.pp, 0) AS pp_base,
            ISNULL(pt.uso, 0) AS uso,
            ISNULL(pt.comentarios, '') AS comentarios,
            ISNULL(p_agg.days_pf, 0) AS days_pf,
            ISNULL(p_agg.days_pp, 0) AS days_pp
        FROM premios_temporal pt
        -- El FULL JOIN permite que existan en una tabla, en la otra, o en ambas
        FULL OUTER JOIN (
            SELECT 
                p.numero_empleado,
                SUM(CASE WHEN p.aplica_pf = 1 AND {filtroDiasPuntualidad} THEN 1 ELSE 0 END) AS days_pf,
                SUM(CASE WHEN p.aplica_pp = 1 AND {filtroDiasPuntualidad} THEN 1 ELSE 0 END) AS days_pp
            FROM puntualidad_temporal p
            INNER JOIN empleados e ON p.numero_empleado = e.numero_empleado
            WHERE e.estatus = 'Activo' AND {filtroTipoPuntualidad}
            GROUP BY p.numero_empleado
        ) p_agg ON pt.numero_empleado = p_agg.numero_empleado
        -- Traemos datos del empleado y categoría para los que resulten del JOIN
        INNER JOIN empleados e ON e.numero_empleado = ISNULL(pt.numero_empleado, p_agg.numero_empleado)
        INNER JOIN categorias c ON IIF(@ciclo_val = 'Zafra', e.categoria_zafra, e.categoria_reparacion) = c.id_categoria
        LEFT JOIN premio_destajo pd ON e.numero_empleado = pd.numero_empleado
        WHERE e.estatus = 'Activo'";

            try
            {
                Conex.OpenNomina();
                using (var cmd = new SqlCommand(query, Conex.nomi))
                {
                    // Parámetros para evitar errores de formato de fecha e inyección SQL
                    cmd.Parameters.AddWithValue("@start", StartDate);
                    cmd.Parameters.AddWithValue("@end", EndDate);

                    cmd.ExecuteNonQuery();
                    return 1;
                }
            }
            catch (Exception)
            {
                // Log(ex.Message); 
                return 0;
            }
            finally
            {
                Conex.CloseNomina();
            }
        }

        public List<ListAttend> FilterByNameListAttend(int NumberEmployee)
        {
            List<ListAttend> list = new List<ListAttend>();

            try
            {

                using (cmd = new SqlCommand($"SELECT * FROM puntualidad_temporal WHERE numero_empleado = {NumberEmployee}", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowListAttend(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los premios: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<LastAward> GetLastAward()
        {
            List<LastAward> list = new List<LastAward>();

            try
            {

                using (cmd = new SqlCommand(@"TRUNCATE TABLE premios_temporal
                                                DECLARE @semana_pago INT;
                                                SET @semana_pago = (
                                                    SELECT TOP 1 semana_pago
                                                    FROM premio_presencia_fisica
                                                    ORDER BY id DESC
                                                );

                                                SELECT 
                                                    ppf.numero_empleado,
                                                    ppf.dias_presencia_f,
                                                    ppf.importe AS importePPF,
                                                    ISNULL(pp.dias_puntualidad, 0) AS dias_puntualidad,
                                                    ISNULL(pp.importe, 0) AS importePP,
                                                    @semana_pago AS semana_pago,
                                                    ppf.uso,
                                                    ppf.comentarios
                                                FROM premio_presencia_fisica ppf
                                                LEFT JOIN premio_puntualidad pp
                                                    ON ppf.numero_empleado = pp.numero_empleado
                                                    AND pp.semana_pago = @semana_pago
                                                WHERE ppf.semana_pago = @semana_pago
                                                ORDER BY ppf.numero_empleado; -- <--- IMPORTANTE
           
                                                INSERT INTO premios_temporal (
                                                    numero_empleado,
                                                    pf,
                                                    importe_pf,
                                                    pp,
                                                    importe_pp,
                                                    uso,
                                                    comentarios
                                                )
                                                SELECT 
                                                    ppf.numero_empleado,
                                                    ppf.dias_presencia_f,
                                                    ppf.importe,
                                                    ISNULL(pp.dias_puntualidad, 0),
                                                    ISNULL(pp.importe, 0),
                                                    ppf.uso,
                                                    ppf.comentarios
                                                FROM premio_presencia_fisica ppf
                                                LEFT JOIN premio_puntualidad pp
                                                    ON ppf.numero_empleado = pp.numero_empleado
                                                    AND pp.semana_pago = @semana_pago
                                                WHERE ppf.semana_pago = @semana_pago;", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowLastAward(reader));
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los premios: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public List<ListAttend> ClearFilterByNameListAttend()
        {
            List<ListAttend> list = new List<ListAttend>();

            try
            {

                using (cmd = new SqlCommand("SELECT id,numero_empleado,fecha,aplica_pf,aplica_pp FROM puntualidad_temporal ORDER BY numero_empleado", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowListAttend(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los premios: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        public void AwardsOverview(int monthlyTarget)
        {
            var awardsList = new List<AwardPPF>();

            string filtroDias = (_Cycle == "Zafra")
                ? "DATEPART(WEEKDAY, punt.fecha) <> 1"
                : "DATEPART(WEEKDAY, punt.fecha) NOT IN (1, 7)";

            string filtroTipoEmpleado = (_Cycle == "Zafra")
                ? "e.tipo IN ('Permanente', 'Temporal')"
                : "e.tipo = 'Permanente'";

            string query = $@"
            DECLARE @ciclo_val VARCHAR(50) = @ciclo;

            SELECT 
                e.numero_empleado,
                ISNULL(pd.destajo, c.salario) AS salary_final,
                ISNULL(pt.pf, 0) AS pf_base,
                ISNULL(pt.pp, 0) AS pp_base,
                ISNULL(pt.uso, 0) AS uso,
                ISNULL(pt.comentarios, '') AS comentarios,
                SUM(CASE WHEN punt.aplica_pf = 1 AND {filtroDias} THEN 1 ELSE 0 END) AS days_pf,
                SUM(CASE WHEN punt.aplica_pp = 1 AND {filtroDias} THEN 1 ELSE 0 END) AS days_pp
            FROM empleados e
            INNER JOIN categorias c ON IIF(@ciclo_val = 'Zafra', e.categoria_zafra, e.categoria_reparacion) = c.id_categoria
            LEFT JOIN premios_temporal pt ON e.numero_empleado = pt.numero_empleado
            LEFT JOIN premio_destajo pd ON e.numero_empleado = pd.numero_empleado
            LEFT JOIN puntualidad_temporal punt ON e.numero_empleado = punt.numero_empleado
            WHERE e.estatus = 'Activo' 
              AND (
                    ({filtroTipoEmpleado})
                    OR pt.numero_empleado IS NOT NULL 
                  )
             GROUP BY 
                e.numero_empleado, 
                pd.destajo, 
                c.salario, 
                pt.pf, 
                pt.pp, 
                pt.uso, 
                pt.comentarios,
                pt.numero_empleado

            HAVING 
                SUM(CASE WHEN punt.aplica_pf = 1 AND {filtroDias} THEN 1 ELSE 0 END) > 0 
                OR SUM(CASE WHEN punt.aplica_pp = 1 AND {filtroDias} THEN 1 ELSE 0 END) > 0
                OR pt.numero_empleado IS NOT NULL;

                TRUNCATE TABLE premio_puntualidad_t;
                TRUNCATE TABLE premio_presencia_fisica_t";

            try
            {
                Conex.OpenNomina();
                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@ciclo", _Cycle);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var award = new AwardPPF
                            {
                                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                                Salary = Convert.ToDecimal(reader["salary_final"]),
                                DaysPF = Convert.ToInt32(reader["days_pf"]),
                                DaysPP = Convert.ToInt32(reader["days_pp"]),
                                Use = Convert.ToInt32(reader["uso"]),
                                Comments = reader["comentarios"].ToString()
                            };

                            decimal pfBase = Convert.ToDecimal(reader["pf_base"]);
                            decimal ppBase = Convert.ToDecimal(reader["pp_base"]);

                            award.PF = GetTabularValue(pfBase, award.DaysPF, monthlyTarget);
                            award.AmountPF = Math.Round(award.Salary * award.PF, 2);

                            award.PP = GetTabularValue(ppBase, award.DaysPP, monthlyTarget);
                            award.AmountPP = Math.Round(award.Salary * award.PP, 2);

                            awardsList.Add(award);
                        }
                    }
                }

                if (awardsList.Count > 0)
                {
                    SaveToTemporaryAwards(awardsList, 1, _Cycle, 00000);
                }
            }
            finally { Conex.CloseNomina(); }

        }

        public List<AwardPPF> GetAwardsOverview()
        {
            List<AwardPPF> list = new List<AwardPPF>();

            try
            {

                using (cmd = new SqlCommand(@"SELECT 
                                                pp.id,
                                                pp.numero_empleado,
                                                c.salario,
                                                pf.dias_presencia_f AS days_pf,
                                                pf.presencia_f,
                                                pf.importe AS monto_pf,
                                                pp.dias_puntualidad AS days_pp,
                                                pp.puntualidad,
                                                pp.importe AS monto_pp,
                                                pp.uso
                                            FROM premio_puntualidad_t pp
                                            INNER JOIN premio_presencia_fisica_t pf 
                                                ON pp.numero_empleado = pf.numero_empleado 
                                                AND pp.uso = pf.uso 
                                            INNER JOIN empleados e
                                                ON pp.numero_empleado = e.numero_empleado
                                            INNER JOIN categorias c 
                                                ON IIF(@ciclo = 'Zafra', e.categoria_zafra, e.categoria_reparacion) = c.id_categoria                                         
                                            ORDER BY pp.numero_empleado ASC", Conex.nomi))
                {
                    Conex.OpenNomina();
                    cmd.Parameters.AddWithValue("@ciclo", _Cycle);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            list.Add(ShowAwardOverview(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los premios: " + ex.Message);
            }
            finally
            {

                Conex.CloseNomina();
            }

            return list;
        }

        private void SaveToTemporaryAwards(List<AwardPPF> awards, int payRoll, string cycle, int payWeek)
        {
            using (SqlTransaction transaction = Conex.nomi.BeginTransaction())
            {
                try
                {
                    foreach (var item in awards)
                    {
                        string queryPP = @"INSERT INTO premio_puntualidad_t
                                   (id_nomina, ciclo, semana_pago, dias_puntualidad, importe, uso, numero_empleado,puntualidad)
                                   VALUES (@idNom, @ciclo, @sem, @dias, @imp, @uso, @num,@punt)";

                        using (SqlCommand cmdPP = new SqlCommand(queryPP, Conex.nomi, transaction))
                        {
                            cmdPP.Parameters.AddWithValue("@idNom", payRoll);
                            cmdPP.Parameters.AddWithValue("@ciclo", cycle);
                            cmdPP.Parameters.AddWithValue("@sem", payWeek);
                            cmdPP.Parameters.AddWithValue("@punt", item.PP);
                            cmdPP.Parameters.AddWithValue("@dias", item.DaysPP);
                            cmdPP.Parameters.AddWithValue("@imp", item.AmountPP);
                            cmdPP.Parameters.AddWithValue("@uso", item.Use);
                            cmdPP.Parameters.AddWithValue("@num", item.NumberEmployee);
                            cmdPP.ExecuteNonQuery();
                        }

                        string queryPF = @"INSERT INTO premio_presencia_fisica_t
                                   (id_nomina, ciclo, semana_pago, numero_empleado, dias_presencia_f, importe, uso,presencia_f)
                                   VALUES (@idNom, @ciclo, @sem, @num, @dias, @imp, @uso,@prese)";

                        using (SqlCommand cmdPF = new SqlCommand(queryPF, Conex.nomi, transaction))
                        {
                            cmdPF.Parameters.AddWithValue("@idNom", payRoll);
                            cmdPF.Parameters.AddWithValue("@ciclo", cycle);
                            cmdPF.Parameters.AddWithValue("@sem", payWeek);
                            cmdPF.Parameters.AddWithValue("@prese", item.PF);
                            cmdPF.Parameters.AddWithValue("@num", item.NumberEmployee);
                            cmdPF.Parameters.AddWithValue("@dias", item.DaysPF);
                            cmdPF.Parameters.AddWithValue("@imp", item.AmountPF);
                            cmdPF.Parameters.AddWithValue("@uso", item.Use);
                            cmdPF.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<PieceworkAward> GetPieceworkAwards()
        {
            List<PieceworkAward> list = new List<PieceworkAward>();

            try
            {

                using (cmd = new SqlCommand(@"DECLARE @ciclo AS VARCHAR(50)
                                                            SET @ciclo = (SELECT ciclo FROM ciclo)
                                                            SELECT pd.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) AS n_completo,IIF(@ciclo = 'Zafra' ,e.categoria_zafra,e.categoria_reparacion) AS categoria,c.salario,pd.destajo,(c.salario + pd.destajo) AS salario_final
                                                            FROM premio_destajo pd
                                                            INNER JOIN empleados e
                                                            ON pd.numero_empleado = e.numero_empleado
                                                            INNER JOIN categorias c
                                                            ON IIF(@ciclo = 'Zafra' ,e.categoria_zafra,e.categoria_reparacion) = c.id_categoria", Conex.nomi))
                {
                    Conex.OpenNomina();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(ShowPieceAward(reader));
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

        public void ImportEmployees(DataTable dtCsv, decimal pieceWork)
        {
            Conex.OpenNomina();

            dtCsv.Columns.Add("c2", typeof(decimal));
            foreach (DataRow row in dtCsv.Rows)
            {
                row["c2"] = pieceWork;
            }

            if (dtCsv == null || dtCsv.Rows.Count == 0)
            {
                throw new Exception("El archivo CSV no contiene datos válidos para importar.");
            }

            using (SqlTransaction trans = Conex.nomi.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE premio_destajo", Conex.nomi, trans))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlBulkCopy bulk = new SqlBulkCopy(Conex.nomi, SqlBulkCopyOptions.Default, trans))
                    {
                        bulk.DestinationTableName = "premio_destajo";
                        bulk.ColumnMappings.Clear();

                        bulk.ColumnMappings.Add("c1", "numero_empleado");
                        bulk.ColumnMappings.Add("c2", "destajo");

                        bulk.WriteToServer(dtCsv);
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    Conex.CloseNomina();
                }
            }
        }

        public int SavePiecewokAward(List<PieceworkAward> list)
        {
            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE premio_destajo", Conex.nomi, tra))
                {
                    cmd.ExecuteNonQuery();
                }


                string queryDetail = @"INSERT INTO premio_destajo(numero_empleado,id_categoria,salario,destajo) 
                                                        VALUES(@numero_empleado, @id_categoria, @salario, @destajo)";

                foreach (var det in list)
                {
                    using (cmd = new SqlCommand(queryDetail, Conex.nomi, tra))
                    {
                        cmd.Parameters.AddWithValue("@numero_empleado", det.NumberEmployee);
                        cmd.Parameters.AddWithValue("@id_categoria", det.CategoryId);
                        cmd.Parameters.AddWithValue("@salario", det.Salary);
                        cmd.Parameters.AddWithValue("@destajo", det.FinalSalary);
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

        public int UpdateListAttend(List<ListAttend> list)
        {
            string query = "UPDATE puntualidad_temporal SET aplica_pf=@aplica_pf,aplica_pp=@aplica_pp WHERE id=@id";

            Conex.OpenNomina();
            SqlTransaction tra = Conex.nomi.BeginTransaction();

            try
            {

                foreach (var det in list)
                {
                    using (SqlCommand cmdIns = new SqlCommand(query, Conex.nomi, tra))
                    {
                        cmdIns.Parameters.AddWithValue("@id", det.Id);
                        cmdIns.Parameters.AddWithValue("@aplica_pf", det.ApplyPF);
                        cmdIns.Parameters.AddWithValue("@aplica_pp", det.ApplyPP);
                        cmdIns.ExecuteNonQuery();
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

        public AwardPPF GetAward(int Id)
        {
            using (cmd = new SqlCommand(@"SELECT 
                                                pp.id,
                                                pp.numero_empleado,
                                                c.salario,
                                                pf.dias_presencia_f AS days_pf,
                                                pf.presencia_f,
                                                pf.importe AS monto_pf,
                                                pp.dias_puntualidad AS days_pp,
                                                pp.puntualidad,
                                                pp.importe AS monto_pp,
                                                pp.uso
                                            FROM premio_puntualidad_t pp
                                            INNER JOIN premio_presencia_fisica_t pf 
                                                ON pp.numero_empleado = pf.numero_empleado 
                                                AND pp.uso = pf.uso 
                                            INNER JOIN empleados e
                                                ON pp.numero_empleado = e.numero_empleado
                                            INNER JOIN categorias c 
                                                ON IIF(@ciclo = 'Zafra', e.categoria_zafra, e.categoria_reparacion) = c.id_categoria
                                            WHERE pp.id = @id", Conex.nomi))
            {

                cmd.Parameters.AddWithValue("@ciclo", _Cycle);
                cmd.Parameters.AddWithValue("@id", Id);

                Conex.OpenNomina();
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        return ShowAwardOverview(reader);
                    }
                }
            }

            return null;
        }

        public int CreateAwardEmployee(AwardPPF aPF)
        {
            int affectedRows = 0;
            try
            {
                Conex.OpenNomina();
              
                using (SqlTransaction trans = Conex.nomi.BeginTransaction())
                {
                    try
                    {
                        string queryPP = @"INSERT INTO premio_puntualidad_t (dias_puntualidad, puntualidad, importe, uso, numero_empleado)
                                   VALUES (@diasPP, @puntualidad, @importePP, @uso, @numEmp)";

                        using (SqlCommand cmdPP = new SqlCommand(queryPP, Conex.nomi, trans))
                        {
                            cmdPP.Parameters.Add("@diasPP", SqlDbType.Int).Value = aPF.DaysPP;
                            cmdPP.Parameters.Add("@puntualidad", SqlDbType.VarChar).Value = aPF.PP; // Ajusta el tipo de dato
                            cmdPP.Parameters.Add("@importePP", SqlDbType.Decimal).Value = aPF.AmountPP;
                            cmdPP.Parameters.Add("@uso", SqlDbType.VarChar).Value = aPF.Use;
                            cmdPP.Parameters.Add("@numEmp", SqlDbType.Int).Value = aPF.NumberEmployee;
                            affectedRows += cmdPP.ExecuteNonQuery();
                        }

                        string queryPF = @"INSERT INTO premio_presencia_fisica_t (numero_empleado, presencia_f, dias_presencia_f, importe, uso)
                                   VALUES (@numEmp, @presencia_f, @diasPF, @importePF, @uso)";

                        using (SqlCommand cmdPF = new SqlCommand(queryPF, Conex.nomi, trans))
                        {
                            cmdPF.Parameters.Add("@numEmp", SqlDbType.Int).Value = aPF.NumberEmployee;
                            cmdPF.Parameters.Add("@presencia_f", SqlDbType.VarChar).Value = aPF.PF;
                            cmdPF.Parameters.Add("@diasPF", SqlDbType.Int).Value = aPF.DaysPF;
                            cmdPF.Parameters.Add("@importePF", SqlDbType.Decimal).Value = aPF.AmountPF;
                            cmdPF.Parameters.Add("@uso", SqlDbType.VarChar).Value = aPF.Use;
                            affectedRows += cmdPF.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            finally
            {
                Conex.CloseNomina();
            }
            return affectedRows;
        }

        public int UpdateAwardEmployee(AwardPPF aPF)
        {
            int affectedRows = 0;
            try
            {
                Conex.OpenNomina();
                using (SqlTransaction trans = Conex.nomi.BeginTransaction())
                {
                    try
                    {
                        string queryPP = @"UPDATE premio_puntualidad_t 
                                   SET dias_puntualidad = @diasPP, 
                                       puntualidad = @puntualidad, 
                                       importe = @importePP, 
                                       uso = @uso
                                   WHERE id = @id AND numero_empleado = @numEmp";

                        using (SqlCommand cmdPP = new SqlCommand(queryPP, Conex.nomi, trans))
                        {
                            cmdPP.Parameters.AddWithValue("@diasPP", aPF.DaysPP);
                            cmdPP.Parameters.AddWithValue("@puntualidad", aPF.PP);
                            cmdPP.Parameters.AddWithValue("@importePP", aPF.AmountPP);
                            cmdPP.Parameters.AddWithValue("@uso", aPF.Use);
                            cmdPP.Parameters.AddWithValue("@id", aPF.Id);
                            cmdPP.Parameters.AddWithValue("@numEmp", aPF.NumberEmployee);
                            affectedRows += cmdPP.ExecuteNonQuery();
                        }

                        string queryPF = @"UPDATE premio_presencia_fisica_t 
                                   SET presencia_f = @presencia_f, 
                                       dias_presencia_f = @diasPF, 
                                       importe = @importePF, 
                                       uso = @uso
                                   WHERE id = @id AND numero_empleado = @numEmp";

                        using (SqlCommand cmdPF = new SqlCommand(queryPF, Conex.nomi, trans))
                        {
                            cmdPF.Parameters.AddWithValue("@numEmp", aPF.NumberEmployee);
                            cmdPF.Parameters.AddWithValue("@presencia_f", aPF.PF);
                            cmdPF.Parameters.AddWithValue("@diasPF", aPF.DaysPF);
                            cmdPF.Parameters.AddWithValue("@importePF", aPF.AmountPF);
                            cmdPF.Parameters.AddWithValue("@uso", aPF.Use);
                            cmdPF.Parameters.AddWithValue("@id", aPF.Id);
                            affectedRows += cmdPF.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            finally
            {
                Conex.CloseNomina();
            }
            return affectedRows;
        }

        public int DeleteAwardEmployee(int Id)
        {
            int affectedRows = 0;
            try
            {
                Conex.OpenNomina();
                using (SqlTransaction trans = Conex.nomi.BeginTransaction())
                {
                    try
                    {
                        string queryPP = "DELETE FROM premio_puntualidad_t WHERE id = @id";
                        using (SqlCommand cmdPP = new SqlCommand(queryPP, Conex.nomi, trans))
                        {
                            cmdPP.Parameters.AddWithValue("@id", Id);
                            affectedRows += cmdPP.ExecuteNonQuery();
                        }

                        string queryPF = "DELETE FROM premio_presencia_fisica_t WHERE id = @id ";
                        using (SqlCommand cmdPF = new SqlCommand(queryPF, Conex.nomi, trans))
                        {
                            cmdPF.Parameters.AddWithValue("@id", Id);
                            affectedRows += cmdPF.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            finally
            {
                Conex.CloseNomina();
            }
            return affectedRows;
        }

        public int SaveAwardOverview(int payWeek)
        {
            try
            {
                Conex.OpenNomina();

                using (SqlTransaction trans = Conex.nomi.BeginTransaction())
                {
                    try
                    {
                        string queryPP = @"SELECT 
                        pp.numero_empleado,
                        pp.dias_puntualidad,
                        pp.importe AS monto_pp,
                        pf.dias_presencia_f,
                        pf.importe AS monto_pf,
                        pp.uso
                    INTO #PremiosProcesados
                    FROM premio_puntualidad_t pp
                    INNER JOIN premio_presencia_fisica_t pf 
                        ON pp.numero_empleado = pf.numero_empleado AND pp.uso = pf.uso
                    INNER JOIN empleados e ON pp.numero_empleado = e.numero_empleado
                    INNER JOIN categorias c ON IIF(@ciclo = 'Zafra', e.categoria_zafra, e.categoria_reparacion) = c.id_categoria;

                    INSERT INTO premio_presencia_fisica (id_nomina, ciclo, semana_pago, numero_empleado, dias_presencia_f, importe, uso, id_concepto)
                    SELECT 1, @ciclo, @semana_pago, numero_empleado, dias_presencia_f, monto_pf, uso, 16 
                    FROM #PremiosProcesados;

                    INSERT INTO premio_puntualidad (id_nomina, ciclo, semana_pago, numero_empleado, dias_puntualidad, importe, uso, id_concepto)
                    SELECT 1, @ciclo, @semana_pago, numero_empleado, dias_puntualidad, monto_pp, uso, 17 
                    FROM #PremiosProcesados;

                    DROP TABLE #PremiosProcesados;";

                        using (SqlCommand cmd = new SqlCommand(queryPP, Conex.nomi, trans))
                        {
                            cmd.Parameters.AddWithValue("@ciclo", _Cycle);
                            cmd.Parameters.AddWithValue("@semana_pago", payWeek);
                     
                            cmd.ExecuteNonQuery();
                        }
                   
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            finally
            {
                Conex.CloseNomina();
            }
        }

        public List<AwardReport> GetDataPrintAwards(int payWeek, bool temp)
        {
            List<AwardReport> list = new List<AwardReport>();
            string query = "";

            if (temp)
            {
                query = @"SELECT ppf.numero_empleado,CONCAT(e.nombre,' ',e.apellido_paterno,' ',e.apellido_materno) as n_completo,
                            CASE WHEN @ciclo = 'Zafra' THEN e.categoria_zafra ELSE e.categoria_reparacion END AS categoria,
                            ppf.presencia_f AS pf,
                            ppf.importe AS importePPF,
                            pp.puntualidad AS pp,
                            pp.importe AS importePP,
                            ppf.uso
                            FROM premio_presencia_fisica_t ppf
                            INNER JOIN premio_puntualidad_t pp
                            ON ppf.numero_empleado = pp.numero_empleado
                            INNER JOIN empleados e
                            ON pp.numero_empleado = e.numero_empleado
                            INNER JOIN categorias c
                            ON c.id_categoria = (CASE WHEN @ciclo = 'Zafra' THEN e.categoria_zafra ELSE e.categoria_reparacion END)";
            }
            else
            {
                query = @"SELECT  
                            ppf.numero_empleado,
                            CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) AS n_completo,
                            CASE WHEN @ciclo = 'Zafra' THEN e.categoria_zafra ELSE e.categoria_reparacion END AS categoria,
                            ppf.dias_presencia_f AS pf,
                            ppf.importe AS importePPF,
                            ISNULL(pp.dias_puntualidad, 0) AS pp,
                            ISNULL(pp.importe, 0) AS importePP,
                            ppf.uso
                        FROM premio_presencia_fisica ppf
                        INNER JOIN empleados e 
                            ON ppf.numero_empleado = e.numero_empleado
                        LEFT JOIN premio_puntualidad pp
                            ON ppf.numero_empleado = pp.numero_empleado
                            AND pp.semana_pago = @semana_pago 
                        INNER JOIN categorias c
                            ON c.id_categoria = (CASE WHEN @ciclo = 'Zafra' THEN e.categoria_zafra ELSE e.categoria_reparacion END)
                        WHERE ppf.semana_pago = @semana_pago;";
            }

            try
            {

                using (SqlCommand cmd = new SqlCommand(query, Conex.nomi))
                {
                    cmd.Parameters.AddWithValue("@ciclo", _Cycle);
                    cmd.Parameters.AddWithValue("@semana_pago", payWeek);

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
                throw new Exception("Error al obtener los premios: " + ex.Message);
            }
            finally
            {
                Conex.CloseNomina();
            }

            return list;
        }

        public decimal GetSalaryEmployee(int NumberEmployee)
        {
            using (cmd = new SqlCommand(@"SELECT c.salario
                                        FROM empleados e
                                        INNER JOIN categorias c 
                                        ON IIF(@ciclo = 'Zafra', e.categoria_zafra, e.categoria_reparacion) = c.id_categoria
                                        WHERE e.numero_empleado = @n_employee", Conex.nomi))

            {
                cmd.Parameters.AddWithValue("@ciclo", _Cycle);
                cmd.Parameters.AddWithValue("@n_employee", NumberEmployee);
                Conex.OpenNomina();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        private LastAward ShowLastAward(SqlDataReader reader)
        {
            return new LastAward
            {
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                DaysPF = Convert.ToDecimal(reader["dias_presencia_f"]),
                LastAmountPF = Convert.ToDecimal(reader["importePPF"]),
                DaysPP = Convert.ToDecimal(reader["dias_puntualidad"]),
                LastAmountPP = Convert.ToDecimal(reader["importePP"]),
                PayWeek = Convert.ToInt32(reader["semana_pago"]),

            };

        }

        private ListAttend ShowListAttend(SqlDataReader reader)
        {
            return new ListAttend
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                Date = Convert.ToDateTime(reader["fecha"]),
                //AppplyPF = Convert.ToBoolean(reader["aplica_pf"]),
                ApplyPF = true,
                ApplyPP = Convert.ToBoolean(reader["aplica_pp"])
            };
        }

        private PieceworkAward ShowPieceAward(SqlDataReader reader)
        {
            return new PieceworkAward
            {
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                FullName = Convert.ToString(reader["n_completo"]),
                CategoryId = Convert.ToInt32(reader["categoria"]),
                Salary = Convert.ToDecimal(reader["salario"]),
                Piecework = Convert.ToDecimal(reader["destajo"]),
                FinalSalary = Convert.ToDecimal(reader["salario_final"])
            };
        }

        private AwardPPF ShowAwardOverview(SqlDataReader reader)
        {
            return new AwardPPF
            {
                Id = Convert.ToInt32(reader["id"]),
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),
                Salary = Convert.ToDecimal(reader["salario"]),
                DaysPF = Convert.ToInt32(reader["days_pf"]),
                PF = Convert.ToDecimal(reader["presencia_f"]),
                AmountPF = Convert.ToDecimal(reader["monto_pf"]),
                DaysPP = Convert.ToInt32(reader["days_pp"]),
                PP = Convert.ToDecimal(reader["puntualidad"]),
                AmountPP = Convert.ToDecimal(reader["monto_pp"]),
                Use = Convert.ToInt32(reader["uso"]),
                Comments = Convert.ToString("")
            };
        }

        private AwardReport ShowDataReport(SqlDataReader reader)
        {
            return new AwardReport
            {
                NumberEmployee = Convert.ToInt32(reader["numero_empleado"]),  
                FullName = Convert.ToString(reader["n_completo"]),
                CategoryId = Convert.ToInt32(reader["categoria"]),
                PF = Convert.ToDecimal(reader["pf"]),
                AmountPF = Convert.ToDecimal(reader["importePPF"]),
                PP = Convert.ToDecimal(reader["pp"]),
                AmountPP = Convert.ToDecimal(reader["importePP"]),
                Use = Convert.ToInt32(reader["uso"]),
            };
        }

        private decimal GetTabularValue(decimal currentValue, int daysApplied, int monthlyTarget)
        {
  
            if (daysApplied < monthlyTarget)
            {
                return 0m;
            }

            switch (currentValue)
            {
                case 0m:
                    return 0.5m;
                case 0.5m:
                    return 1.5m;
                case 1.5m:
                    return 2.5m;
                case 2.5m:
                case 3.0m:
                    return 3.0m;
                default:
                    return 3.0m;
            }
        }

    }
}
