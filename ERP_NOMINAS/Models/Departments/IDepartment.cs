using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Departments
{
    public interface IDepartment
    {
        List<Department> GetDepartments();
        Department GetDepartment(int Id);
        int CreateDepartment(Department D);
        int UpdateDepartment(Department D);
        int DeleteDepartment(int Id);
        List<Department> FilterByName(string Name);
        int CheckNextId();
    }
}
