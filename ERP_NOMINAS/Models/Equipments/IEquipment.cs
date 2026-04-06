using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Equipments
{
    public interface IEquipment
    {
        List<Equipment> GetEquipments();
        List<Equipment> FilterByEquipment(int E);
        Equipment GetEquipment(int Id);
        int CreateEquipment(Equipment E);
        int UpdateEquipment(Equipment E);
        int DeleteEquipment(int Id);
        bool ExistsEquipment(int E);
    }
}
