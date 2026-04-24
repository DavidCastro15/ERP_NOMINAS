using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.HeightsTemperatures
{
    public class HeightTemperatureReport
    {
        public int Id { get; set; }
        public int ReferenceNumber { get; set; }
        public int NumberEmployee { get; set; }
        public string Cycle { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public decimal Hours { get; set; }
        public int Use { get; set; }
        public decimal Amount { get; set; }
    }
}
