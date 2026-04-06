using ERP_NOMINAS.GlobalFunctions;
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
    public partial class TemplateMain : BaseForm
    {

        public string cycleTable = "";
        Utilities Util = new Utilities();
        TemplateRepository _repository = new TemplateRepository();
        private int IdTemplate;

        public TemplateMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                dataGridView1.DataSource = new BindingList<Template>(_repository.FilterByNameEmployee(text));
            };
        }

        private void TemplateMain_Load(object sender, EventArgs e)
        {
            LoadGrid(cycleTable);
            this.Text = cycleTable == "plantilla_zafra" ? "Plantillas Zafra" : "Plantillas Reparación";
        }

        public void LoadGrid(string Cycle)
        {
            _repository.Cycle = Cycle;
            var showTemplates = _repository.GetTemplates();       
            Util.ConfigGrid<Template>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Template>(showTemplates);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdTemplate = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void parametersEdit()
        {

            if (IdTemplate <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var templateShow = new TemplateShow(this);
            templateShow.cycleTable = cycleTable;
            templateShow.IdTemplate = IdTemplate;
            templateShow.Edit = true;
            templateShow.ShowDialog();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var templateShow = new TemplateShow(this);
            templateShow.cycleTable = cycleTable;
            templateShow.ShowDialog();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdTemplate <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                _repository.Cycle = cycleTable;
                var deleteTemplate = _repository.DeleteTemplate(IdTemplate);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid(cycleTable);
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
