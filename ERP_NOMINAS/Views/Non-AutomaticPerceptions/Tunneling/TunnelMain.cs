using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Tunneling;
using ERP_NOMINAS.Reports.NonAutomaticPerception.Tunneling;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.Tunneling
{
    public partial class TunnelMain : BaseForm
    {
        Utilities Util = new Utilities();
        TunnelRepository _repository = new TunnelRepository();

        public TunnelMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                if (int.TryParse(text, out int NumberEmployee))
                {
                    dataGridView1.DataSource = new BindingList<Tunnel>(_repository.FilterByValue(NumberEmployee));
                }
                else
                {
                    dataGridView1.DataSource = new BindingList<Tunnel>(_repository.FilterByValue(text));
                }
            };
        }

        private void TunnelMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListTunnel = _repository.GetTunnels();
            Util.ConfigGrid<Tunnel>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Tunnel>(ListTunnel);
        }

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {
            var report = new tunnelListView();
            report.PayWeek = Util.PayWeekNow(Convert.ToInt32(selectPayWeek1.PayWeek), Convert.ToInt32(selectPayWeek1.PayWeekType));
            report.ShowDialog();
        }
    }
}
