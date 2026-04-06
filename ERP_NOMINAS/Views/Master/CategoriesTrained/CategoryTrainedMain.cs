using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.CategoriesTrained;
using ERP_NOMINAS.Repositorys;
using ERP_NOMINAS.Views.Master.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ERP_NOMINAS.GlobalFunctions.Utilities;

namespace ERP_NOMINAS.Views.Master.CategoriesTrained
{
    public partial class CategoryTrainedMain : BaseForm
    {
        Utilities Util = new Utilities();
        CategoryTrainedRepository _repository = new CategoryTrainedRepository();
        private EmployeeMain _EmployeeMain;
        public string _nameEmployee;

        public int IdEmployee = 0;
        private int Id = 0;
        public CategoryTrainedMain(EmployeeMain employeeMain)
        {
            InitializeComponent();
            _EmployeeMain = employeeMain;
        }

        private void CategoryTrainedMain_Load(object sender, EventArgs e)
        {                    
            label1.Text = _nameEmployee;
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListCategoriesTriened = _repository.GetTrainedCategories(IdEmployee);
            Util.ConfigGrid<CategoryTrained>(dataGridView1);
            dataGridView1.DataSource = new BindingList<CategoryTrained>(ListCategoriesTriened);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
               
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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

                var deleteEmployee = _repository.DeleteTrainedCategory(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            CategoryTrained ct = ShowCategoryTrained();
            

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createTrainedCategory = _repository.AddTrainedCategory(ct);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);                 
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
       
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public CategoryTrained ShowCategoryTrained()
        {
            return new CategoryTrained
            {
                IdEmployee = Convert.ToInt32(IdEmployee),
                IdCategory = Convert.ToInt32(searchCatalog1.SelectedValue),
                DateAuthorize = Convert.ToDateTime(DateTime.Now),
            };
        }
    }
}
