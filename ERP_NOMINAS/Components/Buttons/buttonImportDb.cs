using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Components.Buttons
{
    public partial class buttonImportDb : UserControl
    {
        public event EventHandler OnBotonImportDbClick;

        public buttonImportDb()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnBotonImportDbClick?.Invoke(this, e);
        }
    }
}
