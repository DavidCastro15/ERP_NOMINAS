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
    public partial class buttonSave : UserControl
    {
        public event EventHandler OnBotonSaveClick;
        public buttonSave()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OnBotonSaveClick?.Invoke(this, e);
        }
        public bool VisibleBotonSave
        {
            get => button3.Visible;
            set => button3.Visible = value;
           
        }

    }
}
