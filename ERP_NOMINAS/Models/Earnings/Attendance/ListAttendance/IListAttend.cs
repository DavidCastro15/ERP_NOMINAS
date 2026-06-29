using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Attendance.ListAttendance
{
    public interface IListAttend
    {
        List<ListAttendC> GetListAttends();
        List<ListAttendDetail> GetDetailsAttend(int ControlNumber);
        ListAttendDetail GetListAttendDetail(int ControlNumber, int NumberEmployee);
        List<ListAttendDetailReport> GetAttendDataReport(int ControlNumber);
        List<ListAttendAddReport> GetAttendAddDataReport(int PayWeek);
        int AddEmployeeDetails(ListAttendDetail listAttend);
        int UpdateEmployeeListAttend(ListAttendDetail listAttend);
        int DeleteEmployeeListAttend(int ControlNumber, int NumberEmployee);
        int DeleteListAttend(int NumberControl);
        int GetPwdDelete();
        
    }
}
