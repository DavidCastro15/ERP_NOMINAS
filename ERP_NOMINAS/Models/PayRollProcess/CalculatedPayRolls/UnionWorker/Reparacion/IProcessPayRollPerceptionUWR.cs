using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.PayRollProcess.CalculatedPayRolls.UnionWorker.Reparacion
{
    public interface IProcessPayRollPerceptionUWR
    {
        int LimpiarInfo();
        //int DiasFestivos(string Cycle,DateTime StartDate, DateTime EndDate, int EfectiveDays);
        int Listas1X2(DateTime StartDate, DateTime EndDate,int PayWeek);
        int Listas2X2(DateTime StartDate, DateTime EndDate, int PayWeek);
        int IdentificandoDomingosTrabajados();
        int PrimaDominical();
        int FestivosTrabajados();
        int SabadosTrabajados(); 
        int PrimaSabatina();
        int TiempoLegalT1();
        int TiempoLegalT2();
        int TiempoLegalT3();
        int TiempoLegalNormalT6();
        int TiempoLegalNormalT7();
        //int TiempoLegalDomingoTrabajadoT1();
        int TiempoLegalSabadoTrabajadoT2X6();
        int TiempoLegalDomingosTrabajadosT3X7();
        int TiempoLegalDomingosTrabajadosT2X6();
        int TiempoLegalDomingosTrabajadosT3X7_2();
        int TiempoLegalFestivoTrabajado2x6();
        int TiempoLegalFestivosTrabajadoT3X7();
        int AlimentosDiasNormalT1X2X3(string Cycle);
        int AlimentosDomingosT1X2X3(string Cycle);
        int AlimentosFestivosTrabajadosT1X2X3(string Cycle);
        int AlturasTemperaturasDiasNormalesTX1X2X3();
        int AlturasTemperaturasDomingosTX1X2X3();
        int AlturasTemperaturasFestivosTX1X2X3();
        int Compensaciones(DateTime EndDate, int PayWeek);
        int Comisiones(string Cycle,DateTime EndDate,int PayWeek);
        int PremioPPF(string Cycle, DateTime EndDate, int PayWeek);
        int PremioPP(string Cycle, DateTime EndDate, int PayWeek);
        int Destajos(int PayWeek);
        int Subsidios(DateTime EndDate,int PayWeek);
        int GratificacionXDesgasteHerramienta(DateTime EndDate, int PayWeek);
        int HorasElaboracion(DateTime EndDate, int PayWeek);
        int AlturasTemperaturasDocumentadas(int PayWeek);
        int UtilesEscolares(string Cycle, DateTime EndDate, int PayWeek);
        int CargasxDescargas(DateTime EndDate, int PayWeek);
        int Trecex7(DateTime EndDate, int PayWeek);
        int Aguinaldo(DateTime EndDate, int PayWeek);
        int DomingosTrabajados(string Cycle, DateTime EndDate, int PayWeek);
        int BonoPTU(DateTime EndDate, int PayWeek);
        int BonoDomingosDescansos(DateTime EndDate, int PayWeek);
        int SeptimoDia(string Cycle, DateTime StartDate, DateTime EndDate, int PayWeek, int EfectiveDays);
        int SeptimoDiaX2();
        int SextoDia();
        int SeptimoDiaVariable(string Cycle, int PayWeek);
        int FestivoNoLaborado(string Cycle, DateTime StartDate, DateTime EndDate, int NoWorkinDays);
        int GravadosExentos();
        int Gravadas100();
        int MixtasPrimaDominical(int PayWeek);
        int Mixtas(int PayWeek);
        int ApoyoTransporte(string Cycle, DateTime EndDate, int PayWeek);
        int ValesDespensa(string Cycle, DateTime EndDate, int PayWeek);
        int Retroactivo(string Cycle, DateTime EndDate, int PayWeek);
    }
}
