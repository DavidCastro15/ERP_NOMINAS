using ERP_NOMINAS.Models.FoodVauchers;
using ERP_NOMINAS.Models.SupportTransportations;
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
        private SupportTransportationRepository _repositoryST = new SupportTransportationRepository();
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
            string tipo = FVaucher ? "APOYO DE TRANSPORTE" : "VALES DE DESPENSA";
            
            object dataSetSource;

            if (FVaucher)
            {
                dataSetSource = _repositoryST.GetDataReport(PayWeek);
             
            }
            else
            {
                dataSetSource = _repository.GetDataReport(PayWeek);
            }

            string rdlcPath = "NonAutomaticPerception.FoodVaucherSupportTransportation.foodVaucherSupportTransportationList.rdlc";
            string dataSetName = "foodVaucherSupportTransportationList";

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", $"LISTADO DE {tipo} CON SEMANA DE PAGO {PayWeek}"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };


            if (FVaucher)
            {
                List<SupportTransportationReport> dataST = _repositoryST.GetDataReport(PayWeek);
                ShowReport(rdlcPath, dataSetName, dataST, parameters);
                
            }
            else
            {
                List<FoodVaucherReport> data = _repository.GetDataReport(PayWeek);
                ShowReport(rdlcPath, dataSetName, data, parameters);
            }          
        }
    }
}
