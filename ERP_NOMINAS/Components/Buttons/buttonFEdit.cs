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
    public partial class buttonFEdit : UserControl
    {
        public event EventHandler OnBotonFEditClick;

        public buttonFEdit()
        {
            InitializeComponent();
        }

        private void buttonFEdit_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnBotonFEditClick?.Invoke(this, e);
        }
        public bool VisibleBotonEdit
        {
            get => button2.Visible;
            set => button2.Visible = value;
        }
    }
}
