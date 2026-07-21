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
    public partial class DiscountGlobalShow : BaseForm
    {
        private DiscountGlobalMain _loadMain;
        private Utilities Util = new Utilities();
        private DiscountGlobalRepository _repository = new DiscountGlobalRepository();
        public int Id;
        public bool Edit;

        public DiscountGlobalShow(DiscountGlobalMain loadMain)
        {
            InitializeComponent();
            _loadMain = loadMain;
        }

        private void DiscountGlobalShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                var res = _repository.GetDGlobal(Id);
                selectCycle1.SelectValue = res.Cycle;
                selectPayWeek1.PayWeek = Util.GetPayWeek(res.PayWeek);
                selectPayWeek1.PayWeekType = Util.GetPayWeekType(res.PayWeek);
                searchCatalog1.SelectedValue = res.IdConcept;
                checkBox1.Checked = res.Occasional == 1 ? true : false;
                checkBox2.Checked = res.Temporary == 1 ? true : false;
                checkBox3.Checked = res.Permanent == 1 ? true : false;
                numericUpDown1.Value = res.Amount;
                richTextBox1.Text = res.Description;
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
                    DGlobal dg = ShowDGlobal();
                    var res = _repository.CreateDGlobal(dg);
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
                    DGlobal dg = ShowDGlobal();
                    var res = _repository.UpdateDGlobal(dg);
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
        
        private DGlobal ShowDGlobal()
        {
            return new DGlobal
            {
                Id = Convert.ToInt32(Id),
                Cycle = Convert.ToString(selectCycle1.SelectValue),
                PayWeek = Convert.ToInt32(Util.PayWeekNow(selectPayWeek1.PayWeek, selectPayWeek1.PayWeekType)),
                IdConcept = Convert.ToInt32(searchCatalog1.SelectedValue),
                Occasional = Convert.ToByte(checkBox1.Checked == true? 1:0),
                Temporary = Convert.ToByte(checkBox2.Checked == true? 1:0),
                Permanent = Convert.ToByte(checkBox3.Checked == true? 1:0),
                Amount = Convert.ToDecimal(numericUpDown1.Value),
                Description = Convert.ToString(richTextBox1.Text)
            };
        }
    }
}
