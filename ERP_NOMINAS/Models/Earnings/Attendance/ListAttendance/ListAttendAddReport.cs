using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Attendance.ListAttendance
{
    public class ListAttendAddReport
    {
        public int NumberControl { get; set; }
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public int PayWeek { get; set; }
        public int TotalEmployes { get; set; }
        public int TotalCategory { get; set; }
    }
}
