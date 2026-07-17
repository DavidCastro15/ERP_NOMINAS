using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Deductions.VariousDiscounts
{
    public interface IDiscount
    {
        List<Discount> GetDiscounts();
        List<Discount> FilterByConcept(int IdConcept);
        List<Discount> FilterByValue(string Name);
        List<Discount> FilterByValue(int NumberEmployee);
        List<DiscountReport> GetDataReport(int IdConcept);
        Discount GetDiscount(int Id);
        int CreateDiscount(Discount d);
        int UpdateDiscount(Discount d);
        int DeleteDiscount(int Id);
       
    }
}
