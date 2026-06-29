using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Incentives
{
    public class Incentive
    {
        public int Id { get; set; }
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        public int PayWeek { get; set; }
    }
}
