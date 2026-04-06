using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.CategoriesTrained
{
    public interface ICategoryTrained
    {
        List<CategoryTrained> GetTrainedCategories(int IdEmployee);
        int AddTrainedCategory(CategoryTrained ct);
        int DeleteTrainedCategory(int IdEmployee);
    }
}
