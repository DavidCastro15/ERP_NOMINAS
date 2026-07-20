using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Attendance;
using ERP_NOMINAS.Models.Attendance.ListAttendance;
using ERP_NOMINAS.Repositorys;
using ERP_NOMINAS.Views.AttendanceControl.Attendance.ListAttend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.AttendanceControl.Attendance
{
    
    public partial class AttendShow : BaseForm
    {
        private AttendMain _loadAttend;
        private ListAttendShow _loadDetails;
        private Utilities Util = new Utilities();
        private AttendanceRepository _repository = new AttendanceRepository();
        private ListAttendRepository _repoList = new ListAttendRepository();
        public bool Edit;
        public int Id = 0;
        public int NEmployee = 0;
        public bool EditListExists;
        public int NumberControl;

        public AttendShow(AttendMain loadAttend) : this()
        {
            _loadAttend = loadAttend;
        }

        public AttendShow(ListAttendShow listAttendShow) : this()
        {
            _loadDetails = listAttendShow;
            groupBox1.Visible = true;
        }

        private AttendShow()
        {
            InitializeComponent();
            searchCatalog1.OnItemSelected += searchCatalog1_OnItemSelected;
        }

        private void AttendShow_Load(object sender, EventArgs e)
        {
            // 1. Configuración inicial por defecto
            searchCatalog1.SelectedValue = _repository.GetTop1EmployeeEnable();
            kitCrud1.VisibleBotonCrud(true);

            if (!Edit) return; // Salida temprana si no es edición

            // 2. Acciones comunes para cualquier tipo de edición
            kitCrud1.VisibleBotonCrud(false);
            searchCatalog1.Enabled = false;

            if (EditListExists)
            {
                var resEdit = _repoList.GetListAttendDetail(NumberControl, NEmployee);

                searchCatalog1.SelectedValue = NEmployee;
                searchCatalog2.SelectedValue = resEdit.CategoryWorked;
                searchCatalog3.SelectedValue = resEdit.UseWorked;
                searchTurn1.SelectedTurntId = resEdit.ShiftWorked;

                // Simplificación de RadioButtons
                radioButton1.Checked = (resEdit.Status == "Normal");
                radioButton2.Checked = (resEdit.Status != "Normal");
            }
            else
            {
                var res = _repository.GetAttend(NEmployee);

                searchCatalog1.SelectedValue = res.NumberEmployee;
                searchCatalog2.SelectedValue = res.CategoryWorked;
                searchCatalog3.SelectedValue = res.UseWorked;
                searchTurn1.SelectedTurntId = res.TurnWorked;
                dateTimePicker1.Value = res.EntryDate;
            }

        }

        private void searchCatalog1_OnItemSelected(object sender, EventArgs e)
        {
            if (searchCatalog1.SelectedValue != null)
            {
                searchCatalog2.FilterByEmployee(Convert.ToInt32(searchCatalog1.SelectedValue));
            }

        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            // 1. Confirmación inmediata
            var result = MessageBox.Show("¿Está seguro de que desea guardar este registro?",
                "Confirmar Guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Ejecución según el contexto
                if (EditListExists)
                {
                    _repoList.AddEmployeeDetails(ShowAttendDetails());
                    _loadDetails.LoadGrid();
                }
                else
                {
                    _repository.AddAttendEmployee(ShowAttend());
                    _loadAttend.LoadGrid();
                }

                // 3. Éxito común y cierre
                MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Asegura que el padre sepa que se guardó
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            // 1. Validación temprana: Preguntar antes de procesar datos
            var confirm = MessageBox.Show("¿Está seguro de que desea editar este registro?",
                "Confirmar Edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Ejecutar lógica según el estado
                if (EditListExists)
                {
                    _repoList.UpdateEmployeeListAttend(ShowAttendDetails());
                    _loadDetails.LoadGrid();
                }
                else
                {
                    _repository.UpdateAtttend(ShowAttend());
                }

                // 3. Éxito común para ambos casos
                MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AttendShow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 2. Ejecuta la misma función de guardado pasando parámetros vacíos
                kitCrud1_Edit(this, EventArgs.Empty);

                // 3. Detiene el sonido "beep" de Windows y evita que el Enter salte a otro control
                e.SuppressKeyPress = true;
            }
        }

        private Attend ShowAttend()
        {
            return new Attend
            {
                Id = Convert.ToInt32(Id),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                CategoryWorked = Convert.ToInt32(searchCatalog2.SelectedValue),
                UseWorked = Convert.ToInt32(searchCatalog3.SelectedValue),
                TurnWorked = Convert.ToInt32(searchTurn1.SelectedTurntId),
                EntryDate = Convert.ToDateTime(dateTimePicker1.Value),
                EntryTime = DateTime.Today,
                DepartureTime = DateTime.Today,
                Status = Convert.ToString(1)
            };
        }

        private ListAttendDetail ShowAttendDetails()
        {
            var Status = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            return new ListAttendDetail
            {
                NumberControl = Convert.ToInt32(NumberControl),
                PayRoll = 1,
                Status = Convert.ToString(Status.Text),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                UseWorked = Convert.ToInt32(searchCatalog3.SelectedValue),
                CategoryWorked = Convert.ToInt32(searchCatalog2.SelectedValue),
                ShiftWorked = Convert.ToInt32(searchTurn1.SelectedTurntId)
            };
        }

       
    }
}
