using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.PayRollProcess.CalculatedPayRolls
{
    public interface IProcessPayRollDeduction
    {
        int ISR_50();
        int IMSS_51();
        int PensionAlimenticia_52();
        int Infonavit_53();
        int PrestamoEmpresa_55();
        int AdeudoCasa_56();
        int PrestamoCompraHeramienta_57();
        int AjusteISR_58();
        int ISPTPendiente_59();
        int CuotaSindicalNacional_60();
        int CuotaSindicalLocal_61();
        int AhorroVoluntario10_62();
        int DescuentoDiaHaber_63();
        int DescuentoSindicalFijo_64();
        int DescuentoEspecialSindicato_65();
        int Nomifin_67();
        int CobroAzucar_69();
        int Ajustes_70();
        int CajaSindicalAhorroVoluntario_73();
        int DiferenciaNomina_76();
        int ISRAjustadoxSubsidio_79();
        int DescuentoLentes_81();
        int OtrosDescuentos_85();
    }
}
