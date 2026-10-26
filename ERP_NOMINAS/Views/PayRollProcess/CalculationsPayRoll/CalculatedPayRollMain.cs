using ERP_NOMINAS.Models.PayRollProcess.CalculatedPayRolls;
using ERP_NOMINAS.Repositorys.PayRollProcess;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.PayRollProcess.CalculationsPayRoll
{
    public partial class CalculatedPayRollMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private CalculatedPRollRepository _repository = new CalculatedPRollRepository();
        private int Id;
        private int PayWeek;

        public CalculatedPayRollMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int PayWeek))
                {
                    dataGridView1.DataSource = new BindingList<CalculatedPRolls>(_repository.FilterByPayWeek(PayWeek));
                }              
            };
        }

        private void CalculatedPayRollMain_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["Column5"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns["Column6"].DefaultCellStyle.Format = "yyyy-MM-dd";
            LoadGrid();
        }

        public void LoadGrid()
        {
            var list = _repository.GetCalculatedPRolls();
            Util.ConfigGrid<CalculatedPRolls>(dataGridView1);
            dataGridView1.DataSource = new BindingList<CalculatedPRolls>(list);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
                PayWeek = Convert.ToInt32(row.Cells[2].Value);
            }
        }
    }
}
