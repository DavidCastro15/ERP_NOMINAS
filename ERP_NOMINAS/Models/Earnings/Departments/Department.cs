using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Departments
{
    public class Department
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Departamento")]
        public int DepartmentId { get; set; }
        [DisplayName("Nombre Departamento")]
        public string NameDepartment { get; set; }
        [DisplayName("Responsable")]
        public string Responsible { get; set; }
        [DisplayName("Gerencia")]
        public int ManagmentId { get; set; }
        [DisplayName("Nombre Gerencia")]
        public string NameManagment { get; set; }
    }
}
