using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Materials
{
    public class Material
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Material")]
        public string _Material { get; set; }
        [DisplayName("Pago Tonelada")]
        public decimal PayTon { get; set; }
    }
}
