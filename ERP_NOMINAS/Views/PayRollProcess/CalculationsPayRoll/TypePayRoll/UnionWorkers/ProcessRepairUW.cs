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

        public List<MethodResult> Process(string _Cycle, int _PayWeek, DateTime _StartDate, DateTime _EndDate, int _NoWorkinDays = 0, int _EfectiveDays = 0)
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

            ExecuteStep("Listas1X2", () => _repo.Listas1X2(_StartDate, _EndDate, _PayWeek));
            ExecuteStep("Listas2X2", () => _repo.Listas2X2(_StartDate, _EndDate, _PayWeek));

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
            ExecuteStep("Devoluciones", () => _repo.Devoluciones(_Cycle, _EndDate, _PayWeek));

            ExecuteStep("AlimentosDiasNormalT1X2X3", () => _repo.AlimentosDiasNormalT1X2X3(_Cycle));
            ExecuteStep("AlimentosDomingosT1X2X3", () => _repo.AlimentosDomingosT1X2X3(_Cycle));
            ExecuteStep("AlimentosFestivosTrabajadosT1X2X3", () => _repo.AlimentosFestivosTrabajadosT1X2X3(_Cycle));
            ExecuteStep("AlturasTemperaturasDiasNormalesTX1X2X3", () => _repo.AlturasTemperaturasDiasNormalesTX1X2X3());
            ExecuteStep("AlturasTemperaturasDomingosTX1X2X3", () => _repo.AlturasTemperaturasDomingosTX1X2X3());
            ExecuteStep("AlturasTemperaturasFestivosTX1X2X3", () => _repo.AlturasTemperaturasFestivosTX1X2X3());

            ExecuteStep("Compensaciones", () => _repo.Compensaciones(_EndDate, _PayWeek));
            ExecuteStep("Comisiones", () => _repo.Comisiones(_Cycle, _EndDate, _PayWeek));
            ExecuteStep("PremioPP", () => _repo.PremioPP(_Cycle, _EndDate, _PayWeek));
            ExecuteStep("PremioPPF", () => _repo.PremioPPF(_Cycle, _EndDate, _PayWeek));
            ExecuteStep("Destajos", () => _repo.Destajos(_PayWeek));
            ExecuteStep("Subsidios", () => _repo.Subsidios(_EndDate, _PayWeek));
            ExecuteStep("GratificacionXDesgasteHerramienta", () => _repo.GratificacionXDesgasteHerramienta(_EndDate, _PayWeek));
            ExecuteStep("HorasElaboracion", () => _repo.HorasElaboracion(_EndDate, _PayWeek));
            ExecuteStep("AlturasTemperaturasDocumentadas", () => _repo.AlturasTemperaturasDocumentadas(_PayWeek));
            ExecuteStep("UtilesEscolares", () => _repo.UtilesEscolares(_Cycle, _EndDate, _PayWeek));
            ExecuteStep("CargasxDescargas", () => _repo.CargasxDescargas(_EndDate, _PayWeek));
            ExecuteStep("Trecex7", () => _repo.Trecex7(_EndDate, _PayWeek));
            ExecuteStep("Aguinaldo", () => _repo.Aguinaldo(_EndDate, _PayWeek));
            ExecuteStep("DomingosTrabajados", () => _repo.DomingosTrabajados(_Cycle, _EndDate, _PayWeek));
            ExecuteStep("BonoPTU", () => _repo.BonoPTU(_EndDate, _PayWeek));
            ExecuteStep("BonoDomingosDescansos", () => _repo.BonoDomingosDescansos(_EndDate, _PayWeek));

            ExecuteStep("SeptimoDia", () => _repo.SeptimoDia(_Cycle, _StartDate, _EndDate, _PayWeek, _EfectiveDays));
            ExecuteStep("SeptimoDiaX2", () => _repo.SeptimoDiaX2());
            ExecuteStep("SextoDia", () => _repo.SextoDia());
            ExecuteStep("SeptimoDiaVariable", () => _repo.SeptimoDiaVariable(_Cycle, _PayWeek));
            ExecuteStep("FestivoNoLaborado", () => _repo.FestivoNoLaborado(_Cycle, _StartDate, _EndDate, _NoWorkinDays));

            ExecuteStep("GravadosExentos", () => _repo.GravadosExentos());
            ExecuteStep("Gravadas100", () => _repo.Gravadas100());
            ExecuteStep("MixtasPrimaDominical", () => _repo.MixtasPrimaDominical(_PayWeek));
            ExecuteStep("Mixtas", () => _repo.Mixtas(_PayWeek));

            ExecuteStep("ApoyoTransporte", () => _repo.ApoyoTransporte(_Cycle, _EndDate, _PayWeek));
            ExecuteStep("ValesDespensa", () => _repo.ValesDespensa(_Cycle, _EndDate, _PayWeek));
            ExecuteStep("Retroactivo", () => _repo.Retroactivo(_Cycle, _EndDate, _PayWeek));

            ExecuteStep("GuardarRegistroCalculo", () => _repo.SaveResult(_StartDate,_EndDate,_PayWeek,_Cycle,false));

            return executionLog;
        }
    }
}
