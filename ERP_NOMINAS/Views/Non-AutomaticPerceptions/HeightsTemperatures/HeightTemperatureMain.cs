using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.HeightsTemperatures;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.HeightsTemperatures
{
    public partial class HeightTemperatureMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private HeightTemperatureRepository _repository = new HeightTemperatureRepository();
        private int ReferenceN;

        public HeightTemperatureMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                // 1. Usar string.IsNullOrWhiteSpace es más seguro que ""
                if (string.IsNullOrWhiteSpace(text))
                {
                    LoadGrid();
                    return;
                }

                // 2. TryParse evita que la app se cierre si escriben letras
                if (int.TryParse(text, out int id))
                {
                    var res = _repository.FilterByReferenceNumber(id);

                    // 3. Evitamos el NullReferenceException si el repo devuelve null
                    // y refrescamos el DataSource de forma eficiente
                    dataGridView1.DataSource = new BindingList<HeightTemperature>(res ?? new List<HeightTemperature>());
                }
                else
                {
                    // Opcional: Si no es un número, podrías limpiar el grid o no hacer nada
                    dataGridView1.DataSource = new BindingList<HeightTemperature>();
                }
            };
        }

        private void HeightTemperatureMain_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["Column5"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var res = _repository.GetHeightTemperatureHeads();
            Util.ConfigGrid<HeightTemperature>(dataGridView1);
            dataGridView1.DataSource = new BindingList<HeightTemperature>(res);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ReferenceN = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var heightTemp = new HeightTemperatureShow(this);
            heightTemp.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (ReferenceN <= 0)
            {
                MessageBox.Show("Seleccione un folio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCategorory = _repository.DeleteHeightTemperature(ReferenceN);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void parametersEdit()
        {

            if (ReferenceN <= 0)
            {
                MessageBox.Show("Seleccione un Folio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var heightTemp = new HeightTemperatureShow(this);
            heightTemp.RefetenceN = ReferenceN;
            heightTemp.Edit = true;
            heightTemp.ShowDialog();

        }
    }
}
