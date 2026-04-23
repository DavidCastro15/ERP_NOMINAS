using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.OffsetsGratuities
{
    public interface IOffsetGratuity
    {
        List<OffSetGratuity> GetOffsetsGratuity();
        List<OffSetGratuity> FilterByValue(string name);
        List<OffSetGratuity> FilterByValue(int payWeek);
        int CreateOffsetGratuity(OffSetGratuity o);
        OffSetGratuity GetOffsetGratuityEmployeeDetail(int Id);
        int UpdateOffsetGratuityEmployeeDetail(OffSetGratuity o);
        int DeleteOffsetGratuity(int Id);
    }
}
