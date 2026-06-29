using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.ChildSupport
{
    public class CSupportReport
    {
        public int NumberEmployee { get; set; }
        public string BeneficiaryName { get; set; }
        public string AccountBank { get; set; }
        public decimal Amount { get; set; }
    }
}
