using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Managments
{
    public interface IManagment
    {
        List<Managment> GetManagments();
        Managment GetManagment(int Id);
        int CreateManagment(Managment managment);
        int UpdateManagment(Managment managment);
        int DeleteManagment(int Id);
        List<Managment> FilterByManagment(string Name);
        int CheckNextId();
    }
}
