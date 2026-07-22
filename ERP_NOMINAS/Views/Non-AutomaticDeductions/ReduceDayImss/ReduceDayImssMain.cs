using ERP_NOMINAS.Models.Deductions.ReduceDayImss;
using ERP_NOMINAS.Repositorys.Deductions;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.ReduceDayImss
{
    public partial class ReduceDayImssMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private RDImssRepository _repository = new RDImssRepository();
        private int Id;

        public ReduceDayImssMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<RDImss>(_repository.FilterByValue(NumberEmployee));
                }
                else
                {
                    dataGridView1.DataSource = new BindingList<RDImss>(_repository.FilterByValue(text));
                }
            };
        }

        private void ReduceDayImssMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var list = _repository.GetRDImss();
            Util.ConfigGrid<RDImss>(dataGridView1);
            dataGridView1.DataSource = new BindingList<RDImss>(list);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void buttonAdd1_OnBotonAddClick(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            numericUpDown1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            numericUpDown1.Value = 0;
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

                var delete = _repository.DeleteRDImss(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void buttonSave1_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    RDImss rd = new RDImss
                    {
                        NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                        Day = Convert.ToInt32(numericUpDown1.Value)
                    };
                    if (rd.Day <=0)
                    {
                        MessageBox.Show("Ingrese los dias a restar", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var res = _repository.CreateRDImss(rd);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    groupBox1.Visible = false;
                    numericUpDown1.Value = 0;
                    LoadGrid();
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
    }
}
