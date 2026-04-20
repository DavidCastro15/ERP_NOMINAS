using ERP_NOMINAS.Models.Employees;
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

namespace ERP_NOMINAS.Reports.Master.Employees
{
    public partial class employeeListView : ReportForm
    {
        private EmployeeRepository _repository = new EmployeeRepository();

        public employeeListView()
        {
            InitializeComponent();
        }

        private void employeeListView_Load(object sender, EventArgs e)
        {

        }

        private void employeeListView_Shown(object sender, EventArgs e)
        {
            // 1. Obtener los datos
            List<EmployeeReport> data = _repository.GetDataPrintEmployees();

            // 2. Configurar los parámetros
            ReportParameter[] parameters = new ReportParameter[]
            {
        new ReportParameter("CompanyName", "AZUCARERA SAN JOSE DE ABAJO, S.A DE C.V"),
        new ReportParameter("ReportTitle", "LISTADO DE LOS EMPLEADOS ACTIVOS"),
        new ReportParameter("DateTimeIssue", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            ShowReport(
                "Master.Employees.employeeList.rdlc",
                "employeeList",
                data,
                parameters
            );
        }
    }
}
