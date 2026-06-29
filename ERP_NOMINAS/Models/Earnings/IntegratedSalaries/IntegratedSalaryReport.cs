using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.IntegratedSalaries
{
    public class IntegratedSalaryReport
    {
        public int Id { get; set; }
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public decimal SalaryIntegratedImss { get; set; }
        public decimal SalaryIntegratedInfonavit { get; set; }
    }
}
