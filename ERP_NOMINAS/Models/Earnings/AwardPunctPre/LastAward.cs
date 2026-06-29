using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.AwardPunctPre
{
    public class LastAward
    {
        [DisplayName("Empleado")]
        public int NumberEmployee { get; set; }
        [DisplayName("PF")]
        public decimal DaysPF { get; set; }
        [DisplayName("Importe PF Ant.")]
        public decimal LastAmountPF { get; set; }
        [DisplayName("PP")]
        public decimal DaysPP { get; set; }
        [DisplayName("Importe PP Ant.")]
        public decimal LastAmountPP { get; set; }
        [DisplayName("Mes")]
        public int PayWeek { get; set; }
    }
}
