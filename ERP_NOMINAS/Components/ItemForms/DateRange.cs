using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Components.Search
{
    public partial class DateRange : UserControl
    {
        public DateRange()
        {
            InitializeComponent();
        }

        public DateTime StartDate
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        }

        public DateTime EndDate
        {
            get { return dateTimePicker2.Value; }
            set { dateTimePicker2.Value = value; }
        }
    }
}
