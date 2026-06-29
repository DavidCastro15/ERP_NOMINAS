using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.HeightsTemperatures
{
    public interface IHeightTemperature
    {
        List<HeightTemperature> GetHeightTemperatureHeads();
        List<HeightTemperature> FilterByReferenceNumber(int rf);
        List<HeightTemperatureReport> ShowReportDetail(DateTime d1, DateTime d2);     
        HeightTemperature GetHeightTemperature(int rf);
        HeightTemperatureDetail GetDetailEmployee(int NumberEmployee);
        int CreateHeightTemperature(HeightTemperature data);
        int UpdateHeightTemperature(HeightTemperature data);
        int DeleteHeightTemperature(int rf);
        int CheckNextReferenceNumber();
    }
}
