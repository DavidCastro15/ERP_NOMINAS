using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.SubsidyFees;
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
    public partial class SubsidyRateShow : BaseForm
    {
        public bool Edit;
        public int IdSubsidy;
        public string type;
        public string useTable;
        public SubsidyRateMain _rateMain;
        Utilities Util = new Utilities();
        SubsidyFeeRepository _repository = new SubsidyFeeRepository();

        public SubsidyRateShow(SubsidyRateMain rateMain)
        {
            InitializeComponent();
            _rateMain = rateMain;
        }

        private void SubsidyRateShow_Load(object sender, EventArgs e)
        {
            this.Text += type;

            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                _repository.useTable = useTable;
                SubsidyFee sf = _repository.GetSubsidyFee(IdSubsidy);
                numericUpDown1.Value = sf.LowerLimit;
                numericUpDown2.Value = sf.UpperLimit;
                numericUpDown3.Value = sf.Subsidy;
        
            }
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            SubsidyFee sf = ShowSubsidyFee();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.useTable = useTable;
                    var createSubsidyFee = _repository.CreateSubsidyFee(sf);
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
            SubsidyFee sf = ShowSubsidyFee();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.useTable = useTable;
                    var updateSubsidyFee = _repository.UpdateSubsidyFee(sf);
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

        private SubsidyFee ShowSubsidyFee()
        {
            return new SubsidyFee
            {
                Id = Convert.ToInt32(IdSubsidy),
                LowerLimit = Convert.ToDecimal(numericUpDown1.Value),
                UpperLimit = Convert.ToDecimal(numericUpDown2.Value),
                Subsidy = Convert.ToDecimal(numericUpDown3.Value)
            };
        }
    }
}
