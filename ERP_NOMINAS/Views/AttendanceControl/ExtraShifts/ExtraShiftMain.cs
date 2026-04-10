using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.ExtraShifts;
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

namespace ERP_NOMINAS.Views.AttendanceControl.ExtraShifts
{
    public partial class ExtraShiftMain : BaseForm
    {
        Utilities Util = new Utilities();
        ExtraShiftRepository _repository = new ExtraShiftRepository();
        //int indexTableSelected = 0;

        public ExtraShiftMain()
        {
            InitializeComponent();
        }

        private void ExtraShiftMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns["Column3"].DefaultCellStyle.Format = "yyyy-MM-dd";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var res = _repository.GetExtraShifts(dateTimePicker1.Value, dateTimePicker2.Value);
            Util.ConfigGrid<ExtraShift>(dataGridView1);
            dataGridView1.DataSource = new BindingList<ExtraShift>(res);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //btnActualizar.Enabled = false;

            // Configuramos el receptor del progreso
            var progress = new Progress<int>(v => {
                progressBar1.Value = v;
            });

            string message = "¿Está seguro de actualizar las asistencias?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                List<ExtraShift> list = new List<ExtraShift>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        ExtraShift shift = new ExtraShift();
                        shift.AttendListDetailId = Convert.ToInt32(row.Cells["Column8"].Value);
                        list.Add(shift);
                    }
                }

                int resultado = await Task.Run(() => _repository.UpdateAttend(list, progress)); ;
                MessageBox.Show("Listas actualizadas con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
    }
}
