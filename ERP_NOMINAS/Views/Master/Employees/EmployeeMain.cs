using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Employees;
using ERP_NOMINAS.Repositorys;
using ERP_NOMINAS.Views.Master.CategoriesTrained;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.Master.Employees
{
    public partial class EmployeeMain : BaseForm
    {
        Utilities Util = new Utilities();
        EmployeeRepository _repository = new EmployeeRepository();
        private int IdEmployee = 0;
        private int idEmp = 0;
        private string _nameEmployee;

        public EmployeeMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                dataGridView1.DataSource = new BindingList<Employee>(_repository.FilterByName(text));
            };

        }

        private void EmployeeMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            List<Employee> listEmployee = _repository.GetEmployees();
            Util.ConfigGrid<Employee>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Employee>(listEmployee);
            IdEmployee = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value);
            idEmp = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value);
            _nameEmployee = $"{Convert.ToString(dataGridView1.Rows[0].Cells[2].Value)} {Convert.ToString(dataGridView1.Rows[0].Cells[3].Value)} {Convert.ToString(dataGridView1.Rows[0].Cells[4].Value)}";
        }

        private void parametersEdit()
        {

            if (IdEmployee <= 0)
            {
                MessageBox.Show("Seleccione un Empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var empShow = new EmployeeShow(this);
            empShow.IdEmployee = IdEmployee;
            empShow.Edit = true;
            empShow.ShowDialog();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdEmployee = Convert.ToInt32(row.Cells[0].Value);
                idEmp = Convert.ToInt32(row.Cells[1].Value);
                _nameEmployee = $"{Convert.ToString(row.Cells[2].Value)} {Convert.ToString(row.Cells[3].Value)} {Convert.ToString(row.Cells[4].Value)}";
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var cc = new CategoryTrainedMain(this);
            cc.IdEmployee = idEmp;
            cc._nameEmployee = _nameEmployee;
            cc.ShowDialog();
        }
      
        private void kitForm1_Add(object sender, EventArgs e)
        {
            var emp_Show = new EmployeeShow(this);
            emp_Show.ShowDialog();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdEmployee <= 0)
            {
                MessageBox.Show("Seleccione un Empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteEmployee = _repository.DeleteEmployee(IdEmployee);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }
    }
}
