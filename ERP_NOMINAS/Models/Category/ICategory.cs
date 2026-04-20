using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_NOMINAS.Models.Category
{
    public interface ICategory
    {
        List<Category> GetCategories();
        Category GetCategory(int id);
        int CreateCategory(Category category);
        int UpdateCategory(Category category);
        int DeleteCategory(int id);
        List<Category> ShowFilterSearch(string Name);
        int CheckNextId();
        void ExportToExcel(List<Category> list, string filePath);
        List<CategoryReport> GetDataPrintCategories();

    }
}
