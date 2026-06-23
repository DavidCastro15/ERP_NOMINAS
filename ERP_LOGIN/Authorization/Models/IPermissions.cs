using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LOGIN.Authorization.Models
{
    public interface IPermissions
    {
        List<string> GetNameMenuXUser(int idUser);
        List<Catalog> GetPermissionsXUser(int idUser);

        // Para guardar los cambios de la pantalla de gestión
        bool SavePermissions(int idUser, List<int> idsCatalogsSelects);
    }
}
