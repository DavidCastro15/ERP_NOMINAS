using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Materials;
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

namespace ERP_NOMINAS.Views.Master.Materials
{
    public partial class MaterialMain : BaseForm
    {
        Utilities Util = new Utilities();
        MaterialRepository _repository = new MaterialRepository();
        private int IdMaterial = 0;

        public MaterialMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                dataGridView1.DataSource = new BindingList<Material>(_repository.FilterByName(text));
            };
        }

        private void MaterialMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListMaterial = _repository.GetMaterials();
            Util.ConfigGrid<Material>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Material>(ListMaterial);
        }    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdMaterial = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (IdMaterial <= 0)
            {
                MessageBox.Show("Seleccione un Material", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var materialShow = new MaterialShow(this);
            materialShow.IdMaterial = IdMaterial;
            materialShow.Edit = true;
            materialShow.ShowDialog();

        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var materialShow = new MaterialShow(this);
            materialShow.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdMaterial <= 0)
            {
                MessageBox.Show("Seleccione un Material", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteMaterial = _repository.DeleteMaterial(IdMaterial);
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
            string message = "¿Está seguro de que desea aumentar el pocentaje del precio del material?";
            string title = "Confirmar aumento";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var increasePrice = _repository.IncreasePrice(Convert.ToDecimal(numericUpDown1.Value));
                MessageBox.Show("Aumento realizado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
                groupBox2.Visible = false;
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }
    }
}
