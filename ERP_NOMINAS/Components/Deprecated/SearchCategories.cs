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
    [Obsolete("Component DONT USE obsolete and not optimized, Please use SearchCatalog for now")]
    public partial class SearchCategories : UserControl
    {
        Utilities Util = new Utilities();

        public event EventHandler OnCategorySelected;

        public SearchCategories()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Util.LoadTypeComboFilter(comboBox1, TypeCatalog.Category, textBox1.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las categorias: " + ex.Message);
            }
        }

        public object SelectedCategoryId
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

        public string TittleLabelCategory
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        private void SearchCategories_Load(object sender, EventArgs e)
        {
            Util.LoadTypeCombo(comboBox1, TypeCatalog.Category);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCategorySelected?.Invoke(this, e);
        }
    }
}
