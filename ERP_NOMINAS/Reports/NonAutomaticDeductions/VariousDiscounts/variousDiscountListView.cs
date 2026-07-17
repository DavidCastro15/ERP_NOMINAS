using ERP_NOMINAS.Models.Deductions.VariousDiscounts;
using ERP_NOMINAS.Repositorys.Deductions;
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

namespace ERP_NOMINAS.Reports.NonAutomaticDeductions.VariousDiscounts
{
    public partial class variousDiscountListView : ReportForm
    {
        private DiscountRepository _repository = new DiscountRepository();
        public int IdConcept;

        public variousDiscountListView()
        {
            InitializeComponent();
        }

        private void variousDiscountListView_Load(object sender, EventArgs e)
        {

        }

        private void variousDiscountListView_Shown(object sender, EventArgs e)
        {
            // {IdConcept.ToString()}
            List<DiscountReport> data = _repository.GetDataReport(IdConcept);

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", $"INFORME DE DESCUENTOS CON CONCEPTO:"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "NonAutomaticDeductions.VariousDiscounts.variousDiscountList.rdlc",
                "discountList",
                data,
                parameters
            );
        }
    }
}
