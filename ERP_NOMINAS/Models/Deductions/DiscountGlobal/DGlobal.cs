using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.DiscountGlobal
{
    public class DGlobal
    {
        public int Id { get; set; }
        public string Cycle { get; set; }
        public int PayWeek { get; set; }
        public int IdConcept { get; set; }
        public byte Occasional { get; set; }
        public byte Temporary { get; set; }
        public byte Permanent { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
