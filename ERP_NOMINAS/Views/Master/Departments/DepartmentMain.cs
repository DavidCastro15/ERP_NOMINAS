using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Departments;
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

namespace ERP_NOMINAS.Views.Master.Departments
{
    public partial class DepartmentMain : BaseForm
    {
       
        private Utilities Util = new Utilities();
        private DepartmentRepository _repository = new DepartmentRepository();
        private int IdDepartment = 0;

        public DepartmentMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                dataGridView1.DataSource = new BindingList<Department>(_repository.FilterByName(text));
            };
        }

        private void DepartmentMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListDepartment = _repository.GetDepartments();
            Util.ConfigGrid<Department>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Department>(ListDepartment);
        } 

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdDepartment = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var Depar = new DepartmentShow(this);         
            Depar.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdDepartment <= 0)
            {
                MessageBox.Show("Seleccione un departamento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteDepar = _repository.DeleteDepartment(IdDepartment);
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

            if (IdDepartment <= 0)
            {
                MessageBox.Show("Seleccione un departamento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var Depar = new DepartmentShow(this);
            Depar.IdDepartment = IdDepartment;
            Depar.Edit = true;
            Depar.ShowDialog();

        }
    }
}
