using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.ToolWears;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.ToolWears
{
    public partial class ToolWearShow : BaseForm
    {
        private Utilities Util = new Utilities();
        private ToolWearRepository _repository = new ToolWearRepository();
        private ToolWear _tool;
        private ToolWearMain _loadMain;
        public bool Edit;

        public ToolWearShow(ToolWearMain loadMain, ToolWear tool)
        {
            InitializeComponent();
            _loadMain = loadMain;
            _tool = tool;
        }

        private void ToolWearShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                searchCatalog1.Enabled = false;
                searchCatalog2.Enabled = false;
                searchCatalog3.Enabled = false;
                searchCatalog1.SelectedValue = _tool.NumberEmployee;
                searchCatalog2.SelectedValue = _tool.CategoryId;
                searchCatalog3.SelectedValue = _tool.Use;
                numericUpDown1.Value = _tool.Days;
                numericUpDown2.Value = Convert.ToDecimal(_tool.Amount);  
            }
            else
            {
                searchCatalog1.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;               
            }
        }

        public ToolWear GetCreateToolWear()
        {
            return _tool;
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            try
            {
                string txtCatalog = searchCatalog1.SelectedText;
                string txtCatalogCat = searchCatalog2.SelectedText;

                string fullName = Util.ExtractPart(txtCatalog, "--", "Empleado Nuevo");
                string nameCategory = Util.ExtractPart(txtCatalogCat, "--", "Cat");

                _tool = new ToolWear
                {
                    NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                    FullName = fullName,
                    CategoryId = Convert.ToInt32(searchCatalog2.SelectedValue),
                    CategoryName = nameCategory,
                    Use = Convert.ToInt32(searchCatalog3.SelectedValue),
                    Days = Convert.ToInt32(numericUpDown1.Value),             
                    Amount = Convert.ToDecimal(numericUpDown2.Value)
                };

                MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.Yes;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            if (_tool == null)
            {
                MessageBox.Show("No hay ningún empleado seleccionado para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _tool.Days = Convert.ToInt32(numericUpDown1.Value);
                    _tool.Amount = Convert.ToDecimal(numericUpDown2.Value);
                                   
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    Close();
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
