using ERP_NOMINAS.Models.Deductions.ChildSupport;
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

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.ChildSupports
{
    public partial class ChildSupportMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private ChildSupportRepository _repository = new ChildSupportRepository();
        private int Id;

        public ChildSupportMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<CSupport>(_repository.FilterByValue(NumberEmployee));
                }
                else
                {
                    dataGridView1.DataSource = new BindingList<CSupport>(_repository.FilterByValue(text));
                }
            };
        }

        private void ChildSupportMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var listCs = _repository.GetCSupports();
            Util.ConfigGrid<CSupport>(dataGridView1);
            dataGridView1.DataSource = new BindingList<CSupport>(listCs);
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
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var csShow = new ChildSupportShow(this);
            csShow.Id = Id;
            csShow.Edit = true;
            csShow.ShowDialog();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var cs = new ChildSupportShow(this);
            cs.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (Id <= 0)
            {
                MessageBox.Show("Seleccione una pension", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCs = _repository.DeleteCSupport(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
