using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.DiscountGlobal
{
    public interface IISkipDGlobal
    {
        List<SkipDGlobal> GetSkipDGlobals();
        List<SkipDGlobal> FilterByValue(string Name);
        List<SkipDGlobal> FilterByValue(int NumberEmployee);
        void ImportEmployees(DataTable dtCsv);
        int DeleteSkipDGlobaEmployee(int Id);
    }
}
