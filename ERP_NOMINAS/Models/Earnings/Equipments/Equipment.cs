using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Equipments
{
    public class Equipment
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Equipo")]
        public int Equip { get; set; }
        [DisplayName("Descripcion")]
        public string Description { get; set; }
        [DisplayName("Grupo")]
        public int Group { get; set; }
    }
}
