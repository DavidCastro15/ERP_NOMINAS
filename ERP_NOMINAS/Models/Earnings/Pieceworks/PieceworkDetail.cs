using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Pieceworks
{
   public class PieceworkDetail
    {
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public int Foreman { get; set; }
        public int Category { get; set; }

    }
}
