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

namespace ERP_NOMINAS.Components
{
    [DesignTimeVisible(false)]
    [Obsolete("Component DONT USE obsolete and not optimized, Please use SearchCatalog for now")]
    public partial class SearchUse : UserControl
    {
        Utilities Util = new Utilities();

        public event EventHandler OnUseSelected;

        public SearchUse()
        {
            InitializeComponent();
        }

        private void SearchUse_Load(object sender, EventArgs e)
        {
            Util.LoadTypeCombo(comboBox1, TypeCatalog.Use);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          OnUseSelected?.Invoke(this, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Util.LoadTypeComboFilter(comboBox1, TypeCatalog.Use, textBox1.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los usos: " + ex.Message);
            }
        }

        public object SelectedUse
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
