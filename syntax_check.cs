using System;
using System.Collections.Generic;
using ERP_NOMINAS.Models.PayRollProcess;
using ERP_NOMINAS.Views.PayRollProcess.CalculationsPayRoll.TypePayRoll.UnionWorkers;

class Program {
    static void Main() {
        IPayrollProcessor p = new ProcessRepairUW();
        List<MethodResult> r = p.Process("test", 1, DateTime.Now, DateTime.Now, 0, 0);
    }
}
