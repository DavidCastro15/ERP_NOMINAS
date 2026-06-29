using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.SubsidyFees
{
    public interface ISubsidyFee
    {
        List<SubsidyFee> GetSubsidyFees();
        SubsidyFee GetSubsidyFee(int Id);
        int CreateSubsidyFee(SubsidyFee s);
        int UpdateSubsidyFee(SubsidyFee s);
        int DeleteSubsidyFee(int Id);
    }
}
