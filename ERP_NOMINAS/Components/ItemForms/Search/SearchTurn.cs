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
    public partial class SearchTurn : UserControl
    {
        Utilities Util = new Utilities();

        public event EventHandler OnTurnSelected;

        public SearchTurn()
        {
            InitializeComponent();
        }

        private void SearchTurn_Load(object sender, EventArgs e)
        {
            Util.LoadTypeCombo(comboBox1, TypeCatalog.Turn);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTurnSelected?.Invoke(this, e);
        }

        public object SelectedTurntId
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

        public string TittleLabelTurn
        {
            get => label1.Text;
            set
            {
                label1.Text = value;
                // Ajustamos la posición del TextBox basándonos en el ancho del Label
                // Dejamos un margen de 5 o 10 píxeles
                label1.Left = label1.Right + 10;

                // Opcional: Ajustar el ancho del TextBox para que no se salga del UserControl
                label1.Width = this.Width - label1.Left - 5;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
