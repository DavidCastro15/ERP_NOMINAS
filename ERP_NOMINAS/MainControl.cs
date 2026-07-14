using FormsAuth = ERP_NOMINAS.Views.Auth;
using FormsMaster = ERP_NOMINAS.Views.Master;
using FormsAttend = ERP_NOMINAS.Views.AttendanceControl;
using FormsNonAutomatic = ERP_NOMINAS.Views.Non_AutomaticPerceptions;
using FormsNonAutomaticDeduction = ERP_NOMINAS.Views.Non_AutomaticDeductions;
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
using ERP_SHARED.Auth;
using System.IO;
using System.Diagnostics;

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

            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.Margin = new Padding(2, 1, 2, 1);
            }
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

        }

        private void MainControl_Load(object sender, EventArgs e)
        {

            try
            {
                // 1. Validamos que la sesión no sea nula para evitar caídas
                if (ERP_SHARED.Auth.Sesion.UserIsLoggin != null)
                {
                    // 2. Extraemos el rol forzándolo a String para evitar el conflicto con .Equals
                    string rolUser = ERP_SHARED.Auth.Sesion.UserIsLoggin.NameRole.ToString();

                    // 3. Hacemos la comparación clásica de textos de forma segura
                    if (rolUser.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                    {
                        // 🟢 SI ES ADMINISTRADOR: Simplemente salimos de la función.
                        // No llamamos a TrackForms, el radar global ya está vigilando esta pantalla.
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar rol de usuario: {ex.Message}");
            }

            ApplyFilterSecurity();

        }

        private void ApplyFilterSecurity()
        {
            // Convertimos la lista de permitidos a un HashSet para búsquedas eficientes
            var allowed = new HashSet<string>(Sesion.AuthorizedCatalogs, StringComparer.OrdinalIgnoreCase);

            // Recorremos los menús de la barra principal (Ej: Archivo, Catálogos, Asistencias)
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    // Evaluamos de forma recursiva si el menú o sus hijos deben mostrarse
                    menuItem.Visible = EvaluatePermissionsMenu(menuItem, allowed);
                }
            }
        }

        private bool EvaluatePermissionsMenu(ToolStripMenuItem menuItem, HashSet<string> allowed)
        {
            // No filtramos nunca la opción de salir
            if (menuItem.Name == "salirToolStripMenuItem") return true;

            // CASO 1: Tiene submenús (Es una pestaña padre o contenedor)
            if (menuItem.HasDropDownItems)
            {
                bool childrenVisible = false;

                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    if (subItem is ToolStripMenuItem subMenuItem)
                    {
                        // Llamada recursiva para evaluar al hijo
                        bool childrenAllowed = EvaluatePermissionsMenu(subMenuItem, allowed);
                        subMenuItem.Visible = childrenAllowed;

                        if (childrenAllowed)
                        {
                            childrenVisible = true; // Registramos que este padre tiene contenido útil
                        }
                    }
                    else if (subItem is ToolStripSeparator)
                    {
                        // Opcional: Mantener separadores visuales si deseas, o ignorar
                        subItem.Visible = true;
                    }
                }

                // El menú padre será visible SOLOS si al menos uno de sus hijos se va a mostrar
                return childrenVisible;
            }

            // CASO 2: Es un botón final (Un catálogo directo, no tiene hijos)
            // Es visible si su nombre exacto está en la lista de la base de datos
            return allowed.Contains(menuItem.Name);
        }

        private void OpenForm<T>(Action<T> setup = null) where T : Form, new()
        {
            using (T form = new T())
            {
                setup?.Invoke(form);

                try
                {
                    // 1. Registramos la apertura en la base de datos (Lo que ya funciona súper bien)
                    ERP_SHARED.GlobalFunctions.Logs.Auditor.RegisterChildScreen(form);

                    form.HandleCreated += (sender, e) =>
                    {
                        string nombrePantallaReal = form.GetType().Name;
                // Llamamos a tu método recursivo que ya programamos en SHARED
                ERP_SHARED.GlobalFunctions.Logs.Auditor.MapControlRecursive(form.Controls, nombrePantallaReal);
                    };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al auditar componentes en OpenForm: " + ex.Message);
                }

                form.ShowDialog();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                ERP_SHARED.Auth.Sesion.UserIsLoggin = null;
                ERP_SHARED.Auth.Sesion.AuthorizedCatalogs = null;

                string rutaLogin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ERP_LOGIN.exe");

                if (File.Exists(rutaLogin))
                {
                    Process.Start(rutaLogin); 
                    Application.Exit();      
                }
                else
                {
                    MessageBox.Show("Error: No se encontró el módulo de Login.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}");
            }
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsAuth.PermissionControlMain>();

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<FormsMaster.Categories.CategoryMain>();
        }

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

        private void pensionAlimenticiaToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomaticDeduction.ChildSupports.ChildSupportMain>();

        private void infonavitToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomaticDeduction.Infonavit.InfonavitMain>();

        private void iSRVariable50ToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomaticDeduction.IsrVariable.IsrVariableMain>();

        private void reparacionToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.Templates.TemplateMain>(f => f.cycleTable = "plantilla_reparacion");

        private void zafraToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.Templates.TemplateMain>(f => f.cycleTable = "plantilla_zafra");

        private void impuestoToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.TaxRateMain>(f => { f._useTable = "tarifa_diaria_impuesto"; f.type = " Diaria"; });

        private void impuestoToolStripMenuItem1_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.TaxRateMain>(f => { f._useTable = "tarifa_semanal_impuesto"; f.type = " Semanal"; });

        private void impuestoToolStripMenuItem2_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.TaxRateMain>(f => { f._useTable = "tarifa_mensual_impuesto"; f.type = " Mensual"; });

        private void subsidioToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.SubsidyRateMain>(f => { f._useTable = "tarifa_diaria_subsidio"; f.type = " Diaria"; });

        private void subsidioToolStripMenuItem1_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.SubsidyRateMain>(f => { f._useTable = "tarifa_semanal_subsidio"; f.type = " Semanal"; });

        private void subsidioToolStripMenuItem2_Click(object sender, EventArgs e) =>
            OpenForm<FormsMaster.TaxSubsidy.SubsidyRateMain>(f => { f._useTable = "tarifa_mensual_subsidio"; f.type = " Mensual"; });

        private void compensacionesToolStripMenuItem_Click(object sender, EventArgs e) =>
           OpenForm<FormsNonAutomatic.OffsetsGratuities.OffsetGratuityMain>(f => { f.Table = "compensaciones"; });

        private void gratificacionesToolStripMenuItem_Click(object sender, EventArgs e) =>
            OpenForm<FormsNonAutomatic.OffsetsGratuities.OffsetGratuityMain>(f => { f.Table = "gratificaciones"; });

        private void MainControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. Validamos la razón del cierre. 
            // Si el cierre fue provocado por el código (ej. cuando usas Application.Exit() en el botón Salir), 
            // dejamos que el flujo continúe normal para evitar bucles infinitos.
            if (e.CloseReason == CloseReason.ApplicationExitCall) return;

            try
            {
                // 2. Ejecutamos exactamente la misma lógica de limpieza que tu botón de menú
                ERP_SHARED.Auth.Sesion.UserIsLoggin = null;
                ERP_SHARED.Auth.Sesion.AuthorizedCatalogs = null;

                string rutaLogin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ERP_LOGIN.exe");

                if (File.Exists(rutaLogin))
                {
                    // Lanzamos el Login limpio antes de que esta pantalla muera
                    Process.Start(rutaLogin);

                    // 3. Le decimos a Windows: "Cancela el cierre ordinario de este Form, 
                    // nosotros nos encargaremos de apagar la aplicación de forma limpia"
                    e.Cancel = false;
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Error: No se encontró el módulo de Login.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar el sistema desde la X: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomaticDeduction.IsrAdjust.IsrAdjustMain>();

        private void ajusteIsrToolStripMenuItem_Click(object sender, EventArgs e) => OpenForm<FormsNonAutomaticDeduction.IsrAdjust.IsrAdjustMain>();
    }

}
