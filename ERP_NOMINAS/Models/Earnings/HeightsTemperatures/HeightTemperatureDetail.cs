using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.HeightsTemperatures
{
    public class HeightTemperatureDetail
    {
        public int Id { get; set; }
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public int CategoryId { get; set; }
        public decimal Hours { get; set; }
        public int Use { get; set; }
    }
}
