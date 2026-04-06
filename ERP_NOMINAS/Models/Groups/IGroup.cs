using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Groups
{
    public interface IGroup
    {
        List<Group> GetGroups();
        List<Group> FilterByGroup(int G);
        Group GetGroup(int Id);
        int CreateGroup(Group G);
        int UpdateGroup(Group G);
        int DeleteGroup(int Id);
        bool ExistsGroup(int G);
    }
}
