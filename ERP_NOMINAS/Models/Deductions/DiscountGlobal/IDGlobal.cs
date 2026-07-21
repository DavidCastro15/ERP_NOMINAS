using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.DiscountGlobal
{
    public interface IDGlobal
    {
        List<DGlobal> GetDGlobals();
        List<DGlobal> FilterByConcept(int IdConcept);
        DGlobal GetDGlobal(int Id);
        int CreateDGlobal(DGlobal d);
        int UpdateDGlobal(DGlobal d);
        int DeleteDGlobal(int Id);
    }
}
