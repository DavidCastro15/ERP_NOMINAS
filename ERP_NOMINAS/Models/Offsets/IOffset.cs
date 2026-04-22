using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_NOMINAS.GlobalFunctions.Utilities;

namespace ERP_NOMINAS.Models.Offsets
{
    public interface IOffset
    {
        List<Offset> GetOffsets();
        List<Offset> FilterByValue(string name);
        List<Offset> FilterByValue(int payWeek);
        int CreateOffset(Offset o);
        Offset GetOffsetEmployeeDetail(int Id);
        int UpdateOffsetEmployeeDetail(Offset o);
        int DeleteOffset(int Id);
    }
}
