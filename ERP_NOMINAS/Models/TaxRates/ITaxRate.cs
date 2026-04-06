using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.TaxRates
{
    public interface ITaxRate
    {
        List<TaxRate> GetTaxRates();
        TaxRate GetTaxRate(int Id);
        int CreateTaxRate(TaxRate t);
        int UpdateTaxRate(TaxRate t);
        int DeleteTaxRate(int Id);       
    }
}
