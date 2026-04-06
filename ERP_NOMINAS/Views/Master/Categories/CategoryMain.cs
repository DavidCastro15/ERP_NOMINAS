using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Category;
using ERP_NOMINAS.Repositorys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.Master.Categories

{
    public partial class CategoryMain : BaseForm
    {

        Utilities Util = new Utilities();
        CategoryRepository _repository = new CategoryRepository();
        int IdCategory = 0;

        public CategoryMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                dataGridView1.DataSource = new BindingList<Category>(_repository.ShowFilterSearch(text));
            };
        }

        private void CategoryMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListCategories = _repository.GetCategories();
            Util.ConfigGrid<Category>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Category>(ListCategories);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdCategory = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (IdCategory <= 0)
            {
                MessageBox.Show("Seleccione una Categoria", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var catShow = new CategoryShow(this);
            catShow.IdCategory = IdCategory;
            catShow.Edit = true;
            catShow.ShowDialog();

        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var catShow = new CategoryShow(this);
            catShow.ShowDialog();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdCategory <= 0)
            {
                MessageBox.Show("Seleccione una Categoria", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCategorory = _repository.DeleteCategory(IdCategory);
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
