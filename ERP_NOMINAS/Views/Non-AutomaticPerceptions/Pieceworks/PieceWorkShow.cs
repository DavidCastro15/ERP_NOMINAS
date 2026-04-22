using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Pieceworks;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.Pieceworks
{
    public partial class PieceWorkShow : BaseForm
    {
        private PieceWorkMain _loadpieceWork;
        private Utilities Util = new Utilities();
        private PieceworkRepository _repository = new PieceworkRepository();
        public int RefetenceN;
        public bool Edit;
        private BindingSource _bsDetails = new BindingSource();
        private Piecework _res; // Para tener acceso a la lista original

        public PieceWorkShow(PieceWorkMain loadpieceWork)
        {
            InitializeComponent();
            _loadpieceWork = loadpieceWork;
            _res = new Piecework();
            _res.Details = new List<PieceworkDetail>();

            // 2. Vinculamos el BindingSource a la lista recién creada
            _bsDetails.DataSource = _res.Details;
            searchCatalog2.OnItemSelected += searchCatalog2_OnItemSelected;
            searchCatalog2_OnItemSelected(searchCatalog2, EventArgs.Empty);
        }

        private void PieceWorkShow_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _bsDetails;
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                _res = _repository.GetPiecework( RefetenceN);
                if (_res.Details == null) _res.Details = new List<PieceworkDetail>();

                dateTimePicker1.Value = _res.Date;
                searchCatalog1.SelectedValue = _res.Use;
                searchCatalog2.SelectedValue = _res.Material;
                numericUpDown1.Value = Util.GetPayWeek(_res.PayWeek);
                numericUpDown2.Value = Util.GetPayWeekType(_res.PayWeek);
                numericUpDown3.Value = _res.TonRate;
                numericUpDown4.Value = _res.TonCharged;
                textBox1.Text = _res.QuantityCharged.ToString();

                _bsDetails.DataSource = _res.Details;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (searchCatalog3.SelectedValue == null) return;

            PieceworkDetail res = _repository.GetDetailEmployee(Convert.ToInt32(searchCatalog3.SelectedValue));

            if (res != null)
            {
                // 4. Usar el BindingSource para añadir. Esto actualiza el Grid e _res.Details
                _bsDetails.Add(res);

                // Actualizar contador
                int count = _bsDetails.Count;
                textBox1.Text = count.ToString();
                searchCatalog3.Focus();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que sea el botón (ajusta el nombre o índice de tu columna de botón)
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // 1. Obtener el objeto de la fila
                var detalle = (PieceworkDetail)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                // 2. Confirmar (Opcional pero recomendado)

                // 3. Eliminar de la lista interna a través del BindingSource
                _bsDetails.Remove(detalle);
                textBox1.Text = (int.Parse(textBox1.Text) - 1).ToString();
                // Nota: Al usar _bsDetails.Remove(), la lista _res.Details se actualiza sola
                // y el Grid se refresca automáticamente.

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
                    Piecework pw = ShowPieceWork();
                    var res = _repository.CreatePiecework(pw);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadpieceWork.LoadGrid();
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
                    Piecework pw = ShowPieceWork();
                    var res = _repository.UpdatePiecework(pw);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadpieceWork.LoadGrid();

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

        private void searchCatalog2_OnItemSelected(object sender, EventArgs e)
        {
            decimal res = _repository.CheckRateTon(searchCatalog2.SelectedValue.ToString());
            numericUpDown3.Value = res;
        }

        private Piecework ShowPieceWork()
        {
            string _payWeek = $"{dateTimePicker1.Value.Year}{numericUpDown1.Value}{numericUpDown2.Value}";

            return new Piecework
            {
                ReferenceNumber = Convert.ToInt32(RefetenceN),
                Date = Convert.ToDateTime(dateTimePicker1.Value),
                Shift = Convert.ToInt32(searchTurn1.SelectedTurntId),
                Material = Convert.ToString(searchCatalog2.SelectedValue),
                TonRate = Convert.ToDecimal(numericUpDown3.Value),
                TonCharged = Convert.ToDecimal(numericUpDown4.Value),
                QuantityCharged = Convert.ToInt32(textBox1.Text),
                PayWeek = Convert.ToInt32(Util.PayWeekNow((int)numericUpDown1.Value, (int)numericUpDown2.Value)),
                Cycle = Convert.ToString(""),
                Use = Convert.ToInt32(searchCatalog1.SelectedValue),
                PercepctionId = Convert.ToInt32(20),
                PayRollId = Convert.ToInt32(1),
                Details = _res.Details
            };
        }
    }
}
