using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.ReduceDayImss
{
    public interface IRDImss
    {
        List<RDImss> GetRDImss();
        List<RDImss> FilterByValue(string Name);
        List<RDImss> FilterByValue(int NumberEmployee);
        int CreateRDImss(RDImss rd);
        int DeleteRDImss(int Id);
    }
}
