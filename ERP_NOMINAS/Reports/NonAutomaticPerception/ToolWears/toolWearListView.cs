using ERP_NOMINAS.Models.ToolWears;
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

namespace ERP_NOMINAS.Reports.NonAutomaticPerception.ToolWears
{
    public partial class toolWearListView : ReportForm
    {
        private ToolWearRepository _repository = new ToolWearRepository();
        public int PayWeek;
        public bool Temp;
        public toolWearListView()
        {
            InitializeComponent();
        }

        private void toolWearListView_Load(object sender, EventArgs e)
        {

        }

        private void toolWearListView_Shown(object sender, EventArgs e)
        {
            string Month = Convert.ToDateTime(DateTime.Now).AddMonths(-1).ToString("MMMM").ToUpper();
            decimal Amount = _repository.GetImportToolWear();

            string headerTitle = $"RELACION DE TRABAJADORES A LOS CUALES LA EMPRESA AZUCARERA INGENIO SAN JOSE DE ABAJO, S. A. DE C. V." + Environment.NewLine +
                     $"SE LE PAGA DESGASTE DE HERRAMIENTA CORRESPONDIENTE A {Month} DE {DateTime.Now.Year} DE ACUERDO A" + Environment.NewLine +
                     $"LAS PLATICAS VERBALES QUE SE TUVIERON CON EL ING. RODOLFO PERDOMO BUENO EL DIA 07 DE FEBRERO DE 1998," + Environment.NewLine +
                     $"DONDE SE ACUERDA QUE DICHO PAGO SERIA DE $ 30.00 MENSUALES QUEDANDO SUJETO A LOS" + Environment.NewLine +
                     $"INCREMENTOS SALARIALES QUE EXISTEN DENTRO DE LA INDUSTRIA AZUCARERA" + Environment.NewLine +
                     $"PAGO POR MES ACTUALMENTE {Amount.ToString()}";

            List<ToolWearReport> data = _repository.GetDataReport(PayWeek,Temp);

            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", "PAGO DE DESGASTE DE HERRAMIENTAS"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
        new ReportParameter("HeaderTitle", headerTitle),
            };

            ShowReport(
                "NonAutomaticPerception.ToolWears.toolWearList.rdlc",
                "toolWearList",
                data,
                parameters
            );
        }
    }
}
