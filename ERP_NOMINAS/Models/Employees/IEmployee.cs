using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Employees
{
    public interface IEmployee
    {
        List<Employee> GetEmployees();
        List<Employee> FilterByName(string Name);
        Employee GetEmployee(int Id);
        Employee GetEmployeeByNumber(int NumberEmployee);
        int CreateEmployee(Employee e);
        int UpdateEmployee(Employee e);
        int DeleteEmployee(int Id);
        bool ExistsId(int Id);
      
    }
}
