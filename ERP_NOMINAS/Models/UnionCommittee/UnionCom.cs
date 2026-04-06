using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.UnionCommittee
{
    public class UnionCom
    {
        public int Id { get; set; }

        [DisplayName("No. Empleado")]
        public int NumberEmployee { get; set; }
        [DisplayName("Nombre Completo")]
        public string NameEmployee { get; set; }
        [DisplayName("Porcentaje")]
        public decimal Percentage { get; set; }
        [DisplayName("Cat. Zafra")]
        public int CategoryHarvest { get; set; }
        [DisplayName("Cat. Reparacion")]
        public int CategoryRepair { get; set; }
        [DisplayName("Tipo")]
        public string Type { get; set; }
        [DisplayName("Id Concepto")]
        public int IdConcept { get; set; }
        [DisplayName("Uso")]
        public int Use { get; set; }

       
    }
}
