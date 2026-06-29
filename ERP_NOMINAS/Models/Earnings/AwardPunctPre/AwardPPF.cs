using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.AwardPunctPre
{
    public class AwardPPF
    {
        public int Id { get; set; }
        public int NumberEmployee { get; set; }
        public decimal Salary { get; set; }
        public int DaysPF { get; set; }
        public decimal PF { get; set; }
        public decimal AmountPF { get; set; }
        public int DaysPP { get; set; }
        public decimal PP { get; set; }
        public decimal AmountPP { get; set; }
        public int Use { get; set; }
        public string Comments { get; set; }
    }
}
