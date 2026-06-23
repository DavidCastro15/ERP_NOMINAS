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

namespace ERP_NOMINAS.Components.Search
{
    public partial class FilterByT : UserControl
    {
        Utilities Util = new Utilities();
        public Action<string> FilterBy { get; set; }
        private System.Windows.Forms.Timer _timerDebounce;

        public FilterByT()
        {
            InitializeComponent();

            _timerDebounce = new System.Windows.Forms.Timer();
            _timerDebounce.Interval = 400;
            _timerDebounce.Tick += TimerDebounce_Tick;

            textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) //Validación de texto vacío
            {
                _timerDebounce.Stop();
                FilterBy?.Invoke(""); // Carga inmediata de todo
                return;
            }
            _timerDebounce.Stop();
            _timerDebounce.Start();
        }

        private void TimerDebounce_Tick(object sender, EventArgs e)
        {
            // El tiempo se cumplió (el usuario dejó de escribir)
            _timerDebounce.Stop();

            // Invocamos el método de filtrado que está en tu Formulario/Repositorio
            FilterBy?.Invoke(textBox1.Text.Trim());
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
