using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.IntegratedSalaries
{
    public interface IIntegratedSalary
    {
        List<IntegratedSalary> GetIntegratedSalaries();
        List<IntegratedSalary> GetSkipIntegratedSalaries();
        List<IntegratedSalary> FilterByNumberEmployee(int NumberEmployee);
        IntegratedSalary GetIntegratedSalary(int Id);
        int CreateIntegratedSalary(IntegratedSalary I);
        int UpdateIntegratedSalary(IntegratedSalary I);
        int DeleteIntegratedSalary(int Id);
        int StatusEmployee(bool State, int Id);
        void ImportEmployees(DataTable dtCsv);
        void ExportToExcel(List<IntegratedSalaryReport> list, string filePath);
        List<IntegratedSalaryReport> GetDataPrintIntegratedSalary();
    }
}
