using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Attendance;
using ERP_NOMINAS.Repositorys;
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
        private Utilities Util = new Utilities();
        private AttendanceRepository _repository = new AttendanceRepository();
        public bool Edit;
        public int Id;
        public int NEmployee = 0;

        public AttendShow(AttendMain loadAttend)
        {
            InitializeComponent();
            _loadAttend = loadAttend;
            searchCatalog1.OnItemSelected += searchCatalog1_OnItemSelected;
        }

        private void AttendShow_Load(object sender, EventArgs e)
        {
            searchCatalog1.SelectedValue = _repository.GetTop1EmployeeEnable();
            kitCrud1.VisibleBotonCrud(true);
            
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                searchCatalog1.Enabled = false;
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
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {

                    Attend att = ShowAttend();
                    _repository.AddAttendEmployee(att);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadAttend.LoadGrid();
                }
                else
                {
                    MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Attend c = ShowAttend();

            try
            {

                if (ress == DialogResult.Yes)
                {
                    var updateAttend = _repository.UpdateAtttend(c);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ESTO ES CLAVE: Indica que terminó con éxito y cierra
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }


                //if (ress == DialogResult.Yes)
                //{
                //    var updateAttend = _repository.UpdateAtttend(c);
                //    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    _loadAttend.LoadGrid();
                //    _loadAttend.parametersEdit(_repository.ChechNextNumberEmployee(NEmployee));
                //    this.Close();
                //}
                else
                {
                    MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                DepartureTime = DateTime.Today
            };
        }
    }
}
