using ERP_NOMINAS.Models.Deductions.SkipSaving;
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

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.SkipSaving
{
    public partial class SkipSavingMain : BaseForm
    {
        private SkipSavingRepository _repository = new SkipSavingRepository();
        private Utilities Util = new Utilities();
        private int Id =0;

        public SkipSavingMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<SSaving>(_repository.FilterByValue(NumberEmployee));
                }
                else
                {
                    dataGridView1.DataSource = new BindingList<SSaving>(_repository.FilterByValue(text));
                }
            };
        }

        private void SkipSavingMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var list = _repository.GetSavings();
            Util.ConfigGrid<SSaving>(dataGridView1);
            dataGridView1.DataSource = new BindingList<SSaving>(list);
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

                var deleteSa = _repository.DeleteSaving(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    if (_repository.CheckExists(Convert.ToInt32(searchCatalog1.SelectedValue))>=1)
                    {
                        MessageBox.Show("Ya existe un empleado en la lista");
                        return;
                    }
                    SSaving ss = new SSaving
                    {
                        Id = Convert.ToInt32(Id),
                        NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                    };
                   
                    var res = _repository.CreateSaving(ss);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGrid();
                    groupBox1.Visible = false;
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
