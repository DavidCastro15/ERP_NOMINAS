using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Offsets;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.Offsets
{
    public partial class OffsetEdit : BaseForm
    {
        private Utilities Util = new Utilities();
        private OffsetsRepository _repository = new OffsetsRepository();
        private OffsetMain _loadOffset;
        public int Id;

        public OffsetEdit(OffsetMain loadOffset)
        {
            InitializeComponent();
            _loadOffset = loadOffset;
        }

        private void OffsetEdit_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(false);

            var res = _repository.GetOffsetEmployeeDetail(Id);

            searchCatalog1.SelectedValue = res.NumberEmployee;
            searchCatalog2.SelectedValue = res.Use;
            numericUpDown1.Value = res.Amount;
            numericUpDown2.Value = Util.GetPayWeek(res.PayWeek);
            numericUpDown3.Value = Util.GetPayWeekType(res.PayWeek);
            radioButton1.Checked = string.Equals(res.Cycle, "Zafra", StringComparison.OrdinalIgnoreCase);
            radioButton2.Checked = string.Equals(res.Cycle, "Reparación", StringComparison.OrdinalIgnoreCase);
            richTextBox1.Text = res.Comments;
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
                    Offset of = ShowOffset();
                    var res = _repository.UpdateOffsetEmployeeDetail(of);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadOffset.LoadGrid();
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

        private Offset ShowOffset()
        {
            int payWeek = Util.PayWeekNow(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value));
            var cicle = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            return new Offset
            {
                Id = Convert.ToInt32(Id),
                PayrollId = Convert.ToInt32(1),             
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                Amount = Convert.ToDecimal(numericUpDown1.Value),
                Cycle = Convert.ToString(cicle.Text),
                PayWeek = Convert.ToInt32(payWeek),
                Use = Convert.ToInt32(searchCatalog2.SelectedValue),
                Comments = Convert.ToString(richTextBox1.Text),
                IdConcept = Convert.ToInt32(12)
            };
        }
    }
}
