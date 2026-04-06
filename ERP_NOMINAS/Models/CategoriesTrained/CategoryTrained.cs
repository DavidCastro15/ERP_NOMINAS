using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.CategoriesTrained
{
    public class CategoryTrained
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Id Categoria")]
        public int IdCategory { get; set; }
        [DisplayName("Nombre Categoria")]
        public string NameCategory { get; set; }
        public int IdEmployee { get; set; }
        public string NameEmployee { get; set; }
        public DateTime DateAuthorize { get; set; }
    }
}
