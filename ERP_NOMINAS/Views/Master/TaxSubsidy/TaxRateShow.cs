using ERP_SHARED.GlobalFunctions;
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
    public partial class TaxRateShow : BaseForm
    {
        public bool Edit;
        public int IdFee;
        public string type;
        public string useTable;
        public TaxRateMain _rateMain;
        Utilities Util = new Utilities();
        TaxRateRepository _repository = new TaxRateRepository();
        
        public TaxRateShow(TaxRateMain rateMain)
        {
            InitializeComponent();        
            _rateMain = rateMain;
        }

        private void TaxRateShow_Load(object sender, EventArgs e)
        {
            this.Text += type;

            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                _repository.useTable = useTable;
                TaxRate tx = _repository.GetTaxRate(IdFee);
                numericUpDown1.Value = tx.LowerLimit;
                numericUpDown2.Value = tx.UpperLimit;
                numericUpDown3.Value = tx.FixedFee;
                numericUpDown4.Value = tx.ExcessPercentage;
            }
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            TaxRate tx = ShowTaxRate();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.useTable = useTable;
                    var createTaxRate = _repository.CreateTaxRate(tx);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _rateMain._useTable = useTable;
                    _rateMain.LoadGrid();
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
            TaxRate tx = ShowTaxRate();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.useTable = useTable;
                    var updateTaxRate = _repository.UpdateTaxRate(tx);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _rateMain._useTable = useTable;
                    _rateMain.LoadGrid();

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
        
        private TaxRate ShowTaxRate()
        {
            return new TaxRate
            {
                Id = Convert.ToInt32(IdFee),
                LowerLimit = Convert.ToDecimal(numericUpDown1.Value),
                UpperLimit = Convert.ToDecimal(numericUpDown2.Value),
                FixedFee = Convert.ToDecimal(numericUpDown3.Value),
                ExcessPercentage = Convert.ToDecimal(numericUpDown4.Value),
            };
        }
    }
}
