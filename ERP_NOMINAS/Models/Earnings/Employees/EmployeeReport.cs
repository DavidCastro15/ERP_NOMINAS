using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Employees
{
    public class EmployeeReport
    {
        public int Id { get; set; }
        public long NumberEmployee { get; set; }
        public string Name { get; set; }
        public string LastnameFather { get; set; }
        public string LastnameMother { get; set; }
        public string Imss { get; set; }
        public string CURP { get; set; }
        public string Type { get; set; }
        public string Classification { get; set; }
        public int CategoryHarvest { get; set; }
        public int CategoryRepair { get; set; }
        public string RFC { get; set; }     
        public string Status { get; set; }

    }
}
