using ERP_NOMINAS.Models.Tunneling;
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

namespace ERP_NOMINAS.Reports.NonAutomaticPerception.Tunneling
{
    public partial class tunnelListView : ReportForm
    {
        private TunnelRepository _repository = new TunnelRepository();
        public int PayWeek;
        public tunnelListView()
        {
            InitializeComponent();
        }

        private void tunnelListView_Load(object sender, EventArgs e)
        {

        }

        private void tunnelListView_Shown(object sender, EventArgs e)
        {
            List<TunnelReport> data = _repository.GetDataReport(PayWeek);

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", $"LISTADO DE PAGOS DE TOPOS CON SEMANA DE PAGO {PayWeek}"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "NonAutomaticPerception.Tunneling.tunnelList.rdlc",
                "tunnelList",
                data,
                parameters
            );
        }
    }
}
