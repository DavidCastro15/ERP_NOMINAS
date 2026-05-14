using ERP_NOMINAS.Models.FoodVauchers;
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

namespace ERP_NOMINAS.Reports.NonAutomaticPerception.FoodVaucherSupportTransportation
{
    public partial class foodVaucherSuportTransportationListView : ReportForm
    {
        private FoodVaucherRepository _repository = new FoodVaucherRepository();
        public int PayWeek;
        public bool FVaucher;

        public foodVaucherSuportTransportationListView()
        {
            InitializeComponent();
        }

        private void FoodVaucherSuportTransportation_Load(object sender, EventArgs e)
        {

        }

        private void FoodVaucherSuportTransportation_Shown(object sender, EventArgs e)
        {
            string tipo = FVaucher ? "VALES DE DESPENSA" : "APOYO DE TRANSPORTE";

            List<FoodVaucherReport> data = _repository.GetDataReport(PayWeek);

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", $"LISTADO DE {tipo} CON SEMANA DE PAGO {PayWeek}"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "NonAutomaticPerception.FoodVaucherSupportTransportation.foodVaucherSupportTransportationList.rdlc",
                "foodVaucherSupportTransportationList",
                data,
                parameters
            );
        }
    }
}
