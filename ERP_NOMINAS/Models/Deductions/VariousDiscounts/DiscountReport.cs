using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.VariousDiscounts
{
    public class DiscountReport
    {
        public int Id { get; set; }
        public int IdConcept { get; set; }
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public decimal Discount { get; set; }
        public decimal OutstandingBalance { get; set; }
    }
}
