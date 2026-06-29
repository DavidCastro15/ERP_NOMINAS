using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.FoodVauchers
{
    public interface IFoodVaucher
    {
        List<FoodVaucher> GetListAttend(DateTime Start, DateTime End, int WorkedDays);
        decimal GetImportFoodVaucher();
        int SaveFoodVauchers(List<FoodVaucher> list,int PayWeek);
        List<FoodVaucherReportExcel> GetDataFoodVaucherExcel(int Payweek);
        List<FoodVaucherReport> GetDataReport(int Payweek);
        void ExportToExcel(List<FoodVaucherReportExcel> list, string filePath);
    }
}
