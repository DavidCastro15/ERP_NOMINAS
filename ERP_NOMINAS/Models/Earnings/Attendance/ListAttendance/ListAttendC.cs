using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Attendance.ListAttendance
{
    public class ListAttendC
    {
        [DisplayName("Numero Control")]
        public int ControlNumber { get; set; }
        [DisplayName("Id Nomina")]
        public int PayrollId { get; set; }
        [DisplayName("Fecha")]
        public DateTime Date { get; set; }
        [DisplayName("Truno")]
        public int Shift { get; set; }
        [DisplayName("Periodo")]
        public string Period { get; set; }
        [DisplayName("Estatus")]
        public string Status { get; set; }
        [DisplayName("Semana Pago")]
        public int PayWeek { get; set; }
    }
}
