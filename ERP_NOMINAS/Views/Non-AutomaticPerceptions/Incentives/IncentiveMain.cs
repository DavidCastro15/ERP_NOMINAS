using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Incentives;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.Incentives
{
    public partial class IncentiveMain : BaseForm
    {
        Utilities Util = new Utilities();
        IncentiveRepository _repository = new IncentiveRepository();
        private int Id;
        private int PayWeek;

        public IncentiveMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<Incentive>(_repository.FilterByValue(NumberEmployee));
                }
                else
                {
                    dataGridView1.DataSource = new BindingList<Incentive>(_repository.FilterByValue(text));
                }
            };
        }

        private void IncentiveMain_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "El .CSV solo ingresar datos en este formato id_empleado,importe,semana_pago");
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListIncentive = _repository.GetIncentives();
            Util.ConfigGrid<Incentive>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Incentive>(ListIncentive);
        }

        private void buttonFDelete1_OnBotonDeleteClick(object sender, EventArgs e)
        {
            if (Id <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                var deleteEmployee = _repository.DeleteIncentiveEmployee(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PayWeek <= 0)
            {
                MessageBox.Show("Seleccione un registro con la semana de pago que quiere borrar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                var deletePayWeek = _repository.DeleteIncentivePayWeek(PayWeek);
                MessageBox.Show("Registros eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Id = Convert.ToInt32(row.Cells[0].Value);
                PayWeek = Convert.ToInt32(row.Cells[4].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog { Filter = "CSV Files|*.csv" };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonImport1_OnBotonImportClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que importar este archivo?";
            string title = "Importar importes de Estimulo 61";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                try
                {
                    DataTable dt = Util.GetDataCSV(openFileDialog1.FileName);
                    _repository.ImportEmployees(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                MessageBox.Show("Importacion masiva con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
