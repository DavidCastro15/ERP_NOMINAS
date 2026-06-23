using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ERP_SHARED.GlobalFunctions;

using ERP_SHARED.GlobalFunctions.Enums;

namespace ERP_NOMINAS.Components.Search
{
    public partial class SearchPayWeek : UserControl
    {
        Utilities Util = new Utilities();
        public event EventHandler OnPayWeekSelected;

        public SearchPayWeek()
        {
            InitializeComponent();
        }

        private void SearchPayWeek_Load(object sender, EventArgs e)
        {
            Util.LoadTypeCombo(comboBox1, TypeCatalog.PayWeek);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPayWeekSelected?.Invoke(this, e);
        }

        public object SelectedValue
        {
            get => comboBox1.SelectedValue;
            set
            {
                // Forzar la creación del contexto de unión si es necesario
                if (comboBox1.BindingContext == null)
                    comboBox1.BindingContext = new BindingContext();

                comboBox1.SelectedValue = value;
            }
        }
    }
}
