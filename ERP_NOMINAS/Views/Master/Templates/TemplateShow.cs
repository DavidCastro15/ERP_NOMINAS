using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Templates;
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


namespace ERP_NOMINAS.Views.Master.Templates
{
    public partial class TemplateShow : BaseForm
    {

        Utilities Util = new Utilities();
        private TemplateRepository _repository = new TemplateRepository();
        private TemplateMain _TemplateMain;
        public string cycleTable = "";
        public int IdTemplate = 0;
        public bool Edit = false;

        public TemplateShow(TemplateMain templateMain)
        {
            InitializeComponent();
            _TemplateMain = templateMain;
        }

        private void TemplateShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {

                kitCrud1.VisibleBotonCrud(false);
                _repository.Cycle = cycleTable;
                Template t = _repository.GetTemplate(IdTemplate);

                searchCatalog1.SelectedValue = t.NumberEmployee;
                searchCatalog2.SelectedValue = t.Use;
                searchCatalog3.SelectedValue = t.Category;
                searchCatalog4.SelectedValue = t.CategoryRequired;
                radioButton1.Checked = t.Status == "Habilitado" ? true : false;
                radioButton2.Checked = t.Status == "Cancelado" ? true : false;
                searchTurn1.SelectedTurntId = t.ShiftPeriod;
                searchTurn2.SelectedTurntId = t.ShiftWorked;
            }

        }
      
        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Template t = ShowTemplate();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.Cycle = cycleTable;
                    var createTemplate = _repository.CreateTemplate(t);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _TemplateMain.LoadGrid(cycleTable);
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

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Template t = ShowTemplate();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var editTemplate = _repository.UpdateTemplate(t);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _TemplateMain.LoadGrid(cycleTable);

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

        private Template ShowTemplate()
        {
            return new Template
            {
                Id = Convert.ToInt32(IdTemplate),
                PayrollId = Convert.ToInt32(1),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                Category = Convert.ToInt32(searchCatalog3.SelectedValue),
                CategoryRequired = Convert.ToInt32(searchCatalog4.SelectedValue),
                ShiftPeriod = Convert.ToInt32(searchTurn1.SelectedTurntId),
                ShiftWorked = Convert.ToInt32(searchTurn2.SelectedTurntId),
                Use = Convert.ToInt32(searchCatalog2.SelectedValue),
                Status = Convert.ToString(radioButton1.Checked == true ? "Habilitado" : "Cancelado")
            };
        }
    }
}
