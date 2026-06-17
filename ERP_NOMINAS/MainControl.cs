using FormsMaster = ERP_NOMINAS.Views.Master;
using FormsAttend = ERP_NOMINAS.Views.AttendanceControl;
using FormsNonAutomatic = ERP_NOMINAS.Views.Non_AutomaticPerceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ERP_NOMINAS.Views;

namespace ERP_NOMINAS
{
    public partial class MainControl : BaseForm
    {

        public MainControl()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            menuStrip1.Renderer = new MenuToolStripRender();
            menuStrip1.BackColor = Color.White;

            // Darle espacio a los botones para que los rectángulos luzcan mejor
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.Margin = new Padding(2, 1, 2, 1);
            }
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        private void MainControl_Load(object sender, EventArgs e)
        {

        }

        private void OpenForm<T>(Action<T> setup = null) where T : Form, new()
        {
            using (T form = new T())
            {
                setup?.Invoke(form);
                form.ShowDialog();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Categories.CategoryMain>();

        private void percepcionDeduccionToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.PerceptionDeductions.PerceptionDeductionMain>();

        private void empleadosToolStripMenuItem1_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Employees.EmployeeMain>();

        private void comiteSindicalToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.UnionCommittee.UnionCommitteMain>();

        private void materialesToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Materials.MaterialMain>();

        private void festivosToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Holidays.HolidayMain>();

        private void cicloToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Cycle.CycleMain>();

        private void salariosIntegradosToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.IntegratedSalaries.IntegratedSalaryMain>();

        private void omitirImssToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.IntegratedSalaries.SkipImss>();

        private void usosToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Uses.UseMain>();

        private void gerenciasToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Managements.ManagmentMain>();

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Departments.DepartmentMain>();

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Groups.GroupMain>();

        private void equiposToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsMaster.Equipments.EquipmentMain>();

        private void asistenciaToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsAttend.Attendance.AttendMain>();

        private void listasDeAsistenciaToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsAttend.Attendance.ListAttend.ListAttendMain>();

        private void turnosAdicionalesToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsAttend.ExtraShifts.ExtraShiftMain>();

        private void destajosToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.Pieceworks.PieceWorkMain>();

        private void alturasYTemperaturasToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.HeightsTemperatures.HeightTemperatureMain>();

        private void subsidiosToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.Subsidies.SubsidyMain>();

        private void premioPFYPToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.AwardPunctPre.AwardPPMain>();

        private void valesDespensaToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.FoodVauchers.FoodVaucherMain>();

        private void apoyoTransporteToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.SupportTransportations.SupportTransportationMain>();

        private void desgasteHerramientaToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.ToolWears.ToolWearMain>();

        private void estimulo61ToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.Incentives.IncentiveMain>();

        private void toposToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.Tunneling.TunnelMain>();

        private void button1_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomatic.Tunneling.TunnelMain>();

        // --- Formularios Con Parámetros (Plantillas) ---
        private void reparacionToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.Templates.TemplateMain>(f => f.cycleTable = "plantilla_reparacion");

        private void zafraToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.Templates.TemplateMain>(f => f.cycleTable = "plantilla_zafra");

        // --- Formularios de Impuestos ---
        private void impuestoToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.TaxRateMain>(f => { f._useTable = "tarifa_diaria_impuesto"; f.type = " Diaria"; });

        private void impuestoToolStripMenuItem1_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.TaxRateMain>(f => { f._useTable = "tarifa_semanal_impuesto"; f.type = " Semanal"; });

        private void impuestoToolStripMenuItem2_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.TaxRateMain>(f => { f._useTable = "tarifa_mensual_impuesto"; f.type = " Mensual"; });

        // --- Formularios de Subsidios ---
        private void subsidioToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.SubsidyRateMain>(f => { f._useTable = "tarifa_diaria_subsidio"; f.type = " Diaria"; });

        private void subsidioToolStripMenuItem1_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.SubsidyRateMain>(f => { f._useTable = "tarifa_semanal_subsidio"; f.type = " Semanal"; });

        private void subsidioToolStripMenuItem2_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.SubsidyRateMain>(f => { f._useTable = "tarifa_mensual_subsidio"; f.type = " Mensual"; });

        //--- Formularios de Compensaciones y Gratificaciones ---
        private void compensacionesToolStripMenuItem_Click(object sender, EventArgs e) =>
           OpenForm<FormsNonAutomatic.OffsetsGratuities.OffsetGratuityMain>(f => { f.Table = "compensaciones"; });

        private void gratificacionesToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsNonAutomatic.OffsetsGratuities.OffsetGratuityMain>(f => { f.Table = "gratificaciones"; });

    }

}
