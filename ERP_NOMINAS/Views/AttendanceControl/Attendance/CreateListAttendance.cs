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

namespace ERP_NOMINAS.Views.AttendanceControl.Attendance
{
    public partial class CreateListAttendance : BaseForm
    {
        private AttendanceRepository _repository = new AttendanceRepository();

        public CreateListAttendance()
        {
            InitializeComponent();
        }

        private void CreateListAttendance_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var Status = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var Period = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            string _payWeek = $"{dateTimePicker1.Value.Year}{numericUpDown1.Value}{numericUpDown2.Value}";
            int PayWeek = Convert.ToInt32(_payWeek);
            int Shift = Convert.ToInt32(searchTurn1.SelectedTurntId);

            string message = "¿Está seguro de crear la lista?";
            string title = "Confirmar registro de la lista";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.CreateAttendanceList(Status.Text,Period.Text, PayWeek,dateTimePicker1.Value,Shift);
                    MessageBox.Show("Lista creada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
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
    }
}
