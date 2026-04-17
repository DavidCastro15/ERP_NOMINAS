using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Pieceworks
{
    public interface IPiecework
    {
        List<Piecework> GetPieceworks();
        List<Piecework> FilterByRefenceNumber(int rf);
        Piecework GetPiecework(int rf);
        PieceworkDetail GetDetailEmployee(int NumberEmployee); 
        int CreatePiecework(Piecework p);
        int UpdatePiecework(Piecework p);
        int CheckNextReferenceNumber();
        int DeletePieceWork(int rf);
        decimal CheckRateTon(string m);
        List<PieceWorkDetailReport> ShowReportDetail(DateTime d1, DateTime d2);
    }
}
