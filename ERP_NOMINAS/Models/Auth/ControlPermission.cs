using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Auth
{
    public class ControlPermission
    {
        [DisplayName("Id Usuario")]
        public int IdUser { get; set; }
        [DisplayName("Usuario")]
        public string NameUser { get; set; }
        [DisplayName("Contraseña")]
        public string Password { get; set; }
        [DisplayName("Id Role")]
        public int IdRole { get; set; }
        [DisplayName("Role")]
        public string NameRole { get; set; }
        public List<Catalog> Catalogs { get; set; }
    }
}
