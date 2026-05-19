using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.SupportTransportations
{
    public interface ISupportTransportation
    {
        List<SupportTransportation> GetSupportTransportations(DateTime Start, DateTime End, int WorkedDays);
        decimal GetImportTransport();
        int SaveSupportTransports(List<SupportTransportation> list, int PayWeek);
        List<SupportTransportationReportExcel> GetDataTransportationExcel(int Payweek);
        List<SupportTransportationReport> GetDataReport(int Payweek);
        void ExportToExcel(List<SupportTransportationReportExcel> list, string filePath);
    }
}
