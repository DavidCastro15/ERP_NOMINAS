using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ERP_NOMINAS.GlobalFunctions;
using static ERP_NOMINAS.GlobalFunctions.Utilities;

namespace ERP_NOMINAS.Components
{
    [DesignTimeVisible(false)]
    [Obsolete("Component DONT USE obsolete and not optimized, Please use SearchCatalog for now")]
    public partial class SearchConcept : UserControl
    {
        Utilities Util = new Utilities();

        public event EventHandler OnConceptSelected;

        public SearchConcept()
        {
            InitializeComponent();
        }

        private void SearchConcept_Load(object sender, EventArgs e)
        {
            Util.LoadTypeCombo(comboBox1, TypeCatalog.Concept);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Util.LoadTypeComboFilter(comboBox1, TypeCatalog.Concept, textBox1.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los conceptos: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnConceptSelected?.Invoke(this, e);
        }

        public object SelectedConceptId
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
