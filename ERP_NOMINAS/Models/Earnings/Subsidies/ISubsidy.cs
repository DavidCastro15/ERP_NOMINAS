using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Subsidies
{
    public interface ISubsidy
    {
        List<Subsidy> GetSubsidies();
        List<Subsidy> FilterByValue(int pw);
        List<Subsidy> FilterByValue(string Name);
        List<SubsidyReport> ShowDataReport(int pw);
        Subsidy GetSubsidy(int Id);
        int CreateSubsidy(Subsidy s);
        int UpdateSubsidy(Subsidy s);
        int DeleteSubidy(int id);

    }
}
