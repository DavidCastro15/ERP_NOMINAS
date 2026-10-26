using ERP_NOMINAS.Models.PayRollProcess.CalculatedPayRolls.UnionWorker.Reparacion;
using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Repositorys.PayRollProcess.CalculationsPayRoll.UnionWorker.Reparacion
{
    public class ProcessPerceptionRepositoryUWR : IProcessPayRollPerceptionUWR
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        private int ExecuteQuery(Action<SqlCommand, SqlTransaction> action)
        {
            SqlTransaction tra = null;
            try
            {
                Conex.OpenNomina();
                tra = Conex.nomi.BeginTransaction();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Conex.nomi;
                    cmd.Transaction = tra;

                    action(cmd, tra);
                }

                tra.Commit();
                return 1;
            }
            catch (Exception)
            {
                tra?.Rollback();
                throw;
            }
            finally
            {
                Conex.nomi.Close();
            }
        }

        public int Aguinaldo(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                 (select 1, aguip.id_empleado, aguip.no_periodo, aguip.ciclo, 0, 15, aguip.importe_total, @fecha2, 0, 0, 0, 1 from aguinaldo_pagar aguip
                  where aguip.no_periodo = @semana)";

                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.ExecuteNonQuery();
            });
        }

        public int LimpiarInfo()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = "TRUNCATE TABLE resultado_sind_indiv_det_previo";
                cmd.ExecuteNonQuery();
            });
        }

        public int ActualizandoPlazas()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"UPDATE rp SET rp.tipo_planta = e.tipo
                                FROM resultado_sind_indiv_det_previo rp
                                INNER JOIN empleados e
                                ON rp.numero_empleado = e.numero_empleado
                                AND rp.tipo_planta IS NULL;";
                cmd.ExecuteNonQuery();
            });
        }

        public int AlimentosDiasNormalT1X2X3(string Cycle)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 32, (cate.salario / 16), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join empleados emple on resid.numero_empleado = emple.numero_empleado
                         join categorias cate on resid.id_categoria = cate.id_categoria where (resid.id_concepto = 1 or resid.id_concepto = 2) and (resid.turno = 1 or resid.turno = 2 or resid.turno = 3) and cate.alimentos = 'Si' and @ciclo = 'Zafra')";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);

                cmd.ExecuteNonQuery();
            });
        }

        public int AlimentosDomingosT1X2X3(string Cycle)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 32, (cate.salario / 16), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join empleados emple on resid.numero_empleado = emple.numero_empleado
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 3 and (resid.turno = 1 or resid.turno = 2 or resid.turno = 3) and cate.alimentos = 'Si' and @ciclo = 'Zafra')";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);

                cmd.ExecuteNonQuery();
            });
        }

        public int AlimentosFestivosTrabajadosT1X2X3(string Cycle)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 32, (cate.salario / 16), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join empleados emple on resid.numero_empleado = emple.numero_empleado
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 5 and (resid.turno = 1 or resid.turno = 2 or resid.turno = 3) and cate.alimentos = 'Si' and @ciclo = 'Zafra')";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);

                cmd.ExecuteNonQuery();
            });
        }

        public int Devoluciones(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion, exento)
                            (select 1, d.numero_empleado, @semana, @ciclo, 0, 9, d.importe, @fecha2, 0, 0, 0, 1, d.importe from devoluciones d
                            where d.semana_pago =@semana )";
                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int AlturasTemperaturasDiasNormalesTX1X2X3()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 10, (cate.salario / 16), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where (resid.id_concepto = 1 or resid.id_concepto = 2) and (resid.turno = 1 or resid.turno = 2 or resid.turno = 3) and cate.alturas_temp = 'Si')";

                cmd.ExecuteNonQuery();
            });
        }

        public int AlturasTemperaturasDocumentadas(int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select alte.id_nomina, altd.numero_empleado, alte.semana_pago, alte.ciclo, altd.id_categoria, alte.id_percepcion, (cate.salario / 8 * altd.horas), alte.fecha, 0, altd.uso, 0,1  from alturas_temp_encabezado alte
                         join alturas_temp_detalle altd on alte.folio = altd.folio
                         join categorias cate on altd.id_categoria = cate.id_categoria
                         where alte.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int AlturasTemperaturasDomingosTX1X2X3()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 10, (cate.salario / 16), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 3 and (resid.turno = 1 or resid.turno = 2 or resid.turno = 3) and cate.alturas_temp = 'Si')";

                cmd.ExecuteNonQuery();
            });
        }

        public int AlturasTemperaturasFestivosTX1X2X3()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 10, (cate.salario / 16), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 5 and (resid.turno = 1 or resid.turno = 2 or resid.turno = 3) and cate.alturas_temp = 'Si')";

                cmd.ExecuteNonQuery();
            });
        }

        public int ApoyoTransporte(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion, exento)
        (select 1, apoyot.numero_empleado, apoyot.semana_pago, @ciclo, 0, 39, apoyot.importe, @fecha2, 0, 0, 0, 1, apoyot.importe from apoyo_transporte apoyot
         where apoyot.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int BonoDomingosDescansos(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion,gravado)
        (select 1, domd.empleado, domd.semana_pago, 'Reparación', 0, 21, domd.domingos_descanso, @fecha2, 0, 0, 0, 1,domd.domingos_descanso from dom_descanso_a_pagar domd
         where domd.semana_pago = @semana AND domd.domingos_descanso >0)";

                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int BonoPTU(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
        (select 1, bonopt.numero_empleado, bonopt.semana_pago, 'Reparación', 0, 19, bonopt.importe, @fecha2, 0, 0, 0, 1 from bono_ptu bonopt
         where bonopt.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int CargasxDescargas(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select 1, topo.id_empleado, topo.semana_pago, 'Zafra', 0, 18, topo.importe, @fecha2, 0, 0, 0, 1 from topos topo
                         where topo.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int Comisiones(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                                (select 1, comi.numero_empleado, @semana, @ciclo, 0, 11, cate.salario * 3 *(comi.porcentaje/100), @fecha2, 0, comi.uso, 0, 1 from comite_sindical comi
                                join categorias cate on comi.categoria_reparacion = cate.id_categoria where @ciclo = 'Reparación')";

                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.ExecuteNonQuery();
            });
        }

        public int Compensaciones(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select compen.id_nomina, compen.numero_empleado, compen.periodo, compen.ciclo, 0, 12, compen.importe, @fecha2, 0, compen.uso, 0, 1 from compensaciones compen
                        where compen.periodo = @semana)";

                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.ExecuteNonQuery();
            });
        }

        public int Destajos(int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select deste.id_nomina, destd.numero_empleado, deste.semana_pago, deste.ciclo, 0, deste.id_percepcion, (deste.tarifa_tonelada * deste.toneladas_cargadas / deste.cantidad_cargadores), deste.fecha, deste.turno, deste.uso, 0, 1 from destajos deste
                         join destajos_detalle destd on deste.folio = destd.folio where deste.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int DomingosTrabajados(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
        (select 1, domir.empleado, domir.no_periodo, @ciclo, 0, 21, domir.importe, @fecha2, 0, 0, 0, 1 from dom_descanso_resumen domir
         where domir.no_periodo = @semana)";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);

                cmd.ExecuteNonQuery();
            });
        }

        public int FestivoNoLaborado(string Cycle, DateTime StartDate, DateTime EndDate, int NoWorkinDays)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"select @dias_inhabiles = count(fecha) from festivos where fecha between @fecha1 and @fecha2

                        if @dias_inhabiles > 0 begin

                        IF @ciclo = 'Zafra' begin

                        Update resultado_sind_indiv_det_previo set
                         dias_categoria_igual = (select count(t2.id_categoria) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and (t2.id_concepto = 1 or t2.id_concepto = 5) and resid1.id_categoria = t2.id_categoria
						                        and datepart(weekday,resid1.fecha) <> 1 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha)),
                         dias_depto_igual = (select count(t2.uso) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and (t2.id_concepto = 1 or t2.id_concepto = 5) and Round(resid1.uso/1000000,0) = Round(t2.uso/1000000,0)
						                        and datepart(weekday,resid1.fecha) <> 1 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha))
                         from resultado_sind_indiv_det_previo resid1
                        where numero_empleado = resid1.numero_empleado and (id_concepto = 1 or id_concepto = 2 or id_concepto = 5 or id_concepto = 8 or id_concepto = 10 or id_concepto = 32)
                        end
                        if @ciclo = 'Reparación' begin
                        Update resultado_sind_indiv_det_previo set
                         dias_categoria_igual = (select count(t2.id_categoria) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and (t2.id_concepto = 1 or t2.id_concepto = 5) and resid1.id_categoria = t2.id_categoria
						                        and datepart(weekday,resid1.fecha) <> 1 and  datepart(weekday,resid1.fecha) <> 7 AND Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha)),
                         dias_depto_igual = (select count(t2.uso) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and (t2.id_concepto = 1 or t2.id_concepto = 5) and Round(resid1.uso/1000000,0) = Round(t2.uso/1000000,0)
						                        and datepart(weekday,resid1.fecha) <> 1 and datepart(weekday,resid1.fecha) <> 7 AND Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha))
                         from resultado_sind_indiv_det_previo resid1
                        where numero_empleado = resid1.numero_empleado and (id_concepto = 1 or id_concepto = 2 or id_concepto = 5 or id_concepto = 8 or id_concepto = 10 or id_concepto = 32)
                        end 

                        Update resultado_sind_indiv_det_previo set
                        festivo_no_lab_depto_flag = (iif(Exists(select * from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and t2.dias_depto_igual >= 3 AND Exists(select fest.fecha from festivos fest where fest.fecha between @fecha1 AND @fecha2)),1,0))
                        from resultado_sind_indiv_det_previo resid1
                        where numero_empleado = resid1.numero_empleado and (id_concepto = 1 or id_concepto = 2)

                        Update resultado_sind_indiv_det_previo set
                        festivo_no_lab_categ_flag = (iif(Exists(select * from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and t2.dias_categoria_igual >= 3 AND Exists(select fest.fecha from festivos fest where fest.fecha between @fecha1 AND @fecha2)),1,0))
                        from resultado_sind_indiv_det_previo resid1
                        where numero_empleado = resid1.numero_empleado and (id_concepto = 1 or id_concepto = 2)

	                    Update resultado_sind_indiv_det_previo set festivo_no_laborado = (
	                    iif(festivo_no_lab_depto_flag = 1 or festivo_no_lab_categ_flag = 1,iif(resid1.turno = 1 or resid1.turno = 4 or resid1.turno = 5, resid1.importe / resid1.dias_normales, categ.salario_turno2 / resid1.dias_normales),0))
	                    from resultado_sind_indiv_det_previo resid1 join categorias categ on resid1.id_categoria = categ.id_categoria
	                    where id_concepto = 1


	                    Update resultado_sind_indiv_det_previo set festivo_no_laborado = (
	                    iif(festivo_no_lab_depto_flag = 1 or festivo_no_lab_categ_flag = 1,(categ.salario_turno2 - resid1.importe) / resid1.dias_normales,0))
	                    from resultado_sind_indiv_det_previo resid1 join categorias categ on resid1.id_categoria = categ.id_categoria
	                    where id_concepto = 2

                        insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 38, resid.festivo_no_laborado, resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                        where (resid.id_concepto = 1 or resid.id_concepto = 2)  and resid.festivo_no_laborado > 0)

                        end";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha1", StartDate);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@dias_inhabiles", NoWorkinDays);
                cmd.ExecuteNonQuery();
            });
        }

        public int FestivosTrabajados()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set id_concepto = 5, importe = (importe * 2) from resultado_sind_indiv_det_previo resd
                        join festivos fest on resd.fecha = fest.fecha
                        where (id_concepto = 1 or id_concepto = 2)";

                cmd.ExecuteNonQuery();
            });
        }

        public int GratificacionXDesgasteHerramienta(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select grati.id_nomina, grati.numero_empleado, grati.periodo, grati.ciclo, 0, 26, grati.importe, @fecha2, 0, grati.uso, 0, 1 from gratificaciones grati
                        where grati.periodo = @semana)";

                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int Gravadas100()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set
	                    total_percepciones = importe, Total_percepciones50 = 0, Umas = 0, gravado = importe, exento = 0, otros_pagos = 0
                         where
                         id_concepto <> 3
                         and id_concepto <> 4
                         and id_concepto <> 5

                         and id_concepto <> 14

                         and id_concepto <> 22
                         and id_concepto <> 23
                         and id_concepto <> 24

                         and id_concepto <> 27
                         and id_concepto <> 29
                         and id_concepto <> 34
                         and id_concepto <> 39
                         and id_concepto <> 41
                         and id_concepto <> 44";

                cmd.ExecuteNonQuery();
            });
        }

        public int Exentos100()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set
	                    total_percepciones = importe, Total_percepciones50 = 0, Umas = 0, gravado = 0, exento = importe, otros_pagos = 0
                         where
                         id_concepto = 9";

                cmd.ExecuteNonQuery();
            });
        }

        public int GravadosExentos()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set
	                    total_percepciones = 0, Total_percepciones50 = 0, Umas = 0, gravado = 0, exento = 0, otros_pagos = 0";

                cmd.ExecuteNonQuery();
            });
        }

        public int HorasElaboracion(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select 1, horae.id_empleado, horae.semana_pago, 'Zafra', 0, 42, horae.importe, @fecha2, 0, 0, 0, 1 from horas_elaboracion horae
                         where horae.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int IdentificandoDomingosTrabajados()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set id_concepto = 3, importe = (importe * 2) where datepart(dw,fecha) = 1 and (id_concepto = 1 or id_concepto = 2)";

                cmd.ExecuteNonQuery();
            });
        }

        public int Listas1X2(DateTime StartDate, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion,tipo_de_lista)
                                                (select lise.id_nomina, lisd.numero_empleado, lise.semana_pago, lise.periodo, lisd.categoria_requerida, 1, iif(lise.estatus = 'Normal', cate.salario, cate.salario * 0.6666), lise.fecha, lisd.turno_trabajado, lisd.uso, 0, 1 ,lise.estatus from listas_detalle lisd join listas_encabezado lise on lisd.numero_control = lise.numero_control
                                                 join categorias cate on lisd.categoria_requerida = cate.id_categoria where lisd.asistencia = 1 and lise.fecha between @fecha1 and @fecha2 AND lise.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@fecha1", StartDate);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int Listas2X2(DateTime StartDate, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion,tipo_de_lista)
                                            (select lise.id_nomina, lisd.numero_empleado, lise.semana_pago, lise.periodo, lisd.categoria_requerida, 2, iif(lise.estatus = 'Normal', cate.salario, cate.salario * 0.6666), lise.fecha, lisd.turno_trabajado, lisd.uso, 0, 1,lise.estatus from listas_detalle lisd join listas_encabezado lise on lisd.numero_control = lise.numero_control
                                             join categorias cate on lisd.categoria_requerida = cate.id_categoria where lisd.asistencia = 2 and lise.fecha between @fecha1 and @fecha2 AND lise.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@fecha1", StartDate);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int Mixtas(int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set
	                    total_percepciones = (select sum(t2.importe) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and (t2.id_concepto = 3 or t2.id_concepto = 5  or t2.id_concepto = 9) and t2.semana_pago = @semana),
	                    total_percepciones50 = (select sum(t2.importe) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and (t2.id_concepto = 3 or t2.id_concepto = 5 or t2.id_concepto = 9) and t2.semana_pago = @semana) / 2, 
	                    Umas = (select valor_uma from parametros where registro = 1) * 5, 
	                    gravado = 0, 
	                    exento = 0,
	                    otros_pagos = 0
	                    from resultado_sind_indiv_det_previo resid1
                         where
                         id_concepto = 3
                         or id_concepto = 5

                         Update resultado_sind_indiv_det_previo set
	                    gravado = iif(total_percepciones50 > umas, ((total_percepciones - umas) * importe / total_percepciones), ((total_percepciones / 2) * importe / total_percepciones)), 
	                    exento = importe - iif(total_percepciones50 > umas, ((total_percepciones - umas) * importe / total_percepciones), ((total_percepciones / 2) * importe / total_percepciones))
                         where
                         id_concepto = 3
                         or id_concepto = 5";

                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int MixtasPrimaDominical(int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set
	                    total_percepciones = (select sum(t2.importe) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and t2.id_concepto = 4 and t2.semana_pago = @semana),
	                    total_percepciones50 = 0, 
	                    Umas = (select valor_uma from parametros where registro = 1), 
	                    gravado = 0, 
	                    exento = 0,
	                    otros_pagos = 0
	                    from resultado_sind_indiv_det_previo resid1
                        where
                        id_concepto = 4

                        Update resultado_sind_indiv_det_previo set
	                    gravado = iif(total_percepciones > umas, ((total_percepciones - umas) * importe / total_percepciones), 0), 
	                    exento = importe - iif(total_percepciones > umas, ((total_percepciones - umas) * importe / total_percepciones), 0)
                        where
                        id_concepto = 4";

                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int PremioPP(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select 1, prep.numero_empleado, @semana, @ciclo, 0, prep.id_concepto, prep.importe, @fecha2, 0, prep.uso, 0, 1 from premio_puntualidad prep
                        where prep.semana_pago = @semana AND prep.importe > 0)";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int PremioPPF(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select 1, prepf.numero_empleado, @semana, @ciclo, 0, prepf.id_concepto, prepf.importe, @fecha2, 0, prepf.uso, 0, 1 from premio_presencia_fisica prepf
                        where prepf.semana_pago = @semana AND prepf.importe > 0)";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int PrimaDominical()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                                    (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 4, ((importe/2) * 0.25), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid where resid.id_concepto = 3)";

                cmd.ExecuteNonQuery();
            });
        }

        public int Retroactivo(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion, exento)
        (select 1, rp.numero_empleado, @semana, @ciclo, 0, 43, rp.retroactivo, @fecha2, 0, 0, 0, 1, rp.retroactivo from retroactivo_pagar rp 
        where rp.semana_pago =@semana)";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int SeptimoDia(string Cycle, DateTime StartDate, DateTime EndDate, int PayWeek ,int EfectiveDays)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @" Update resultado_sind_indiv_det_previo set
                         dias_normales = (select count(t2.id_concepto) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and t2.id_concepto = 1 and t2.semana_pago = @semana),
                         dias_tiempo_legal = (select count(t2.id_concepto) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and t2.id_concepto = 8 and t2.semana_pago = @semana
					                        and datepart(weekday,t2.fecha) <> 1 and datepart(weekday,t2.fecha) <> 7 and Not Exists(select fest.fecha from festivos fest where fest.fecha = t2.fecha)),
                         dias_alimentos = (select count(t2.id_concepto) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and t2.id_concepto = 32 and t2.semana_pago = @semana and Not Exists(select fest.fecha from festivos fest where fest.fecha = t2.fecha)),
                         dias_alturas = (select count(t2.id_concepto) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and t2.id_concepto = 10 and t2.semana_pago = @semana and Not Exists(select fest.fecha from festivos fest where fest.fecha = t2.fecha))
                         from resultado_sind_indiv_det_previo resid1
                        where numero_empleado = resid1.numero_empleado and
                        ((id_concepto = 1) or (id_concepto = 2)
                         or (id_concepto = 10 and datepart(weekday,resid1.fecha) <> 1 and datepart(weekday,resid1.fecha) <> 7 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha))
                         or (id_concepto = 8 and datepart(weekday,resid1.fecha) <> 1 and datepart(weekday,resid1.fecha) <> 7 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha))
                         or (id_concepto = 32 and datepart(weekday,resid1.fecha) <> 1 and datepart(weekday,resid1.fecha) <> 7 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha)))

                        if @ciclo = 'Zafra' begin
                        select @dias_efectivos = 6 - count(fecha) from festivos where fecha between @fecha1 and @fecha2 and datepart(weekday,fecha) <> 1
                        end
                         else begin
                         select @dias_efectivos = 5 - count(fecha) from festivos where fecha between @fecha1 and @fecha2 and datepart(weekday,fecha) <> 1 and datepart(weekday,fecha) <> 7
                         end



                            if @dias_efectivos = 6 begin
	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 6,(importe * 0.16667),
	                            iif(dias_normales = 5,(importe * 0.20000),
	                            iif(dias_normales = 4,(importe * 0.18750),
	                            iif(dias_normales = 3,(importe * 0.16667),
	                            iif(dias_normales = 2,(importe * 0.16667),
	                            iif(dias_normales = 1,(importe * 0.16667),0)))))))
	                            where id_concepto = 1

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 6,(importe * 0.16667),
	                            iif(dias_normales = 5,(importe * 0.20000),
	                            iif(dias_normales = 4,(importe * 0.18750),
	                            iif(dias_normales = 3,(importe * 0.16667),
	                            iif(dias_normales = 2,(importe * 0.16667),
	                            iif(dias_normales = 1,(importe * 0.16667),0)))))))
	                            from resultado_sind_indiv_det_previo resid1
	                            where id_concepto = 8 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha)

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 6,(importe * 0.16667),
	                            iif(dias_normales = 5,(importe * 0.20000),
	                            iif(dias_normales = 4,(importe * 0.18750),
	                            iif(dias_normales = 3,(importe * 0.16667),
	                            iif(dias_normales = 2,(importe * 0.16667),
	                            iif(dias_normales = 1,(importe * 0.16667),0)))))))
	                            where id_concepto = 32

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 6,(importe * 0.16667),
	                            iif(dias_normales = 5,(importe * 0.20000),
	                            iif(dias_normales = 4,(importe * 0.18750),
	                            iif(dias_normales = 3,(importe * 0.16667),
	                            iif(dias_normales = 2,(importe * 0.16667),
	                            iif(dias_normales = 1,(importe * 0.16667),0)))))))
	                            where id_concepto = 10

                            end

                            if @dias_efectivos = 5 begin
	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 5,(importe * 0.20000),
	                            iif(dias_normales = 4,(importe * 0.25000),
	                            iif(dias_normales = 3,(importe * 0.25000),
	                            iif(dias_normales = 2,(importe * 0.20000),
	                            iif(dias_normales = 1,(importe * 0.20000),0))))))
	                            where id_concepto = 1

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 5,(importe * 0.20000),
	                            iif(dias_normales = 4,(importe * 0.25000),
	                            iif(dias_normales = 3,(importe * 0.25000),
	                            iif(dias_normales = 2,(importe * 0.20000),
	                            iif(dias_normales = 1,(importe * 0.20000),0))))))
	                            from resultado_sind_indiv_det_previo resid1
	                            where id_concepto = 8 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha)

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 5,(importe * 0.20000),
	                            iif(dias_normales = 4,(importe * 0.25000),
	                            iif(dias_normales = 3,(importe * 0.25000),
	                            iif(dias_normales = 2,(importe * 0.20000),
	                            iif(dias_normales = 1,(importe * 0.20000),0))))))
	                            where id_concepto = 32

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 5,(importe * 0.20000),
	                            iif(dias_normales = 4,(importe * 0.25000),
	                            iif(dias_normales = 3,(importe * 0.25000),
	                            iif(dias_normales = 2,(importe * 0.20000),
	                            iif(dias_normales = 1,(importe * 0.20000),0))))))
	                            where id_concepto = 10
                            end

                            if @dias_efectivos = 4 begin
	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 4,(importe * 0.25000),
	                            iif(dias_normales = 3,(importe * 0.33333),
	                            iif(dias_normales = 2,(importe * 0.37500),
	                            iif(dias_normales = 1,(importe * 0.25000),0)))))
	                            where id_concepto = 1

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 4,(importe * 0.25000),
	                            iif(dias_normales = 3,(importe * 0.33333),
	                            iif(dias_normales = 2,(importe * 0.37500),
	                            iif(dias_normales = 1,(importe * 0.25000),0)))))
	                            from resultado_sind_indiv_det_previo resid1
	                            where id_concepto = 8 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha)

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 4,(importe * 0.25000),
	                            iif(dias_normales = 3,(importe * 0.33333),
	                            iif(dias_normales = 2,(importe * 0.37500),
	                            iif(dias_normales = 1,(importe * 0.25000),0)))))
	                            where id_concepto = 32

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 4,(importe * 0.25000),
	                            iif(dias_normales = 3,(importe * 0.33333),
	                            iif(dias_normales = 2,(importe * 0.37500),
	                            iif(dias_normales = 1,(importe * 0.25000),0)))))
	                            where id_concepto = 10
                            end

                            if @dias_efectivos = 3 begin
	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 3,(importe * 0.33333),
	                            iif(dias_normales = 2,(importe * 0.50000),
	                            iif(dias_normales = 1,(importe * 0.75000),0))))
	                            where id_concepto = 1

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 3,(importe * 0.33333),
	                            iif(dias_normales = 2,(importe * 0.50000),
	                            iif(dias_normales = 1,(importe * 0.75000),0))))
	                            from resultado_sind_indiv_det_previo resid1
	                            where id_concepto = 8 and Not Exists(select fest.fecha from festivos fest where fest.fecha = resid1.fecha)

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 3,(importe * 0.33333),
	                            iif(dias_normales = 2,(importe * 0.50000),
	                            iif(dias_normales = 1,(importe * 0.75000),0))))
	                            where id_concepto = 32

	                            Update resultado_sind_indiv_det_previo set septimo_dia = (
	                            iif(dias_normales = 3,(importe * 0.33333),
	                            iif(dias_normales = 2,(importe * 0.50000),
	                            iif(dias_normales = 1,(importe * 0.75000),0))))
	                            where id_concepto = 10
                            end";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha1", StartDate);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.Parameters.AddWithValue("@dias_efectivos", EfectiveDays);
                cmd.ExecuteNonQuery();
            });
        }

        public int SeptimoDiaX2()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 36, resid.septimo_dia, resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                        where (resid.id_concepto = 1 or resid.id_concepto = 8 or resid.id_concepto = 32 or resid.id_concepto = 10) and resid.septimo_dia > 0)";
             
                cmd.ExecuteNonQuery();
            });
        }

        public int SeptimoDiaVariable(string Cycle, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set
                        dias_adicionales = (select count(t2.id_concepto) from resultado_sind_indiv_det_previo t2 where resid1.numero_empleado = t2.numero_empleado and t2.id_concepto = 2 and t2.semana_pago = @semana
                        and datepart(weekday,t2.fecha) <> 1)
                        from resultado_sind_indiv_det_previo resid1
                        where numero_empleado = resid1.numero_empleado and id_concepto = 2 and datepart(weekday,resid1.fecha) <> 1
                        if @ciclo = 'Zafra' begin
	                        Update resultado_sind_indiv_det_previo set importe_adicionales = (
	                        iif(dias_adicionales = 6,(importe * 0.16667),
	                        iif(dias_adicionales = 5,(importe * 0.20000),
	                        iif(dias_adicionales = 4,(importe * 0.18750),
	                        iif(dias_adicionales = 3,(importe * 0.16667),
	                        iif(dias_adicionales = 2,(importe * 0.12500),
	                        iif(dias_adicionales = 1,(importe * 0.12500),0)))))))
	                        where id_concepto = 2
                        end

                        if @ciclo = 'Reparación' begin
	                        Update resultado_sind_indiv_det_previo set importe_adicionales = (
	                        iif(dias_adicionales = 5,(importe * 0.40000),
	                        iif(dias_adicionales = 4,(importe * 0.50000),
	                        iif(dias_adicionales = 3,(importe * 0.50000),
	                        iif(dias_adicionales = 2,(importe * 0.40000),
	                        iif(dias_adicionales = 1,(importe * 0.40000),0))))))
	                        where id_concepto = 2

                        end

                        insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 33, resid.importe_adicionales, resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                        where id_concepto = 2 and importe_adicionales > 0)";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int SextoDia()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 37, resid.septimo_dia, resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                        where (resid.id_concepto = 1 or resid.id_concepto = 8 or resid.id_concepto = 32 or resid.id_concepto = 10) and resid.septimo_dia > 0)";

                cmd.ExecuteNonQuery();
            });
        }

        public int Subsidios(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select subs.id_nomina, subs.numero_empleado, subs.semana_pago, subs.periodo, 0, subs.id_percepcion, subs.importe, @fecha2, 0, subs.uso, 0, 1 from subsidios subs
                        where subs.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalDomingosTrabajadosT2X6()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, (cate.salario / 7.5), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 3 and (resid.turno = 2 or resid.turno = 6))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalDomingosTrabajadosT3X7()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, ((cate.salario / 7) * 2), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 6 and (resid.turno = 3 or resid.turno = 7))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalDomingosTrabajadosT3X7_2()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, ((cate.salario / 7) * 2), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 3 and (resid.turno = 3 or resid.turno = 7))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalFestivoTrabajado2x6()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, (cate.salario / 7.5), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 5 and (resid.turno = 2 or resid.turno = 6))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalFestivosTrabajadoT3X7()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, ((cate.salario / 7) * 2), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 5 and (resid.turno = 3 or resid.turno = 7))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalNormalT6()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, iif (cate.alimentos = 'Si', (cate.salario_promedio_t1y2 - cate.salario - (cate.salario / 16)),(cate.salario_promedio_t1y2 - cate.salario)), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 1 and (resid.turno = 6))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalNormalT7()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, iif (cate.alimentos = 'Si',(cate.salario_turno3 - cate.salario - (cate.salario / 16)), (cate.salario_turno3 - cate.salario)), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 1 and (resid.turno = 7))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalSabadoTrabajadoT2X6()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, (cate.salario / 7.5), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where resid.id_concepto = 6 and (resid.turno = 2 or resid.turno = 6))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalT1()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, (cate.salario_turno1 - cate.salario), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where (resid.id_concepto = 1 or resid.id_concepto = 2)  and (resid.turno = 1) and (cate.salario_turno1 - cate.salario) > 0 )";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalT2()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, (cate.salario_turno2 - cate.salario), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where (resid.id_concepto = 1 or resid.id_concepto = 2)  and (resid.turno = 2))";

                cmd.ExecuteNonQuery();
            });
        }

        public int TiempoLegalT3()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 8, (cate.salario_turno3 - cate.salario), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid
                         join categorias cate on resid.id_categoria = cate.id_categoria where (resid.id_concepto = 1 or resid.id_concepto = 2)  and (resid.turno = 3))";

                cmd.ExecuteNonQuery();
            });
        }

        public int Trecex7(DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                            (select 1, trece.numero_empleado, trece.semana_pago, 'Reparación', 0, 31, trece.importe, @fecha2, 0, 0, 0, 1 from trece_siete trece
                             where trece.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int UtilesEscolares(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @" insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
        (select 1, util.empleado, util.semana_pago, @ciclo, 0, 34, util.importe_total, @fecha2, 0, 0, 0, 1 from utiles_escolares_a_pagar util
         where util.semana_pago = @semana) ";

                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int ValesDespensa(string Cycle, DateTime EndDate, int PayWeek)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion, exento)
        (select 1, valesd.numero_empleado, valesd.semana_pago, @ciclo, 0, 34, valesd.importe, @fecha2, 0, 0, 0, 1, valesd.importe from vales_despensa valesd
         where valesd.semana_pago = @semana)";

                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha2", EndDate);
                cmd.Parameters.AddWithValue("@semana", PayWeek);
                cmd.ExecuteNonQuery();
            });
        }

        public int PrimaSabatina()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"insert into resultado_sind_indiv_det_previo (id_nomina, numero_empleado, semana_pago, ciclo, id_categoria, id_concepto, importe, fecha, turno, uso, doble_turno, percepcion_deduccion)
                        (select resid.id_nomina, resid.numero_empleado, resid.semana_pago, resid.ciclo, resid.id_categoria, 7, (importe * 0.40), resid.fecha, resid.turno, resid.uso, 0, 1 from resultado_sind_indiv_det_previo resid where resid.id_concepto = 6)";

                cmd.ExecuteNonQuery();
            });
        }

        public int SabadosTrabajados()
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"Update resultado_sind_indiv_det_previo set id_concepto = 6 where datepart(dw,fecha) = 7 and (id_concepto = 1 or id_concepto = 2)";

                cmd.ExecuteNonQuery();
            });
        }

        public int SaveResult(DateTime StartDate, DateTime EndDate, int PayWeek,string Cycle,bool PayPPPF)
        {
            return ExecuteQuery((cmd, tra) =>
            {
                cmd.CommandText = @"INSERT INTO calculos (nomina,periodo,no_semana,ciclo,fecha_desde,fecha_hasta,pago_presencia_fyp) 
                                VALUES (@nomina,@periodo,@no_semana,@ciclo,@fecha_desde,@fecha_hasta,@pago_presencia_fyp)";

                cmd.Parameters.AddWithValue("@nomina", 1);
                cmd.Parameters.AddWithValue("@periodo", "");
                cmd.Parameters.AddWithValue("@no_semana", PayWeek);
                cmd.Parameters.AddWithValue("@ciclo", Cycle);
                cmd.Parameters.AddWithValue("@fecha_desde", StartDate);
                cmd.Parameters.AddWithValue("@fecha_hasta", EndDate);
                cmd.Parameters.AddWithValue("@pago_presencia_fyp", PayPPPF);

                cmd.ExecuteNonQuery();
            });
        }
    }
}
