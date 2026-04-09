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

namespace ERP_NOMINAS.Reports.Attendance.AddLists
{
    public partial class addListView : ReportForm
    {
        public int PayWeek;
        private ListAttendRepository _repository = new ListAttendRepository();

        public addListView()
        {
            InitializeComponent();
        }

        private void addListView_Load(object sender, EventArgs e)
        {

        }

        private void addListView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            List<ListAttendAddReport> data = _repository.GetAttendAddDataReport(PayWeek);

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", "RELACION DE ASISTENCIAS POR DEPARTAMENTO O USO"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            GenerarReporte(
                "Attendance.AddLists.addList.rdlc",
                "addList",
                data,
                parameters
            );
        }
    }
}
