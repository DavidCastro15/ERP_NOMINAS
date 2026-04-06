using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Cycles
{
    public interface ICycle
    {
        Cycle GetCycle();
        int ChangeCycle(string C);
    }
}
