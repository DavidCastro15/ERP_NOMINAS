using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Offsets
{
    public class OffSetDetail
    {
        public int NumberEmployee { get; set; }
        public decimal Amount { get; set; }
        public int Use { get; set; }
        public string Comments { get; set; }
    }
}
