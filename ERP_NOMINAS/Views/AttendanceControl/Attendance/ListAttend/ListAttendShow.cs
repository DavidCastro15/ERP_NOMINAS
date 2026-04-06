using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Attendance.ListAttendance;
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
    public partial class ListAttendShow : BaseForm
    {
        private Utilities Util = new Utilities();
        private ListAttendRepository _repository = new ListAttendRepository();
        private ListAttendMain _loadAttend;
        public bool Edit;
        public int ControlNumber;

        public ListAttendShow(ListAttendMain loadAttend)
        {
            InitializeComponent();
            _loadAttend = loadAttend;
        }

        private void ListAttendShow_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListAttend = _repository.GetDetailsAttend(ControlNumber);
            Util.ConfigGrid<ListAttendDetail>(dataGridView1);
            dataGridView1.DataSource = new BindingList<ListAttendDetail>(ListAttend);
        }
    }
}
