using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.ExtraShifts
{
    public interface IExtraShift
    {
        List<ExtraShift> GetExtraShifts(DateTime T1,DateTime T2);
        int UpdateAttend(List<ExtraShift> list, IProgress<int> progress);
    }
}
