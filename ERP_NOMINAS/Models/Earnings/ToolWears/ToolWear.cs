using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.ToolWears
{
    public class ToolWear
    {
        [DisplayName("No. Empleado")]
        public int NumberEmployee { get; set; }
        [DisplayName("Nombre Completo")]
        public string FullName { get; set; }
        [DisplayName("Dias")]
        public int Days { get; set; }
        [DisplayName("Id Cat.")]
        public int CategoryId { get; set; }
        [DisplayName("Nombre Categoria")]
        public string CategoryName { get; set; }
        [DisplayName("Importe")]
        public decimal Amount { get; set; }
        [DisplayName("Uso")]
        public int Use { get; set; }
    }
}
