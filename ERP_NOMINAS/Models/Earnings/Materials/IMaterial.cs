using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Materials
{
    public interface IMaterial
    {
        List<Material> GetMaterials();
        List<Material> FilterByName(string Name);
        Material GetMaterial(int Id);
        int CreateMaterial(Material M);
        int UpdateMaterial(Material m);
        int DeleteMaterial(int Id);
        int IncreasePrice(decimal Price);
    }
}
