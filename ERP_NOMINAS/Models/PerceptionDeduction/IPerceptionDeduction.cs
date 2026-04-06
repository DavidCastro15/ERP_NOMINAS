using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_NOMINAS.Models.PerceptionDeduction
{
    public interface IPerceptionDeduction
    {
        List<PerceptDeduction> GetPerceptionDeductions();
        PerceptDeduction GetPerceptionDeduction(int Id);
        int CreatePerceptionDeduction(PerceptDeduction pc);
        int UpdatePerceptionDeduction(PerceptDeduction pc);
        int DeletePerceptionDeduction(int Id);
        List<PerceptDeduction> FilterById(int Id);
        bool ExistsId(int Id);
        int CheckNextId();

    }
}
