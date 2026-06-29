using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Uses
{
    public class Use
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Uso")]
        public long _Use { get; set; }
        [DisplayName("Descripcion")]
        public string Description { get; set; }
        [DisplayName("Id Gerencia")]
        public int IdManagment { get; set; }
        [DisplayName("Gerencia")]
        public string NameManagement { get; set; }
        [DisplayName("Id Departamento")]
        public int IdDepartment { get; set; }
        [DisplayName("Departamento")]
        public string NameDepartment { get; set; }
        [DisplayName("Grupo")]
        public int Group { get; set; }
        [DisplayName("Equipo")]
        public int Equipment { get; set; }
        [DisplayName("Cuenta Contable")]
        public string AccountingAccount { get; set; }
    }
}
