using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Uses
{
    public interface IUse
    {
        List<Use> GetUses();
        List<Use> FilterByUse(int U);
        Use GetUse(int Id);
        int CreateUse(Use U);
        int UpdateUse(Use U);
        int DeleteUse(int Id);
    }
}
