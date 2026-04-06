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
    public partial class verificationDataView : BaseForm
    {
        private AttendanceRepository _repository = new AttendanceRepository();
        public verificationDataView()
        {
            InitializeComponent();
        }



        private void verificationDataView_Shown(object sender, EventArgs e)
        {
            try
            {
                List<AttendReport> data = _repository.GetAttendDataReport();

                // Limpias orígenes previos
                this.reportViewer1.LocalReport.DataSources.Clear();


                ReportParameter[] parameters = new ReportParameter[]
       {
            new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
            new ReportParameter("ReportTitle", "RELACION DE ASISTENCIAS POR DEPARTAMENTO O USO"),
            new ReportParameter("DateTimeIssue",  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
       };

                // Pasar los parámetros al reporte
                this.reportViewer1.LocalReport.SetParameters(parameters);


                // "verificationData" -> Nombre del DataSet DENTRO del RDLC
                // data -> Tu lista de objetos C#
                ReportDataSource rdc = new ReportDataSource("verificationData", data);

                this.reportViewer1.LocalReport.DataSources.Add(rdc);

                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.Percent; // Ajusta al ancho de ventana
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al vincular los datos: " + ex.Message);
            }
        }

        private void verificationDataView_Load(object sender, EventArgs e)
        {

        }
    }
}
