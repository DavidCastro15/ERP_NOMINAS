using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Attendance.ListAttendance
{
    public class ListAttendDetail
    {
        public int NumberControl { get; set; }
        public int PayRoll { get; set; }
        public int NumberEmployee { get; set; }
        public int UseWorked { get; set; }
        public int CategoryWorked { get; set; }
        public int ShiftWorked { get; set; }
    }
}
