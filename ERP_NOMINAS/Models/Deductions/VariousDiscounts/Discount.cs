using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.VariousDiscounts
{
    public class Discount
    {
        public int Id { get; set; }
        public int IdConcept { get; set; }
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public decimal InitialAmount { get; set; }
        public int NumberPayments { get; set; }
        public decimal _Discount { get; set; }
        public decimal OutstandingBalance { get; set; }
        public decimal AccumulatedDiscounts { get; set; }
        public DateTime CaptureDate { get; set; }
        public string Comments { get; set; }
        public decimal? LastAmountPaid { get; set; }
    }
}
