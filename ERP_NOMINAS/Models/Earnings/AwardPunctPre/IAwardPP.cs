using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.AwardPunctPre
{
    public interface IAwardPP
    {
        List<LastAward> GetLastAward();
        List<AwardPPF> GetAwardsOverview();
        List<ListAttend> GetDetailAttend(DateTime StartDate, DateTime EndDate);
        List<ListAttend> FilterByNameListAttend(int NumberEmployee);
        List<ListAttend> ClearFilterByNameListAttend();
        List<PieceworkAward> GetPieceworkAwards();
        AwardPPF GetAward(int Id);
        void AwardsOverview(int monthlyTarget);
        void ImportEmployees(DataTable dtCsv, decimal pieceWork);
        int SavePiecewokAward(List<PieceworkAward> list);
        int UpdateListAttend(List<ListAttend> list);
        int CreateAwardEmployee(AwardPPF aPF);
        int UpdateAwardEmployee(AwardPPF aPF);
        int DeleteAwardEmployee(int Id);
        decimal GetSalaryEmployee(int NumberEmployee);
        int SaveAwardOverview(int payWeek);
        List<AwardReport> GetDataPrintAwards(int payWeek, bool temp);
    }
}
