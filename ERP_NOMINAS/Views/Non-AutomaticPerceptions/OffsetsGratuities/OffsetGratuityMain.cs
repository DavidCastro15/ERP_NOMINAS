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
    public partial class OffsetGratuityMain : BaseForm
    {
        public string Table;
        private Utilities Util = new Utilities();
        private OffsetsGratuitiesRepository _repository = new OffsetsGratuitiesRepository();
        private int Id;

        public OffsetGratuityMain()
        {          
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {         
                if (int.TryParse(text, out int periodo))
                {
                    _repository.Table = Table;
                    dataGridView1.DataSource = new BindingList<OffSetGratuity>(_repository.FilterByValue(periodo));
                }
                else
                {
                    _repository.Table = Table;
                    dataGridView1.DataSource = new BindingList<OffSetGratuity>(_repository.FilterByValue(text));
                }
            };
        }

        private void OffsetMain_Load(object sender, EventArgs e)
        {
            this.Text = Table == "compensaciones" ? "Compensaciones" : "Gratificaciones";
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            _repository.Table = Table;
            var ListOffset = _repository.GetOffsetsGratuity();
            Util.ConfigGrid<OffSetGratuity>(dataGridView1);
            dataGridView1.DataSource = new BindingList<OffSetGratuity>(ListOffset);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            _repository.Table = Table;
            var offSetShow = new OffsetGratuityShow(this);
            offSetShow.Table = Table;
            offSetShow.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (Id <= 0)
            {
                MessageBox.Show("Seleccione una Compensacion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _repository.Table = Table;
            var offSetEdit = new OffsetGratuityEdit(this);
            offSetEdit.Table = Table;
            offSetEdit.Id = Id;
            offSetEdit.ShowDialog();

        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (Id <= 0)
            {
                MessageBox.Show("Seleccione una compensacion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                _repository.Table = Table;
                var deleteCategorory = _repository.DeleteOffsetGratuity(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {

        }
    }
}
