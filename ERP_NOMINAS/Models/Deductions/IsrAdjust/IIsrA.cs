using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.IsrAdjust
{
    public interface IIsrA
    {
        List<IsrA> GetIsrAs();
        List<IsrA> FilterByValue(string Name);
        List<IsrA> FilterByValue(int NumberEmployee);
        IsrA GetIsr(int Id);
        int CreateIsrA(IsrA isrV);
        int UpdateIsrA(IsrA isrV);
        int DeleteIsrA(int Id);
        void ImportEmployees(DataTable dtCsv);
    }
}
