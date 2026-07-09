using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Auth
{
    public interface IControlPermission
    {
        List<ControlPermission> GetControlPermissions();
        ControlPermission GetControlPermission(int IdUser);
        int CreateUser(ControlPermission ct);
        int UpdateUser(ControlPermission ct);
        int DeleteUser(int Id);
    }
}
