using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LOGIN.Authorization.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string NameCatalog { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
    }
}
