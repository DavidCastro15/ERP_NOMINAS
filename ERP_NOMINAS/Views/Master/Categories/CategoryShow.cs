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
    public partial class CategoryShow : BaseForm
    {        
        Utilities Util = new Utilities();  
        CategoryRepository _categoryRepository = new CategoryRepository();

        private CategoryMain _CategoryMain;
        public int IdCategory = 0;
        public bool Edit = false;

        public CategoryShow(CategoryMain categoryMain)
        {
            InitializeComponent();
            _CategoryMain = categoryMain;
        }
        
        private void CategoryShow_Load(object sender, EventArgs e)
        {
            int numControl = _categoryRepository.CheckNextId();
            textBox1.Text = numControl.ToString();
            kitCrud1.VisibleBotonCrud(true);
            try
            {
                if (Edit)
                {
                    kitCrud1.VisibleBotonCrud(false);
                    Category c = _categoryRepository.GetCategory(IdCategory);
                    textBox1.Enabled = false;
                    
                    textBox1.Text = IdCategory.ToString();
                    textBox2.Text = Convert.ToString(c.Name);

                    //Search in groupbox not panel
                    foreach (RadioButton rb in groupBox1.Controls.OfType<RadioButton>())
                    {
                        rb.Checked = (rb.Text.Trim() == c.Type.Trim());
                    }

                    switch (c.Food)
                    {
                        case "Si": radioButton5.Checked = true; break;
                        case "No": radioButton6.Checked = true; break;
                    }
                    switch (c.HeightsTemperatures)
                    {
                        case "Si": radioButton7.Checked = true; break;
                        case "No": radioButton8.Checked = true; break;
                    }
                    switch (c.ToolWear)
                    {
                        case "Si": radioButton9.Checked = true; break;
                        case "No": radioButton10.Checked = true; break;
                    }

                    numericUpDown1.Value = c.Salary;
                    numericUpDown2.Value = c.Ranking;
                    numericUpDown3.Value = c.SalaryTurn1;
                    numericUpDown4.Value = c.SalaryTurn2;
                    numericUpDown5.Value = c.SalaryTurn3;
                    numericUpDown6.Value = c.AverageSalary;
                    numericUpDown7.Value = c.AverageSalaryT1xT2;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
         
        }
        
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox1.Focus();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignora la pulsación
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.OnlyLetter(sender, e);
        }

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Category c = ShowCategory();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateCategory = _categoryRepository.UpdateCategory(c);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _CategoryMain.LoadGrid();

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

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Category c = ShowCategory();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createCategory = _categoryRepository.CreateCategory(c);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _CategoryMain.LoadGrid();
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

        private Category ShowCategory()
        {
            var typeChange = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var foodChange = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var heightsTemperaturesChange = groupBox3.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var toolWearChange = groupBox4.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            return new Category
            {
                IdCategory = Convert.ToInt32(textBox1.Text),
                Name = Convert.ToString(textBox2.Text),
                Salary = Convert.ToDecimal(numericUpDown1.Value),
                Type = Convert.ToString(typeChange.Text),
                Food = Convert.ToString(foodChange.Text),
                HeightsTemperatures = Convert.ToString(heightsTemperaturesChange.Text),
                SalaryTurn1 = Convert.ToDecimal(numericUpDown3.Value),
                SalaryTurn2 = Convert.ToDecimal(numericUpDown4.Value),
                SalaryTurn3 = Convert.ToDecimal(numericUpDown5.Value),
                AverageSalary = Convert.ToDecimal(numericUpDown6.Value),
                AverageSalaryT1xT2 = Convert.ToDecimal(numericUpDown7.Value),
                Ranking = Convert.ToInt32(numericUpDown2.Value),
                Priority = Convert.ToInt32(0),
                Classified = Convert.ToString("N/A"),
                ToolWear = Convert.ToString(toolWearChange.Text),
            };
        }
    }
}
