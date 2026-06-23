using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.OffsetsGratuities;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.OffsetsGratuities
{
    public partial class OffsetGratuityEdit : BaseForm
    {
        public string Table;
        private Utilities Util = new Utilities();
        private OffsetsGratuitiesRepository _repository = new OffsetsGratuitiesRepository();
        private OffsetGratuityMain _loadOffset;
        public int Id;

        public OffsetGratuityEdit(OffsetGratuityMain loadOffset)
        {
            InitializeComponent();
            _loadOffset = loadOffset;
        }

        private void OffsetEdit_Load(object sender, EventArgs e)
        {
            this.Text = Table == "compensaciones" ? "Compensacion" : "Gratificacion";
            kitCrud1.VisibleBotonCrud(false);
            _repository.Table = Table;
            var res = _repository.GetOffsetGratuityEmployeeDetail(Id);

            searchCatalog1.SelectedValue = res.NumberEmployee;
            searchCatalog2.SelectedValue = res.Use;
            numericUpDown1.Value = res.Amount;
            selectCycle1.SelectValue = res.Cycle;
            selectPayWeek1.PayWeek = Util.GetPayWeek(res.PayWeek);
            selectPayWeek1.PayWeekType = Util.GetPayWeekType(res.PayWeek);
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
                    OffSetGratuity of = ShowOffset();
                    _repository.Table = Table;
                    var res = _repository.UpdateOffsetGratuityEmployeeDetail(of);
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

        private OffSetGratuity ShowOffset()
        {
            return new OffSetGratuity
            {
                Id = Convert.ToInt32(Id),
                PayrollId = Convert.ToInt32(1),             
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                Amount = Convert.ToDecimal(numericUpDown1.Value),
                Cycle = Convert.ToString(selectCycle1.SelectValue),
                PayWeek = Convert.ToInt32(Util.PayWeekNow(selectPayWeek1.PayWeek, selectPayWeek1.PayWeekType)),
                Use = Convert.ToInt32(searchCatalog2.SelectedValue),
                Comments = Convert.ToString(richTextBox1.Text),
                IdConcept = Convert.ToInt32(12)
            };
        }
    }
}
