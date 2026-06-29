using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.IntegratedSalaries
{
    public class IntegratedSalary
    {
        public int Id { get; set; }
        [DisplayName("Id Empleado")]
        public int NumberEmployee { get; set; }
        [DisplayName("Nombre Completo")]
        public string NameEmployee { get; set; }
        [DisplayName("Salario Integrado Imss")]
        public decimal SalaryIntegratedImss { get; set; }        
        [DisplayName("Salario Integrado Infonavit")]
        public decimal SalaryIntegratedInfonavit { get; set; }

    }
}