using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Templates
{
    class Template
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Id Nomina")]
        public int PayrollId { get; set; }

        [DisplayName("No. Empleado")]
        public int NumberEmployee { get; set; }

        [DisplayName("Nombre Completo")]
        public string NameEmployee { get; set; }

        [DisplayName("Categoria")]
        public int Category { get; set; }

        [DisplayName("Cat. Requerida")]
        public int CategoryRequired { get; set; }

        [DisplayName("Turno Periodo")]
        public int ShiftPeriod { get; set; }

        [DisplayName("Turno Trabajdo")]
        public int ShiftWorked { get; set; }

        [DisplayName("Uso")]
        public int Use { get; set; }

        [DisplayName("Estado")]
        public string Status { get; set; }

    }
}
