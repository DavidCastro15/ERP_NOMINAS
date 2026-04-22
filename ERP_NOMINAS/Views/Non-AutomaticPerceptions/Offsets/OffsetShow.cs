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
    public partial class OffsetShow : BaseForm
    {
        private OffsetsRepository _repository = new OffsetsRepository();
        private Utilities Util = new Utilities();
        private BindingSource _bsDetails = new BindingSource();
        private Offset _res;
        private OffsetMain _loadOffset;

        public OffsetShow(OffsetMain offsetMain)
        {
            InitializeComponent();
            _loadOffset = offsetMain;
            _res = new Offset();
            _res.Details = new List<OffSetDetail>();
            _bsDetails.DataSource = _res.Details;
        }

        private void OffsetShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            dataGridView1.AutoGenerateColumns = false; 
            dataGridView1.DataSource = _bsDetails;
        }

        private void buttonAdd1_OnBotonAddClick(object sender, EventArgs e)
        {
            OffSetDetail nuevoDetalle = new OffSetDetail
            {
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                Amount = Convert.ToDecimal(numericUpDown3.Value),
                Use = Convert.ToInt32(searchCatalog2.SelectedValue),
                Comments = richTextBox1.Text
            };

            _bsDetails.Add(nuevoDetalle);
            _bsDetails.ResetBindings(false);
            searchCatalog1.Focus();
            numericUpDown3.Value = 0;
            richTextBox1.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var detail = (OffSetDetail)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                _bsDetails.Remove(detail);
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
                    Offset of = ShowOffset();
                    var res = _repository.CreateOffset(of);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            int payWeek = Util.PayWeekNow(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
            var cicle = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            return new Offset
            {
                Id = Convert.ToInt32(0),
                PayrollId = Convert.ToInt32(1),
                Cycle = Convert.ToString(cicle.Text),
                PayWeek = Convert.ToInt32(payWeek),
                Details = _res.Details,
                IdConcept = Convert.ToInt32(12)               
            };
        }
    }
}
