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
    public partial class buttonClose : UserControl
    {
        public buttonClose()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Busca el formulario que contiene este control
            Form parentForm = this.FindForm();

            if (parentForm != null)
            {
                parentForm.Close(); // Cierra el formulario principal
            }
        }
    }
}
