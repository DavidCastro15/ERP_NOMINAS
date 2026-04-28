using ERP_NOMINAS.Models.HeightsTemperatures;
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

namespace ERP_NOMINAS.Reports.NonAutomaticPerception.HeightTemperatures
{
    public partial class heightTemperatureListView : ReportForm
    {
        public DateTime d1;
        public DateTime d2;
        private HeightTemperatureRepository _repository = new HeightTemperatureRepository();

        public heightTemperatureListView()
        {
            InitializeComponent();
        }

        private void heightTemperatureListView_Load(object sender, EventArgs e)
        {

        }

        private void heightTemperatureListView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            List<HeightTemperatureReport> data = _repository.ShowReportDetail(d1, d2);

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", $"LISTADO DE ALTURAS Y TEMPERATURAS DESDE {d1.ToString("yyyy-MM-dd")} HASTA {d2.ToString("yyyy-MM-dd")}" ),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "NonAutomaticPerception.HeightTemperatures.heightTemperatureList.rdlc",
                "heightTemperatureList",
                data,
                parameters
            );
        }
    }
}
