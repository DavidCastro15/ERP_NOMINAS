using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.ToolWears
{
    public class ToolWearReport
    {
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public int Days { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public int PayWeek { get; set; }
    }
}
