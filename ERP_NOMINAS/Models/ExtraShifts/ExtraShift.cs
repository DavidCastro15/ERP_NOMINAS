using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.ExtraShifts
{
    public class ExtraShift
    {
        [DisplayName("Numero Control")]
        public int NumberControl { get; set; }
        [DisplayName("Semana Pago")]
        public int PayWeek { get; set; }
        [DisplayName("Fecha")]
        public DateTime Date { get; set; }
        [DisplayName("Asistencia")]
        public int Attend { get; set; }
        [DisplayName("Numero Empleado")]
        public int NumberEmployee { get; set; }
        [DisplayName("Uso")]
        public int Use { get; set; }
        [DisplayName("Turno")]
        public int Shift { get; set; }
        [DisplayName("Id")]
        public int AttendListDetailId { get; set; }
    }
}
