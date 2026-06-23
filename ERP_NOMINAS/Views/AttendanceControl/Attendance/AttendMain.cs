using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Attendance;
using ERP_NOMINAS.Reports.AttendanceControl;
using ERP_NOMINAS.Repositorys;
using ERP_NOMINAS.Views.AttendanceControl.Attendance.ListAttend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.AttendanceControl.Attendance
{
    public partial class AttendMain : BaseForm
    {
        Utilities Util = new Utilities();
        AttendanceRepository _repository = new AttendanceRepository();
        private int Id = 0;
        private int NEmployee = 0;

        public AttendMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<Attend>(_repository.FilterByValue(NumberEmployee));
                }
                else
                {
                    dataGridView1.DataSource = new BindingList<Attend>(_repository.FilterByValue(text));
                }
            };
        }

        private void AttendMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns["Column10"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns["Column11"].DefaultCellStyle.Format = "HH:mm:ss";
            dataGridView1.Columns["Column12"].DefaultCellStyle.Format = "HH:mm:ss";
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListAttend = _repository.GetAttends();
            Util.ConfigGrid<Attend>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Attend>(ListAttend);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
                NEmployee = Convert.ToInt32(row.Cells[1].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        public void parametersEdit(int numberEmployee = 0)
        {
            // Si viene en 0, usamos el seleccionado, si no, el que recibimos
            int currentEmp = (numberEmployee == 0) ? NEmployee : numberEmployee;

            while (currentEmp > 0)
            {
                using (var attenShow = new AttendShow(this))
                {
                    attenShow.NEmployee = currentEmp;
                    attenShow.Edit = true;

                    // ShowDialog detiene la ejecución aquí hasta que la ventana se cierre
                    if (attenShow.ShowDialog() == DialogResult.OK)
                    {
                        // Al cerrarse con OK, refrescamos el grid
                        LoadGrid();

                        // Buscamos el siguiente ID
                        currentEmp = _repository.ChechNextNumberEmployee(currentEmp);

                        // Si el repositorio devuelve 0 o el mismo ID, rompemos el ciclo
                        if (currentEmp <= 0) break;
                    }
                    else
                    {
                        // Si el usuario da "Cancelar" o cierra la X, salimos del bucle
                        break;
                    }
                }
            }

            //numberEmployee =numberEmployee == 0 ? NEmployee : numberEmployee;

            //if (NEmployee <= 0)
            //{
            //    MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //var attenShow = new AttendShow(this);
            //attenShow.NEmployee = numberEmployee;
            //attenShow.Edit = true;
            //attenShow.ShowDialog();

        }

        private void buttonImportDb1_OnBotonImportDbClick(object sender, EventArgs e)
        {
            var res = _repository.GetDataFilterAttend(dateTimePicker1.Value, dateTimePicker2.Value.TimeOfDay, dateTimePicker3.Value.TimeOfDay);
            Util.ConfigGrid<Attend>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Attend>(res);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea cambiar los turnos?";
            string title = "Confirmar Cambio de Turno";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            try
            {
                if (ress == DialogResult.Yes)
                {
                    var res = _repository.ChangeTurn(Convert.ToInt32(searchTurn1.SelectedTurntId));             
                    MessageBox.Show("Turnos cambiados con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var att = new AttendShow(this);
            att.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (NEmployee <= 0)
            {
                MessageBox.Show("Seleccione un Empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCategorory = _repository.DeleteAttend(NEmployee);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar agregar el comite sindical?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.AddUnionCommittee();
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button2_Click(object sender, EventArgs e)
        {
            var attenList = new ListAttendMain(this);
            attenList.ListType = "GetList";
            attenList.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var attenList = new ListAttendMain(this);
            attenList.ListType = "EditList";
            attenList.ShowDialog();
        }

        public string NumberListExists
        {
            get => textBox1.Text;
            set
            {
                textBox1.Text = value;       
            }
        }

        public string NumberListUpdate
        {
            get => textBox2.Text;
            set
            {
                textBox2.Text = value;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea usar esta lista?";
            string title = "Confirmar uso de lista";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.GetListAttendByDetails(Convert.ToInt32(textBox1.Text));
                    MessageBox.Show("Lista importada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button5_Click(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea actualizar esta lista?";
            string title = "Confirmar cambios de la lista";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _repository.UpdateListAttendByDetails(Convert.ToInt32(textBox2.Text));
                    MessageBox.Show("Lista actualizada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button6_Click(object sender, EventArgs e)
        {
            var createList = new CreateListAttendance();
            createList.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var report = new verificationDataView();
            report.ShowDialog();
        }


    }
}
