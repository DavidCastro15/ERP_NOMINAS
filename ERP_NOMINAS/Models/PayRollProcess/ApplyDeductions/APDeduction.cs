using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.PayRollProcess.ApplyDeductions
{
    public class APDeduction
    {
        public int Id { get; set; }
        public int IdConcept { get; set; }
        public string NameConcept { get; set; }
        public string Apply { get; set; }
    }
}
