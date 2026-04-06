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
        
        int UpdateListAttend(ListAttendC listAttend);
        int DeleteListAttend(int NumberControl);
        int GetPwdDelete();
    }
}
