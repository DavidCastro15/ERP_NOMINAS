using ERP_NOMINAS.Models.Category;
using ERP_NOMINAS.Repositorys;
using ERP_NOMINAS.Views;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Reports.Master.Categories
{
    public partial class categoriesListView : ReportForm
    {
        private CategoryRepository _repository = new CategoryRepository();

        public categoriesListView()
        {
            InitializeComponent();
        }

        private void categoriesListView_Load(object sender, EventArgs e)
        {

        }

        private void categoriesListView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            List<CategoryReport> data = _repository.GetDataPrintCategories();

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", "LISTADO DE LAS CATEGORIAS DISPONIBLES"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "Master.Categories.categoriesList.rdlc",
                "categoryList",
                data,
                parameters
            );
        }
    }
}
