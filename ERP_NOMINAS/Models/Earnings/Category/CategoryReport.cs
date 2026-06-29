using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Category
{
    public class CategoryReport
    {       
        public int IdCategory { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public decimal SalaryTurn1 { get; set; }
        public decimal SalaryTurn2 { get; set; }
        public decimal SalaryTurn3 { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal AverageSalaryT1xT2 { get; set; }
        public string Food { get; set; }
        public string HeightsTemperatures { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Ranking { get; set; }
        public int Priority { get; set; } = 0;
        public string Classified { get; set; } = string.Empty;
        public string ToolWear { get; set; } = string.Empty;
    }
}
