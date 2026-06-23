using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LOGIN.Authorization.Models
{
    public interface IRole
    {
        List<Role> GetRoles();
    }
}
