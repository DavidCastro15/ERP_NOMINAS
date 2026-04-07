using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Attendance.ListAttendance;
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

namespace ERP_NOMINAS.Views.AttendanceControl.Attendance.ListAttend
{
    public partial class ListAttendShow : BaseForm
    {
        private Utilities Util = new Utilities();
        private ListAttendRepository _repository = new ListAttendRepository();
        private ListAttendMain _loadAttend;
        public bool Edit;
        public int ControlNumber;
        private int NEmployee;

        public ListAttendShow(ListAttendMain loadAttend)
        {
            InitializeComponent();
            _loadAttend = loadAttend;
        }

        private void ListAttendShow_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListAttend = _repository.GetDetailsAttend(ControlNumber);
            Util.ConfigGrid<ListAttendDetail>(dataGridView1);
            dataGridView1.DataSource = new BindingList<ListAttendDetail>(ListAttend);
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var att = new AttendShow(this);
            att.NumberControl = ControlNumber;
            att.EditListExists = true;
            att.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (NEmployee <= 0)
            {
                MessageBox.Show("Seleccione un Empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCategorory = _repository.DeleteEmployeeListAttend(ControlNumber, NEmployee);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void parametersEdit()
        {

            if (ControlNumber <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var editAttend = new AttendShow(this);
            editAttend.NumberControl = ControlNumber;
            editAttend.EditListExists = true;
            editAttend.NEmployee = NEmployee;
            editAttend.Edit = true;
            editAttend.ShowDialog();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                NEmployee = Convert.ToInt32(row.Cells[2].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }
    }
}
