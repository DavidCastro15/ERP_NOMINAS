using ERP_NOMINAS.Conexion;
using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Category;
using ERP_NOMINAS.Reports.Master.Categories;
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener datos del repositorio
                var data = _repository.GetCategories();

                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("No hay información para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                    sfd.FileName = $"Reporte_Categorias_{DateTime.Now:ddMMyyyy}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // Llamar al método del repositorio
                        _repository.ExportToExcel(data, sfd.FileName);

                        MessageBox.Show("Archivo Excel generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {
            var reportPrint = new categoriesListView();
            reportPrint.ShowDialog();
        }
    }

}
