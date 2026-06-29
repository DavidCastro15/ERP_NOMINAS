using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.UnionCommittee
{
    public interface IUnionCom
    {
        List<UnionCom> GetUnionCommittees();
        List<UnionCom> FilterByName(string Name);
        UnionCom GetGetUnionCommittee(int Id);
        int CreateUnionCommitte(UnionCom U);
        int UpdateUnionCommitte(UnionCom U);
        int DeleteUnionCommitte(int Id);
    }
}
