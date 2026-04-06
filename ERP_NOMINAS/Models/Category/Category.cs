using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ERP_NOMINAS.Models.Category
{
    public class Category
    {
        [DisplayName("Id Categoria")]  
        public int IdCategory { get; set; }

        [DisplayName("Nombre Categoria")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Salario Base")]
        public decimal Salary { get; set; }

        [DisplayName("Sal. Turno 1")]
        public decimal SalaryTurn1 { get; set; }

        [DisplayName("Sal. Turno 2")]
        public decimal SalaryTurn2 { get; set; }

        [DisplayName("Sal. Turno 3")]
        public decimal SalaryTurn3 { get; set; }

        [DisplayName("Sal. Promedio")]
        public decimal AverageSalary { get; set; }

        [DisplayName("Sal. Promedio Turno 1 y2")]
        public decimal AverageSalaryT1xT2 { get; set; }

        [DisplayName("Alimentos")]
        public string Food { get; set; }

        [DisplayName("Alt. Temp.")]
        public string HeightsTemperatures { get; set; }
       
        public string Type { get; set; } = string.Empty;

        public int Ranking { get; set; }
    
        public int Priority { get; set; } = 0;

        public string Classified { get; set; } = string.Empty;

        public string ToolWear { get; set; } = string.Empty;


    }
}
