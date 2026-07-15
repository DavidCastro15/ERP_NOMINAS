using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.SkipSaving
{
    public interface ISSaving
    {
        List<SSaving> GetSavings();
        List<SSaving> FilterByValue(string Name);
        List<SSaving> FilterByValue(int NumberEmployee);
        SSaving GetSaving(int Id);
        int CreateSaving(SSaving s);
        int DeleteSaving(int Id);
        int CheckExists(int Id);
    }
}
