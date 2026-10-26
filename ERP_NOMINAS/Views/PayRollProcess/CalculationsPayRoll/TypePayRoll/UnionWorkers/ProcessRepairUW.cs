using ERP_NOMINAS.Repositorys.PayRollProcess.CalculationsPayRoll.UnionWorker.Reparacion;
using ERP_NOMINAS.Models.PayRollProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Views.PayRollProcess.CalculationsPayRoll.TypePayRoll.UnionWorkers
{
    public class ProcessRepairUW : IPayrollProcessor
    {
        private ProcessPerceptionRepositoryUWR _repo = new ProcessPerceptionRepositoryUWR();

        public List<MethodResult> Process(string cycle, int payWeek, DateTime startDate, DateTime endDate, int noWorkingDays = 0, int efectiveDays = 0)
        {
            List<MethodResult> executionLog = new List<MethodResult>();
            int successfulMethodsCount = 0;

            void ExecuteStep(string name, Action action)
            {
                try
                {
                    action();
                    executionLog.Add(new MethodResult { MethodName = name, IsSuccess = true });
                    successfulMethodsCount++;
                }
                catch (Exception ex)
                {
                    executionLog.Add(new MethodResult { MethodName = name, IsSuccess = false, ErrorMessage = ex.Message });
                }
            }

            ExecuteStep("LimpiarInfo", () => _repo.LimpiarInfo());
            ExecuteStep("ActualizandoPlazas", () => _repo.ActualizandoPlazas());

            ExecuteStep("Listas1X2", () => _repo.Listas1X2(startDate, endDate, payWeek));
            ExecuteStep("Listas2X2", () => _repo.Listas2X2(startDate, endDate, payWeek));

            ExecuteStep("IdentificandoDomingosTrabajados", () => _repo.IdentificandoDomingosTrabajados());
            ExecuteStep("PrimaDominical", () => _repo.PrimaDominical());

            ExecuteStep("FestivosTrabajados", () => _repo.FestivosTrabajados());
            ExecuteStep("SabadosTrabajados", () => _repo.SabadosTrabajados());
            ExecuteStep("PrimaSabatina", () => _repo.PrimaSabatina());

            ExecuteStep("TiempoLegalT1", () => _repo.TiempoLegalT1());
            ExecuteStep("TiempoLegalT2", () => _repo.TiempoLegalT2());
            ExecuteStep("TiempoLegalT3", () => _repo.TiempoLegalT3());
            ExecuteStep("TiempoLegalNormalT6", () => _repo.TiempoLegalNormalT6());
            ExecuteStep("TiempoLegalNormalT7", () => _repo.TiempoLegalNormalT7());
            ExecuteStep("TiempoLegalSabadoTrabajadoT2X6", () => _repo.TiempoLegalSabadoTrabajadoT2X6());
            ExecuteStep("TiempoLegalDomingosTrabajadosT3X7", () => _repo.TiempoLegalDomingosTrabajadosT3X7());
            ExecuteStep("TiempoLegalDomingosTrabajadosT2X6", () => _repo.TiempoLegalDomingosTrabajadosT2X6());
            ExecuteStep("TiempoLegalDomingosTrabajadosT3X7_2", () => _repo.TiempoLegalDomingosTrabajadosT3X7_2());
            ExecuteStep("TiempoLegalFestivoTrabajado2x6", () => _repo.TiempoLegalFestivoTrabajado2x6());
            ExecuteStep("TiempoLegalFestivosTrabajadoT3X7", () => _repo.TiempoLegalFestivosTrabajadoT3X7());
            ExecuteStep("Devoluciones", () => _repo.Devoluciones(cycle, endDate, payWeek));

            ExecuteStep("AlimentosDiasNormalT1X2X3", () => _repo.AlimentosDiasNormalT1X2X3(cycle));
            ExecuteStep("AlimentosDomingosT1X2X3", () => _repo.AlimentosDomingosT1X2X3(cycle));
            ExecuteStep("AlimentosFestivosTrabajadosT1X2X3", () => _repo.AlimentosFestivosTrabajadosT1X2X3(cycle));
            ExecuteStep("AlturasTemperaturasDiasNormalesTX1X2X3", () => _repo.AlturasTemperaturasDiasNormalesTX1X2X3());
            ExecuteStep("AlturasTemperaturasDomingosTX1X2X3", () => _repo.AlturasTemperaturasDomingosTX1X2X3());
            ExecuteStep("AlturasTemperaturasFestivosTX1X2X3", () => _repo.AlturasTemperaturasFestivosTX1X2X3());

            ExecuteStep("Compensaciones", () => _repo.Compensaciones(endDate, payWeek));
            ExecuteStep("Comisiones", () => _repo.Comisiones(cycle, endDate, payWeek));
            ExecuteStep("PremioPP", () => _repo.PremioPP(cycle, endDate, payWeek));
            ExecuteStep("PremioPPF", () => _repo.PremioPPF(cycle, endDate, payWeek));
            ExecuteStep("Destajos", () => _repo.Destajos(payWeek));
            ExecuteStep("Subsidios", () => _repo.Subsidios(endDate, payWeek));
            ExecuteStep("GratificacionXDesgasteHerramienta", () => _repo.GratificacionXDesgasteHerramienta(endDate, payWeek));
            ExecuteStep("HorasElaboracion", () => _repo.HorasElaboracion(endDate, payWeek));
            ExecuteStep("AlturasTemperaturasDocumentadas", () => _repo.AlturasTemperaturasDocumentadas(payWeek));
            ExecuteStep("UtilesEscolares", () => _repo.UtilesEscolares(cycle, endDate, payWeek));
            ExecuteStep("CargasxDescargas", () => _repo.CargasxDescargas(endDate, payWeek));
            ExecuteStep("Trecex7", () => _repo.Trecex7(endDate, payWeek));
            ExecuteStep("Aguinaldo", () => _repo.Aguinaldo(endDate, payWeek));
            ExecuteStep("DomingosTrabajados", () => _repo.DomingosTrabajados(cycle, endDate, payWeek));
            ExecuteStep("BonoPTU", () => _repo.BonoPTU(endDate, payWeek));
            ExecuteStep("BonoDomingosDescansos", () => _repo.BonoDomingosDescansos(endDate, payWeek));

            ExecuteStep("SeptimoDia", () => _repo.SeptimoDia(cycle, startDate, endDate, payWeek, efectiveDays));
            ExecuteStep("SeptimoDiaX2", () => _repo.SeptimoDiaX2());
            ExecuteStep("SextoDia", () => _repo.SextoDia());
            ExecuteStep("SeptimoDiaVariable", () => _repo.SeptimoDiaVariable(cycle, payWeek));
            ExecuteStep("FestivoNoLaborado", () => _repo.FestivoNoLaborado(cycle, startDate, endDate, noWorkingDays));

            ExecuteStep("GravadosExentos", () => _repo.GravadosExentos());
            ExecuteStep("Gravadas100", () => _repo.Gravadas100());
            ExecuteStep("MixtasPrimaDominical", () => _repo.MixtasPrimaDominical(payWeek));
            ExecuteStep("Mixtas", () => _repo.Mixtas(payWeek));
            ExecuteStep("Exentos100", () => _repo.Exentos100());

            ExecuteStep("ApoyoTransporte", () => _repo.ApoyoTransporte(cycle, endDate, payWeek));
            ExecuteStep("ValesDespensa", () => _repo.ValesDespensa(cycle, endDate, payWeek));
            ExecuteStep("Retroactivo", () => _repo.Retroactivo(cycle, endDate, payWeek));

            ExecuteStep("GuardarRegistroCalculo", () => _repo.SaveResult(startDate,endDate,payWeek,cycle,false));

            return executionLog;
        }
    }
}
