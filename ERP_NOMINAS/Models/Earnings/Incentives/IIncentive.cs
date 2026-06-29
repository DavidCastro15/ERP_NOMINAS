using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Incentives
{
    public interface IIncentive
    {
        List<Incentive> GetIncentives();
        List<Incentive> FilterByValue(string Name);
        List<Incentive> FilterByValue(int PayWeek);
        void ImportEmployees(DataTable dtCsv);
        int DeleteIncentiveEmployee(int Id);
        int DeleteIncentivePayWeek(int PayWeek);
    }
}
