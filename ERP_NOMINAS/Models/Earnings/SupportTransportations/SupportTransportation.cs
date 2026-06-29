using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.SupportTransportations
{
    public class SupportTransportation
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Numero Empleado")]
        public int NumberEmployee { get; set; }
        [DisplayName("Nombre Completo")]
        public string FullName { get; set; }
        [DisplayName("Dias Habiles")]
        public int BusinessDays { get; set; }
        [DisplayName("Dias Laborados")]
        public int WorkedDays { get; set; }
        [DisplayName("Porc. Ant")]
        public decimal PorcAnt { get; set; }
        [DisplayName("Porc. Act")]
        public decimal PorcAct { get; set; }
        [DisplayName("Importe")]
        public decimal Amount { get; set; }

        public byte D1 { get; set; }
        public byte D2 { get; set; }
        public byte D3 { get; set; }
        public byte D4 { get; set; }
        public byte D5 { get; set; }
        public byte D6 { get; set; }
        public byte D7 { get; set; }
        public byte D8 { get; set; }
        public byte D9 { get; set; }
        public byte D10 { get; set; }
        public byte D11 { get; set; }
        public byte D12 { get; set; }
        public byte D13 { get; set; }
        public byte D14 { get; set; }
        public byte D15 { get; set; }
        public byte D16 { get; set; }
        public byte D17 { get; set; }
        public byte D18 { get; set; }
        public byte D19 { get; set; }
        public byte D20 { get; set; }
        public byte D21 { get; set; }
        public byte D22 { get; set; }
        public byte D23 { get; set; }
        public byte D24 { get; set; }
        public byte D25 { get; set; }
        public byte D26 { get; set; }
        public byte D27 { get; set; }
        public byte D28 { get; set; }
        public byte D29 { get; set; }
        public byte D30 { get; set; }
        public byte D31 { get; set; }


        public int PayrollId { get; set; }
        public int Payweek { get; set; }
    }
}
