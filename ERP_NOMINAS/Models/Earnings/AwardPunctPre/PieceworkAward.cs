using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.AwardPunctPre
{
    public class PieceworkAward
    {
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public int CategoryId { get; set; }
        public decimal Salary { get; set; }
        public decimal Piecework { get; set; }
        public decimal FinalSalary { get; set; }
    }
}
