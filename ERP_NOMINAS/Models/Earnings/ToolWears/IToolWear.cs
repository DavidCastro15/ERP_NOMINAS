using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.ToolWears
{
    public interface IToolWear
    {
        List<ToolWear> GetToolWears(DateTime Start, DateTime End);
        List<ToolWearReport> GetDataReport(int Payweek,bool Temp);
        int SaveToolWears(List<ToolWear> list,int PayWeek);
        int SaveToolWearsTemp(List<ToolWearReport> list);
        decimal GetImportToolWear();
    }
}
