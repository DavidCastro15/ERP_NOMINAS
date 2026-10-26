using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.PayRollProcess.CalculatedPayRolls
{
    public class CalculatedPRolls
    {
        public int Id { get; set; }
        public int PayRollId { get; set; }
        public int PayWeek { get; set; }
        public string Cycle { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool PayPFPP { get; set; }
        public bool Confirmed { get; set; }
    }
}
