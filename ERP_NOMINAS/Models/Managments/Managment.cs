using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Managments
{
    public class Managment
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Gerencia")]
        public int ManagmentId { get; set; }
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Responsable")]
        public string Responsible { get; set; }
    }
}
