using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.HeightsTemperatures
{
    public class HeightTemperature
    {
        [DisplayName("Folio")]
        public int ReferenceNumber { get; set; }
        [DisplayName("Id Nomina")]
        public int PayrollId { get; set; }
        [DisplayName("Semana Pago")]
        public int PayWeek { get; set; }
        [DisplayName("Ciclo")]
        public string Cycle { get; set; }
        [DisplayName("Fecha")]
        public DateTime Date { get; set; }
        [DisplayName("Percepcion")]
        public int IdConcept { get; set; }
        public List<HeightTemperatureDetail> Details { get; set; }

    }
}
