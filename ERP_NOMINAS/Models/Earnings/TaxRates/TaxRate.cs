using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.TaxRates
{
    public class TaxRate
    {
        public int Id { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal FixedFee { get; set; }
        public decimal ExcessPercentage { get; set; }
    }
}
