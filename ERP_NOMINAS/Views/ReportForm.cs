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

namespace ERP_NOMINAS.Views
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
        }

        protected void GenerarReporte<T>(string rutaRdlc, string nombreDataSet, List<T> datos, ReportParameter[] parametros)
        {
            try
            {
                string rutaCompleta = $"ERP_NOMINAS.Reports.{rutaRdlc}";
                this.reportViewer1.LocalReport.ReportEmbeddedResource = rutaCompleta;
                this.reportViewer1.LocalReport.DataSources.Clear();

                if (parametros != null)
                    this.reportViewer1.LocalReport.SetParameters(parametros);

                ReportDataSource rdc = new ReportDataSource(nombreDataSet, datos);
                this.reportViewer1.LocalReport.DataSources.Add(rdc);

                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al vincular los datos: " + ex.Message);
            }
        }
    }
}
