using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Holidays
{
    public class Holiday
    {
        public int Id { get; set; }
        [DisplayName("Dia")]
        public DateTime Day { get; set; }
        [DisplayName("Festividad")]
        public string _Holiday { get; set; }

       
    }
}
