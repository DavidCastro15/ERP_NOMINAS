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
    public partial class OffsetMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private OffsetsRepository _repository = new OffsetsRepository();
        private int Id;

        public OffsetMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                // 1. Intentamos convertir el texto a entero
                if (int.TryParse(text, out int periodo))
                {
                    // Si es un número, usamos la sobrecarga de INT (periodo)
                    dataGridView1.DataSource = new BindingList<Offset>(_repository.FilterByValue(periodo));
                }
                else
                {
                    // Si no es número, usamos la sobrecarga de STRING (nombre)
                    dataGridView1.DataSource = new BindingList<Offset>(_repository.FilterByValue(text));
                }
            };
        }

        private void OffsetMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListOffset = _repository.GetOffsets();
            Util.ConfigGrid<Offset>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Offset>(ListOffset);
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
            var offSetShow = new OffsetShow(this);
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
            var offSetEdit = new OffsetEdit(this);
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

                var deleteCategorory = _repository.DeleteOffset(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
