using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.IsrVariable
{
    public interface IIsrV
    {
        List<IsrV> GetIsrVs();
        List<IsrV> FilterByValue(string Name);
        List<IsrV> FilterByValue(int NumberEmployee);
        IsrV GetIsr(int Id);
        int CreateIsrV(IsrV isrV);
        int UpdateIsrV(IsrV isrV);
        int DeleteIsrV(int Id);
        void ImportEmployees(DataTable dtCsv);
    }
}
