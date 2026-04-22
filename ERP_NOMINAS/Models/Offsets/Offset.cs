using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Offsets
{
    public class Offset
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Id Nomina")]
        public int PayrollId { get; set; }
        [DisplayName("N. Empleado")]
        public int NumberEmployee { get; set; }
        [DisplayName("Nombre Completo")]
        public string FullName { get; set; }
        [DisplayName("Importe")]
        public decimal Amount { get; set; }
        [DisplayName("Ciclo")]
        public string Cycle { get; set; }
        [DisplayName("Periodo")]
        public int PayWeek { get; set; }
        [DisplayName("Uso")]
        public int Use { get; set; }
        [DisplayName("Comentarios")]
        public string Comments { get; set; }
      
        public int IdConcept { get; set; }
        public List<OffSetDetail> Details { get; set; }
    }
}
