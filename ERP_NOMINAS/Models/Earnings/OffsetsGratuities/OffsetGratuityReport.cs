using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.OffsetsGratuities
{
    public class OffsetGratuityReport
    {
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        public string Cycle { get; set; }
        public int PayWeek { get; set; }
        public int Use { get; set; }
        public int IdConcept { get; set; }
    }
}
