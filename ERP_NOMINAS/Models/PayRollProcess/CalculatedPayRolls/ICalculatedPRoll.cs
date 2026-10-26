using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.PayRollProcess.CalculatedPayRolls
{
    public interface ICalculatedPRoll
    {
        List<CalculatedPRolls> GetCalculatedPRolls();
        List<CalculatedPRolls> FilterByPayWeek(int PayWeek);
    }
}
