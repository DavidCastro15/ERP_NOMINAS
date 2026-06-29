using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Tunneling
{
    public class Tunnel
    {
        public int Id { get; set; }
        public int NumberEmployee { get; set; }
        public string FullName { get; set; }
        public int Days { get; set; }
        public decimal PayRate { get; set; }
        public decimal Amount { get; set; }
        public int PayWeek { get; set; }
    }
}
