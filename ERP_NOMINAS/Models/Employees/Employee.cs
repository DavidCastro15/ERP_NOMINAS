using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Employees
{
   public class Employee
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Numero Empleado")]
        public long NumberEmployee { get; set; }  
        
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Apellido Paterno")]
        public string LastnameFather { get; set; }

        [DisplayName("Apellido Materno")]
        public string LastnameMother { get; set; }
     
        [DisplayName("Imss")]
        public string Imss { get; set; }

        [DisplayName("CURP")]
        public string CURP { get; set; }
       
        [DisplayName("Tipo")]
        public string Type { get; set; }

        [DisplayName("Clasificacion")]
        public string Classification { get; set; }

        [DisplayName("Cat. Zafra")]
        public int CategoryHarvest { get; set; }

        [DisplayName("Cat. Reparacion")]
        public int CategoryRepair { get; set; }

        
        public int PayrollId { get; set; }
        public int NumberCredential { get; set; }
        public string RFC { get; set; }
        public int Module18 { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }     
        public string Area { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string PlaceBirth { get; set; }
        public DateTime DateBirth { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }
        public string Cologne { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string CellPhone { get; set; }
        public string Schooling { get; set; }
        public string Profession { get; set; }
        public int YearRepair { get; set; }
        public int YearHarvest { get; set; }
        public int BonusDaysRepair { get; set; }
        public int BonusDaysHarvest { get; set; }
        public int VacationDaysRepair { get; set; }
        public int VacationDaysHarvest { get; set; }
        public int DaysCommission { get; set; }
        public string Declare { get; set; }
        public string Photo { get; set; }
        public string Absenteeism { get; set; }    
        public string Pattern { get; set; }
        public int PresenceDaysPrevious { get; set; }
        public int PunctualityDaysPrevious { get; set; }
        public int PresenceDaysCurrent { get; set; }
        public int PunctualityDaysCurrent { get; set; }
        public int WorkedSundayHarvest { get; set; }
        public string AccountBank { get; set; }
        public string InterbankKey { get; set; }
        public int ApplyUnionDues { get; set; }
        public DateTime DatePlaza { get; set; }

    }
}
