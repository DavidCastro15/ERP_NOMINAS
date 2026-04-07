using ERP_NOMINAS.Models.Attendance;
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

namespace ERP_NOMINAS.Reports.AttendanceControl
{
    public partial class verificationDataView : ReportForm
    {
        private AttendanceRepository _repository = new AttendanceRepository();
        public verificationDataView()
        {
            InitializeComponent();
        }

        private void verificationDataView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            List<AttendReport> data = _repository.GetAttendDataReport();

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", "RELACION DE ASISTENCIAS POR DEPARTAMENTO O USO"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            GenerarReporte(
                "AttendanceControl.verificationData.rdlc",
                "verificationData",
                data,
                parameters
            );
        }

        private void verificationDataView_Load(object sender, EventArgs e)
        {

        }
    }
}
