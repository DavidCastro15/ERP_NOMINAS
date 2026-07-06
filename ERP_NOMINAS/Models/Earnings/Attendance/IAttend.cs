using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Attendance
{
    public interface IAttend
    {
        List<Attend> GetDataFilterAttend(DateTime entryDate, TimeSpan entryTime, TimeSpan departureTime);
        List<Attend> FilterByValue(int NumberEmployee);
        List<Attend> FilterByValue(string NameEmployee);
        List<Attend> GetAttends();
        int CreateAttendanceList(string Status, string Period, int PayWeek, DateTime dateList, int Shift);
        int GetListAttendByDetails(int ControlNumber);
        int UpdateListAttendByDetails(int ControlNumber);
        Attend GetAttend(int NumberEmployee);
        int GetTop1EmployeeEnable();
        int ChangeTurn(int T);
        int AddAttendEmployee(Attend A);
        int UpdateAtttend(Attend A);
        int AddUnionCommittee();
        int DeleteAttend(int NumberEmployee);
        int CheckNextNumControl();
        int ChechNextNumberEmployee(int currentNumber = 0);
        int UpdateStatus(int NumberEmployee, bool Stat);
        List<AttendReport> GetAttendDataReport();
    }
}
