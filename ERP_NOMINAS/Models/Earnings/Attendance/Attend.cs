using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Attendance
{
    public class Attend
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("No. Empleado")]
        public int NumberEmployee { get; set; }

        [DisplayName("Nombre Completo")]
        public string FullName { get; set; }    
        
        [DisplayName("Cat. Original")]
        public int CategoryOriginal { get; set; }

        [DisplayName("Cat. Trabajada")]
        public int CategoryWorked { get; set; }

        [DisplayName("Uso Original")]
        public int UseOriginal { get; set; }

        [DisplayName("Uso Trabajado")]
        public int UseWorked { get; set; }

        [DisplayName("Turno Original")]
        public int TurnOriginal { get; set; }

        [DisplayName("Turno Trabajado")]
        public int TurnWorked { get; set; }

        [DisplayName("Fecha Entrada")]
        public DateTime EntryDate { get; set; }

        [DisplayName("Hora Entrada")]     
        public DateTime EntryTime { get; set; }

        [DisplayName("Hora Salida")]  
        public DateTime DepartureTime { get; set; }

        [DisplayName("Tipo Empleado")]
        public string TypeEmployee { get; set; }
        
        [DisplayName("Estatus")]
        public string StatusED => Status == "1" ? "Activo" : "Inactivo";

        public string Status { get; set; }
    }
}
