using ERP_NOMINAS.Models.Deductions.VariousDiscounts;
using ERP_NOMINAS.Reports.NonAutomaticDeductions.VariousDiscounts;
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

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.VariousDiscounts
{
    public partial class DiscountMain : BaseForm
    {
        private DiscountRepository _repository = new DiscountRepository();
        private Utilities Util = new Utilities();
        private int Id = 0;

        public DiscountMain()
        {
            InitializeComponent();
            
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<Discount>(_repository.FilterByValue(NumberEmployee));
                }
                else
                {
                    dataGridView1.DataSource = new BindingList<Discount>(_repository.FilterByValue(text));
                }
            };

            filterByT2.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<Discount>(_repository.FilterByConcept(NumberEmployee));
                }
                
            };
        }

        private void DiscountMain_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["Column10"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var list = _repository.GetDiscounts();
            Util.ConfigGrid<Discount>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Discount>(list);
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
            var ds = new DiscountShow(this);
            ds.Id = Id;
            ds.Edit = true;
            ds.ShowDialog();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var show = new DiscountShow(this);
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
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCs = _repository.DeleteDiscount(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {
            var report = new variousDiscountListView();
            report.IdConcept = Convert.ToInt16(searchCatalog1.SelectedValue);
            report.ShowDialog();
        }
    }
}
