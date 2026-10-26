using System;
using System.Collections.Generic;

namespace ERP_NOMINAS.Models.PayRollProcess
{
    public class MethodResult
    {
        public string MethodName { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public interface IPayrollProcessor
    {
        List<MethodResult> Process(string cycle, int payWeek, DateTime startDate, DateTime endDate, int noWorkingDays = 0, int efectiveDays = 0, int nominaExtra = 0);
    }
}
