using ERP_NOMINAS.Models.Deductions.VariousDiscounts;
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
    public partial class DiscountShow : BaseForm
    {
        private DiscountMain _loadMain;
        private DiscountRepository _repository = new DiscountRepository();
        private Utilities Util = new Utilities();
        public bool Edit;
        public int Id;

        public DiscountShow(DiscountMain loadMain)
        {
            InitializeComponent();
            _loadMain = loadMain;
        }

        private void DiscountShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                searchCatalog1.Enabled = false;
                searchCatalog2.Enabled = false;
                kitCrud1.VisibleBotonCrud(false);
                var res = _repository.GetDiscount(Id);
                searchCatalog1.SelectedValue = res.IdConcept;
                searchCatalog2.SelectedValue = res.NumberEmployee;
                numericUpDown1.Value = res.InitialAmount;
                numericUpDown2.Value = res.NumberPayments;
                numericUpDown3.Value = res._Discount;
                numericUpDown4.Value = res.OutstandingBalance;
                numericUpDown5.Value = res.AccumulatedDiscounts;
                richTextBox1.Text = res.Comments;
            }
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    Discount ds = ShowDiscount();
                    var res = _repository.CreateDiscount(ds);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadMain.LoadGrid();
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

            try
            {
                if (ress == DialogResult.Yes)
                {
                    Discount ds = ShowDiscount();
                    var res = _repository.UpdateDiscount(ds);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadMain.LoadGrid();

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

        private Discount ShowDiscount()
        {
            return new Discount
            {
                Id = Convert.ToInt32(Id),
                IdConcept = Convert.ToInt32(searchCatalog1.SelectedValue),
                NumberEmployee = Convert.ToInt32(searchCatalog2.SelectedValue),
                InitialAmount = Convert.ToDecimal(numericUpDown1.Value),
                NumberPayments = Convert.ToInt32(numericUpDown2.Value),
                _Discount = Convert.ToInt32(numericUpDown3.Value),
                OutstandingBalance = Convert.ToInt32(numericUpDown4.Value),
                AccumulatedDiscounts = Convert.ToInt32(numericUpDown5.Value),
                Comments = Convert.ToString(richTextBox1.Text)
            };
        }
    }
}
