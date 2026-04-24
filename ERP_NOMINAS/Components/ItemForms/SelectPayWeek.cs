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
    public partial class SelectPayWeek : UserControl
    {
        public SelectPayWeek()
        {
            InitializeComponent();
        }

        public int PayWeek
        {
            get { return Convert.ToInt32(numericUpDown1.Value); }
            set { numericUpDown1.Value = value; }
        }

        public int PayWeekType
        {
            get { return Convert.ToInt32(numericUpDown2.Value); }
            set { numericUpDown2.Value = value; }
        }
    }
}
