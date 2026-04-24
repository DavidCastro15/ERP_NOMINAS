using ERP_NOMINAS.Models.OffsetsGratuities;
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

namespace ERP_NOMINAS.Reports.NonAutomaticPerception.OffsetsGratuities
{
    public partial class offsetsGratuityListView : ReportForm
    {
        public string Table;
        public int PayWeek;

        private OffsetsGratuitiesRepository _repository = new OffsetsGratuitiesRepository();

        public offsetsGratuityListView()
        {
            InitializeComponent();
        }

        private void offsetsGratuityListView_Load(object sender, EventArgs e)
        {

        }

        private void offsetsGratuityListView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            _repository.Table = Table;
            List<OffsetGratuityReport> data = _repository.ShowDataReport(PayWeek);

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", $"LISTADO DE {Table.ToUpper()} POR LA SEMANA DE PAGO {PayWeek}"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "NonAutomaticPerception.OffsetsGratuities.offsetsGratuityList.rdlc",
                "offsetsGratuityList",
                data,
                parameters
            );
        }
    }
}
