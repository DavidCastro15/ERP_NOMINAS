using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.TaxRates;
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

namespace ERP_NOMINAS.Views.Master.TaxSubsidy
{
    public partial class TaxRateMain : BaseForm
    {
        public string type;
        public string _useTable;
        private int IdFee=0;

        Utilities Util = new Utilities();
        TaxRateRepository _repository = new TaxRateRepository();

        public TaxRateMain()
        {
            InitializeComponent();
        }

        private void TaxRateMain_Load(object sender, EventArgs e)
        {
            this.Text += type;
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            _repository.useTable = _useTable;
            var ListFees = _repository.GetTaxRates();
            Util.ConfigGrid<TaxRate>(dataGridView1);
            dataGridView1.DataSource = new BindingList<TaxRate>(ListFees);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdFee = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (IdFee <= 0)
            {
                MessageBox.Show("Seleccione una tarifa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
            var feeShow = new TaxRateShow(this);
            feeShow.useTable = _useTable;
            feeShow.IdFee = IdFee;
            feeShow.type = type;
            feeShow.Edit = true;
            feeShow.ShowDialog();

        }

        private void kitForm1_Add(object sender, EventArgs e)
        {      
            var feeShow = new TaxRateShow(this);
            feeShow.useTable = _useTable;
            feeShow.type = type;
            feeShow.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdFee <= 0)
            {
                MessageBox.Show("Seleccione una tarifa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                _repository.useTable = _useTable;
                var deleteFee = _repository.DeleteTaxRate(IdFee);
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
