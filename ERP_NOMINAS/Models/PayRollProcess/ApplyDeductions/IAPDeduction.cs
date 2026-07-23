using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.PayRollProcess.ApplyDeductions
{
    public interface IAPDeduction
    {
        List<APDeduction> GetAPDeductions();
        int CheckedOne(int Id, string Stat);
        int CheckedAll();
    }
}
