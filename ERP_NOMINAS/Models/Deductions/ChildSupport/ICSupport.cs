using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.ChildSupport
{
   public interface ICSupport
    {
        List<CSupport> GetCSupports();
        List<CSupport> FilterByValue(string Name);
        List<CSupport> FilterByValue(int PayWeek);
        List<CSupportReport> GetDataReport(int PayWeek);
        CSupport GetCSupport(int Id);
        int CreateCSupport(CSupport support);
        int UpdateCSupport(CSupport support);
        int DeleteCSupport(int Id);
    }
}
