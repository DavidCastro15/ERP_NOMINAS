using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Attendance.ListAttendance;
using ERP_NOMINAS.Reports.Attendance.AddLists;
using ERP_NOMINAS.Reports.AttendanceLists;
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

namespace ERP_NOMINAS.Views.AttendanceControl.Attendance.ListAttend
{
    public partial class ListAttendMain : BaseForm
    {

        private int ControlNumber = 0;
        private Utilities Util = new Utilities();
        private ListAttendRepository _repository = new ListAttendRepository();
        private AttendMain _loadAttendMain;
        public string ListType;

        public ListAttendMain() : this(null)
        {

        }

        public ListAttendMain(AttendMain loadAttendMain)
        {
            InitializeComponent();
            _loadAttendMain = loadAttendMain;
            kitForm1.VisibleButtonAdd(false);

        }

        private void ListAttendMain_Load(object sender, EventArgs e)
        {
            if (ListType is null)
            {
             
            }
            else
            {
                kitForm1.Visible = false;
                buttonPrint1.Visible = false;
                groupBox1.Visible = false;
                groupBox2.Visible = false;
            }

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns["Column3"].DefaultCellStyle.Format = "yyyy-MM-dd";
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListAttend = _repository.GetListAttends();
            Util.ConfigGrid<ListAttendC>(dataGridView1);
            dataGridView1.DataSource = new BindingList<ListAttendC>(ListAttend);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ControlNumber = Convert.ToInt32(row.Cells[0].Value);             
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (ListType)
            {
                case "GetList":
                    _loadAttendMain.NumberListExists = ControlNumber.ToString();
                    _loadAttendMain.NumberListUpdate="";
                    Close();
                    break;

                case "EditList":
                    _loadAttendMain.NumberListUpdate = ControlNumber.ToString();
                    _loadAttendMain.NumberListExists="";

                    Close();
                    break;

                default:
                    parametersEdit();
                    break;
            }
        }

        private void parametersEdit()
        {

            if (ControlNumber <= 0)
            {
                MessageBox.Show("Seleccione una lista", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var listAttend = new ListAttendShow(this);
            listAttend.ControlNumber = ControlNumber;
            listAttend.Edit = true;
            listAttend.ShowDialog();

        }
       
        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
        }

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Digite una semana de pago", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var printReport = new addListView();
            printReport.PayWeek = GetPayWeek((int)numericUpDown1.Value, (int)numericUpDown1.Value);
            printReport.ShowDialog();
        }

        private void buttonPrint2_OnBotonPrintClick(object sender, EventArgs e)
        {
            if (ControlNumber <= 0)
            {
                MessageBox.Show("Seleccione una lista", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }       
            var printReport = new attendanceListView();
            printReport.ControlNumber = ControlNumber;
            printReport.ShowDialog();
        }

        private int GetPayWeek(int num1,int num2)
        {
            int year = DateTime.Now.Year;
            int week = (int)num1;
            int extra = (int)num2;

           return (year * 1000) + (week * 10) + extra;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //121283
            if (ControlNumber <= 0)
            {
                MessageBox.Show("Seleccione una lista", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToInt32(textBox1.Text) != 121283)
            {
                MessageBox.Show("Contraseña Incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteList = _repository.DeleteListAttend(ControlNumber);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
                groupBox3.Visible = false;
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
