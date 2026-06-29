using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.AwardPunctPre
{
    public class AwardReport
    {
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public int CategoryId { get; set; }
        public decimal PF { get; set; }
        public decimal AmountPF { get; set; }
        public decimal PP { get; set; }
        public decimal AmountPP { get; set; }
        public int Use { get; set; }
    }
}
