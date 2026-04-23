using ERP_NOMINAS.GlobalFunctions;
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
    public partial class OffsetGratuityShow : BaseForm
    {
        public string Table;
        private OffsetsGratuitiesRepository _repository = new OffsetsGratuitiesRepository();
        private Utilities Util = new Utilities();
        private BindingSource _bsDetails = new BindingSource();
        private OffSetGratuity _res;
        private OffsetGratuityMain _loadOffset;

        public OffsetGratuityShow(OffsetGratuityMain offsetMain)
        {
            InitializeComponent();
            _loadOffset = offsetMain;
            _res = new OffSetGratuity();
            _res.Details = new List<OffSetGratuityDetail>();
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
            OffSetGratuityDetail nuevoDetalle = new OffSetGratuityDetail
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
                var detail = (OffSetGratuityDetail)dataGridView1.Rows[e.RowIndex].DataBoundItem;

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
                    OffSetGratuity of = ShowOffset();
                    _repository.Table = Table;
                    var res = _repository.CreateOffsetGratuity(of);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadOffset.Table = Table;
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
            int payWeek = Util.PayWeekNow(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
            var cicle = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            return new OffSetGratuity
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
