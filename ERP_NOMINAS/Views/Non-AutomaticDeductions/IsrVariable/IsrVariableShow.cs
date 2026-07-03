using ERP_NOMINAS.Models.Deductions.IsrVariable;
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

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.IsrVariable
{
    public partial class IsrVariableShow : BaseForm
    {
        private IsrVariableMain _loadMain;
        private Utilities Util = new Utilities();
        private IsrVariableRepository _repository = new IsrVariableRepository();
        public int Id;
        public bool Edit;

        public IsrVariableShow(IsrVariableMain loadMain)
        {
            InitializeComponent();
            _loadMain = loadMain;
        }

        private void IsrVariableShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                searchCatalog1.Enabled = false;
                kitCrud1.VisibleBotonCrud(false);
                IsrV res = _repository.GetIsr(Id);
                searchCatalog1.SelectedValue = res.NumberEmployee;
                numericUpDown1.Value = res.Amount;
                numericUpDown2.Value = res.Subsidy;
                selectPayWeek1.PayWeek = Util.GetPayWeek(res.PayWeek);
                selectPayWeek1.PayWeekType = Util.GetPayWeekType(res.PayWeek);

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
                    IsrV cs = ShowIsrV();
                    var res = _repository.CreateIsrV(cs);
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
                    IsrV cs = ShowIsrV();
                    var res = _repository.UpdateIsrV(cs);
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

        private IsrV ShowIsrV()
        {
            return new IsrV
            {
                Id = Convert.ToInt32(Id),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                Amount = Convert.ToDecimal(numericUpDown1.Value),
                Subsidy = Convert.ToDecimal(numericUpDown2.Value),
                PayWeek = Convert.ToInt32(Util.PayWeekNow(selectPayWeek1.PayWeek, selectPayWeek1.PayWeekType)),
            };
        }
    }
}
