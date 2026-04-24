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

namespace ERP_NOMINAS.Components.Search
{
    public partial class SearchCatalog : UserControl
    {
        Utilities Util = new Utilities();
        public event EventHandler OnItemSelected;

        private TypeCatalog _typeC;

        [Category("Config Catalog")]
        public TypeCatalog _TypeCatalog
        {
            get => _typeC;
            set
            {
                _typeC = value;
                LoadData(); // Esto refresca el combo cuando cambias el tipo en el diseñador o código
            }
        }

        [Category("Config Catalog")]
        public string TittleChange
        {
            get => label1.Text;
            set
            {
                label1.Text = value;
                // Ajustamos la posición del TextBox basándonos en el ancho del Label
                // Dejamos un margen de 5 o 10 píxeles
                textBox1.Left = label1.Right + 10;

                // Opcional: Ajustar el ancho del TextBox para que no se salga del UserControl
                textBox1.Width = this.Width - textBox1.Left - 5;
            }
        }

        public SearchCatalog()
        {
            InitializeComponent();
            textBox1.TextChanged += (s, e) => FilterBy();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void LoadData()
        {
            // IMPORTANTE: Limpiar el DataSource antes de recargar evita errores de "key null"
            if (this.DesignMode) return;

            comboBox1.DataSource = null;
            Util.LoadTypeCombo(comboBox1, _typeC);
            //Util.LoadTypeComboFilter(comboBox1, TypeCatalog.TrainedCategories, textBox1.Text, empleadoId);
        }

        private void FilterBy()
        {
            try
            {
                Util.LoadTypeComboFilter(comboBox1, _typeC, textBox1.Text);

                // 🔑 Dispara el evento si hay algo seleccionado
                if (comboBox1.SelectedIndex != -1)
                {
                    OnItemSelected?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public object SelectedValue
        {
            get => comboBox1.SelectedValue;
            set
            {

                // PROTECCIÓN: Si el valor a asignar es nulo o el combo no tiene datos, no procesar
                if (value == null || comboBox1.DataSource == null) return;

                if (comboBox1.BindingContext == null) comboBox1.BindingContext = new BindingContext();

                try
                {
                    comboBox1.SelectedValue = value;
                    OnItemSelected?.Invoke(this, EventArgs.Empty);
                }
                catch (ArgumentNullException)
                {

                }
            }
        }

        public void FilterByEmployee(int empleadoId)
        {
            try
            {
                // Aquí llamas a tu utilitario con el parámetro extra
                Util.LoadTypeComboFilter(comboBox1, TypeCatalog.TrainedCategories, textBox1.Text, empleadoId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // USA SOLO ESTE MÉTODO PARA EL EVENTO
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (comboBox1.SelectedIndex != -1 && comboBox1.Focused)
                {
                    OnItemSelected?.Invoke(this, e);
                }
            }
            catch (Exception)
            {

            }


        }
      
        private void SearchCatalog_Load(object sender, EventArgs e)
        {
        }
    }
}
