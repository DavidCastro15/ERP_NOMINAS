using ERP_NOMINAS.Models.AwardPunctPre;
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

namespace ERP_NOMINAS.Reports.NonAutomaticPerception.AwardsPPF
{
    public partial class awardsPPFListView : ReportForm
    {
        private AwardPPRepository _repository = new AwardPPRepository();
        public bool Temp;
        public int Payweek;

        public awardsPPFListView()
        {
            InitializeComponent();
        }

        private void awardsPPFListView_Load(object sender, EventArgs e)
        {

        }

        private void awardsPPFListView_Shown(object sender, EventArgs e)
        {
            List<AwardReport> data = _repository.GetDataPrintAwards(Payweek, Temp);

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", $"INFORME DE PREMIO DE PUNTUALIDAD Y PRESENCIA FISICA CON SEMANA DE PAGO: {(Payweek > 0 ? Payweek.ToString() : "Temporal")}"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "NonAutomaticPerception.AwardsPPF.awardsPPFList.rdlc",
                "awardsPPFList",
                data,
                parameters
            );
        }
    }
}
