using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Subsidies;
using ERP_NOMINAS.Reports.NonAutomaticPerception.Subsidies;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.Subsidies
{
    public partial class SubsidyMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private SubsidyRepository _repository = new SubsidyRepository();
        private int Id;

        public SubsidyMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int payweek))
                {
                    dataGridView1.DataSource = new BindingList<Subsidy>(_repository.FilterByValue(payweek));
                }
                else
                {
                    dataGridView1.DataSource = new BindingList<Subsidy>(_repository.FilterByValue(text));
                }
            };
        }

        private void SubsidyMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var res = _repository.GetSubsidies();
            Util.ConfigGrid<Subsidy>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Subsidy>(res);
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var subShow = new SubsidyShow(this);
            subShow.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (Id <= 0)
            {
                MessageBox.Show("Seleccione un subsidio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var res = _repository.DeleteSubidy(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
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
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (Id <= 0)
            {
                MessageBox.Show("Seleccione un Subsidio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var piecehow = new SubsidyShow(this);
            piecehow.Id = Id;
            piecehow.Edit = true;
            piecehow.ShowDialog();

        }

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {
            var report = new subsidyListView();
            report.PayWeek = Convert.ToInt32(searchPayWeek1.SelectedValue);
            report.ShowDialog();
        }
    }
}
