using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Subsidies
{
    public class Subsidy
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Numero Empleado")]
        public int NumberEmployee { get; set; }
        [DisplayName("Nombre Completo")]
        public string FullName { get; set; }
        [DisplayName("Ciclo")]
        public string Cycle { get; set; }
        [DisplayName("Semana Pago")]
        public int PayWeek { get; set; }
        [DisplayName("Importe")]
        public decimal Amount { get; set; }
        [DisplayName("Uso")]
        public int Use { get; set; }
        [DisplayName("Comentarios")]
        public string Comments { get; set; }
        public int ConceptId { get; set; }
        public int PayrollId { get; set; }
    }
}
