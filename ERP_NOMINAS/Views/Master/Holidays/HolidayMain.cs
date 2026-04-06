using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Holidays;
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

namespace ERP_NOMINAS.Views.Master.Holidays
{
    public partial class HolidayMain : BaseForm
    {
        Utilities Util = new Utilities();
        HolidayRepository _repository = new HolidayRepository();
        private int IdHoliday = 0;

        public HolidayMain()
        {
            InitializeComponent();
        }

        private void HolidayMain_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

            LoadGrid();
            
        }

        public void LoadGrid()
        {
            var ListHolidays = _repository.GetHolidays();
            Util.ConfigGrid<Holiday>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Holiday>(ListHolidays);
        }

        private void buttonAdd1_OnBotonAddClick(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            buttonSave1.VisibleBotonSave = true;
        }

        private void buttonFDelete1_OnBotonDeleteClick(object sender, EventArgs e)
        {
            if (IdHoliday <= 0)
            {
                MessageBox.Show("Seleccione una fecha", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteHoliday = _repository.DeleteHoliday(IdHoliday);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void buttonSave1_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Holiday h = ShowHoliday();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createHoliday = _repository.CreateHoliday(h);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    LoadGrid();
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

        private Holiday ShowHoliday()
        {
            return new Holiday
            {
                Id = Convert.ToInt32(IdHoliday),
                Day = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")),
                _Holiday = Convert.ToString(textBox1.Text)
            };
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdHoliday = Convert.ToInt32(row.Cells[0].Value);
            }
        }
    }
}
