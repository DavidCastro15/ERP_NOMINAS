using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.AwardPunctPre
{
    public class ListAttend
    {
        public int Id { get; set; }
        public int NumberEmployee { get; set; }
        public DateTime Date { get; set; }
        public bool ApplyPF { get; set; }
        public bool ApplyPP { get; set; }
    }
}
