using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.ChildSupport
{
     public class CSupport
    {
        public int Id { get; set; }
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public decimal SupportAmount { get; set; }
        public decimal SupportPercentage { get; set; }
        public string BeneficiaryName { get; set; }
        public string BankAccount { get; set; }
    }
}
