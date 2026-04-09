using ERP_NOMINAS.Models.Attendance.ListAttendance;
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

namespace ERP_NOMINAS.Reports.AttendanceLists
{
    public partial class attendanceListView : ReportForm
    {
        private ListAttendRepository _repository = new ListAttendRepository();
        public int ControlNumber;
        public attendanceListView()
        {
            InitializeComponent();
        }

        private void attendanceListView_Load(object sender, EventArgs e)
        {

        }

        private void attendanceListView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            List<ListAttendDetailReport> data = _repository.GetAttendDataReport(ControlNumber);

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", AppConstants.CompanyName),
        new ReportParameter("ReportTitle", "RELACION DE LISTA DE ASISTENCIA GENERADA POR DEPARTAMENTO O USO"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            GenerarReporte(
                "Attendance.AttendanceLists.attendanceList.rdlc",
                "attendanceList",
                data,
                parameters
            );
        }
    }
}
