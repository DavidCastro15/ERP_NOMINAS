using ERP_NOMINAS.Models.Pieceworks;
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

namespace ERP_NOMINAS.Reports.NonAutomaticPerception.Piecework
{
    public partial class pieceworkListView : ReportForm
    {
        public DateTime d1;
        public DateTime d2;
        private PieceworkRepository _repository = new PieceworkRepository();
        public pieceworkListView()
        {
            InitializeComponent();
        }

        private void pieceworkListView_Load(object sender, EventArgs e)
        {

        }

        private void pieceworkListView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            List<PieceWorkDetailReport> data = _repository.ShowReportDetail(d1,d2);

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", "RELACION DE ASISTENCIAS POR DEPARTAMENTO O USO"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            GenerarReporte(
                "NonAutomaticPerception.Piecework.pieceworkList.rdlc",
                "pieceworkList",
                data,
                parameters
            );
        }
    }
}
