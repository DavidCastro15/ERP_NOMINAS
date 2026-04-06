using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.IntegratedSalaries;
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

namespace ERP_NOMINAS.Views.Master.IntegratedSalaries
{
    public partial class SkipImss : BaseForm
    {
        Utilities Util = new Utilities();
        IntegratedSalaryRepository _repository = new IntegratedSalaryRepository();
        private int IdEmployee = 0;


        public SkipImss()
        {
            InitializeComponent();
        }

        private void SkipImss_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListIntegratedSalary = _repository.GetSkipIntegratedSalaries();
            Util.ConfigGrid<IntegratedSalary>(dataGridView1);
            dataGridView1.DataSource = new BindingList<IntegratedSalary>(ListIntegratedSalary);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IdEmployee <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea habilitar este empleado?";
            string title = "Confirmar";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                var disabledEmployee = _repository.StatusEmployee(false, IdEmployee);
                MessageBox.Show("Empleado habilitado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdEmployee = Convert.ToInt32(row.Cells[1].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdEmployee = Convert.ToInt32(row.Cells[1].Value);
            }
        }
    }
}
