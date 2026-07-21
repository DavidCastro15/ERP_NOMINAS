using ERP_NOMINAS.Models.Deductions.DiscountGlobal;
using ERP_NOMINAS.Repositorys.Earnings;
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

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.DiscountGlobal
{
    public partial class DiscountGlobalMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private DiscountGlobalRepository _repository = new DiscountGlobalRepository();
        private int Id;

        public DiscountGlobalMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<DGlobal>(_repository.FilterByConcept(NumberEmployee));
                }
            };
        }

        private void DiscountGlobalMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var list = _repository.GetDGlobals();
            Util.ConfigGrid<DGlobal>(dataGridView1);
            dataGridView1.DataSource = new BindingList<DGlobal>(list);
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
                MessageBox.Show("Seleccione un descuento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var show = new DiscountGlobalShow(this);
            show.Id = Id;
            show.Edit = true;
            show.ShowDialog();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var show = new DiscountGlobalShow(this);
            show.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (Id <= 0)
            {
                MessageBox.Show("Seleccione un descuento", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var delete = _repository.DeleteDGlobal(Id);
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
