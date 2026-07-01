using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.Infonavit
{
    public interface IDInfonavit
    {
        List<DInfonavit> GetDInfonavits();
        List<DInfonavit> FilterByValue(string Name);
        List<DInfonavit> FilterByValue(int NumberEmployee);
        DInfonavit GetDInfonavit(int Id);
        int CreateDInfonavit(DInfonavit d);
        int UpdateDInfonavit(DInfonavit d);
        int DeleteInfonavit(int Id);
    }
}
