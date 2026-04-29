using ERP_NOMINAS.Models.Subsidies;
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

namespace ERP_NOMINAS.Reports.NonAutomaticPerception.Subsidies
{
    public partial class subsidyListView : ReportForm
    {
        public int PayWeek;
        private SubsidyRepository _repository = new SubsidyRepository();

        public subsidyListView()
        {
            InitializeComponent();
        }

        private void subsidyListView_Load(object sender, EventArgs e)
        {

        }

        private void subsidyListView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            List<SubsidyReport> data = _repository.ShowDataReport(PayWeek);

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", "LISTADO DE SUBSIDIOS"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "NonAutomaticPerception.Subsidies.subsidyList.rdlc",
                "subsidyList",
                data,
                parameters
            );
        }
    }
}
