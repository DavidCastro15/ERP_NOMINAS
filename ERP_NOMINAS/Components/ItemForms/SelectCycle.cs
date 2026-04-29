using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Components.ItemForms
{
    public partial class SelectCycle : UserControl
    {
        public SelectCycle()
        {
            InitializeComponent();
        }

        public string SelectValue
        {
            get { return radioButton1.Checked ? "Zafra" : "Reparación"; }
            set
            {
                if (value == "Zafra") radioButton1.Checked = true;
                else radioButton2.Checked = true;
            }
        }

    }

}
