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
    public partial class buttonAdd : UserControl
    {
        public event EventHandler OnBotonAddClick;

        public buttonAdd()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OnBotonAddClick?.Invoke(this, e);
        }

    }
}
