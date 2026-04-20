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

        protected void ShowReport<T>(string rootRdlc, string nameDataSet, List<T> data, ReportParameter[] parameters)
        {
            try
            {
                string fullRoot = $"ERP_NOMINAS.Reports.{rootRdlc}";
                //"ERP_NOMINAS.Reports.Non-AutomaticPerceptions.PieceworList.pieceworkList.rdlc"
                this.reportViewer1.LocalReport.ReportEmbeddedResource = fullRoot;
                this.reportViewer1.LocalReport.DataSources.Clear();

                if (parameters != null)
                    this.reportViewer1.LocalReport.SetParameters(parameters);

                ReportDataSource rdc = new ReportDataSource(nameDataSet, data);
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
