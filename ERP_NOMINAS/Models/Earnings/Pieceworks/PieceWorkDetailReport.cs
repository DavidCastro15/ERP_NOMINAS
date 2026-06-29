using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Pieceworks
{
    public class PieceWorkDetailReport
    {
        public int ReferenceNumber { get; set; }
        public int NumberEmployee { get; set; }
        public int PayWeek { get; set; }
        public string Cycle { get; set; }
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public string Material { get; set; }
        public decimal TonRate { get; set; }
        public decimal TonCharged { get; set; }
        public int QuantityCharged { get; set; } 
        public int Use { get; set; }
    }
}
