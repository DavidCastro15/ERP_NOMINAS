using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.Infonavit
{
    public class DInfonavit
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("N. Empleado")]
        public int NumberEmployee { get; set; }

        [DisplayName("Nombre Completo")]
        public string FullName { get; set; }

        [DisplayName("Cantidad Total")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Cantidad Pagada")]
        public decimal AmountPaid { get; set; }      

        [DisplayName("N. Abonos")]
        public int CreditContributions { get; set; }

        [DisplayName("Fecha Credito")]
        public DateTime? CreditDate { get; set; }

        [DisplayName("Fecha U. Pago")]
        public DateTime? LastDatePay { get; set; }

        [DisplayName("Descuento Diario")]
        public decimal PeriodicDeduction { get; set; }

        [DisplayName("Porcentaje")]
        public decimal PercentageToBepaid { get; set; }

       
       
        public string CalculatePercentage { get; set; }
        public string Comments { get; set; }
        public int? Status { get; set; }
    }
}
