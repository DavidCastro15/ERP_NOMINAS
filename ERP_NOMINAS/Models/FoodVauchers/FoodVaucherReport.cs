using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.FoodVauchers
{
    public class FoodVaucherReport
    {
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public int WorkDays { get; set; }
        public decimal Amount { get; set; }
    }
}
