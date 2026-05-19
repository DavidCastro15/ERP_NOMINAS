using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.SupportTransportations
{
    public interface IAuthorizeTransport
    {
        List<AuthorizeTransport> GetAuthorizeTransports();
        int AddEmployeeTransport(AuthorizeTransport a);
        int DeleteEmployee(int Id);
    }
}
