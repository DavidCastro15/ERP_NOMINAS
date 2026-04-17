using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Pieceworks
{
    public class Piecework
    {
        [DisplayName("Folio")]
        public int ReferenceNumber { get; set; }
        [DisplayName("Fecha")]
        public DateTime Date { get; set; }
        [DisplayName("Turno")]
        public int Shift { get; set; }
        [DisplayName("Material")]
        public string Material { get; set; }
        [DisplayName("Pago Tonelada")]
        public decimal TonRate { get; set; }
        [DisplayName("Toneladas Cargadas")]
        public decimal TonCharged { get; set; }
        [DisplayName("N. Cargadores")]
        public int QuantityCharged { get; set; }
        [DisplayName("Semana Pago")]
        public int PayWeek { get; set; }
        public string Cycle { get; set; }
        public int Use { get; set; }
        public int PercepctionId { get; set; }
        public int PayRollId { get; set; }
        public List<PieceworkDetail> Details { get; set; }
    }
}
