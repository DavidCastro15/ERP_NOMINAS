using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Groups
{
    public class Group
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Grupo")]
        public int _Group { get; set; }
        [DisplayName("Descripcion")]
        public string Description { get; set; }
        [DisplayName("Id Gerencia")]
        public int ManagmentId { get; set; }
        [DisplayName("Gerencia")]
        public string NameManagment { get; set; }
        [DisplayName("Id Departamento")]
        public int DepartmentId { get; set; }
        [DisplayName("Departamento")]
        public string NameDepartment { get; set; }
    }
}
