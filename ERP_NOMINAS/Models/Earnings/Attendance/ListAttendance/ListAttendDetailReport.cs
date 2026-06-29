using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Attendance.ListAttendance
{
    public class ListAttendDetailReport
    {
        public int NumberControl { get; set; }
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public int UseWorked { get; set; }
        public string Department { get; set; }
        public string CategoryWorked { get; set; }
        public int ShiftWorked { get; set; }
        public string Status { get; set; }
    }
}
