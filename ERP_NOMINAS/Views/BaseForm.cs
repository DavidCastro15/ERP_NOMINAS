using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views
{
    public partial class BaseForm : Form
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_CAPTION_COLOR = 35;
        private const int DWMWA_BORDER_COLOR = 34;
        private Color ColorSelect = Color.LightSkyBlue;
        private Color HeaderColor = Color.LightSkyBlue;

        public BaseForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();     
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            
            this.Paint += GlobalForm_Paint;
            this.HeaderColor = Color.LightGreen;
            ChangeColorHead(ColorSelect);
            SetDefaultBorderColor(ColorSelect);
        }

        private void ChangeColorHead(Color color)
        {
            // El color debe estar en formato BGR (Blue-Green-Red)
            int colorBGR = ColorTranslator.ToWin32(color);

            // Aplicar el atributo al manejador (Handle) de este formulario
            DwmSetWindowAttribute(this.Handle, DWMWA_CAPTION_COLOR, ref colorBGR, sizeof(int));
        }

        private void SetDefaultBorderColor(Color color)
        {
            // Convertir el color a formato BGR que entiende Windows
            int colorBGR = ColorTranslator.ToWin32(color);

            // Aplicar a los 4 bordes
            DwmSetWindowAttribute(this.Handle, DWMWA_BORDER_COLOR, ref colorBGR, sizeof(int));
        }

        private void GlobalForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // Usamos TextRenderingHint para que los iconos de fuente se vean nítidos
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            string[] officeIcons = { "\uE8EF", "\uE8C7", "\uE9D2", "\uE8D7" };
            Font iconFont = new Font("Segoe MDL2 Assets", 18);

            // CAMBIO: Sube el 40 a 150 para probar si aparecen
            Color iconColor = Color.FromArgb(150, Color.LightSkyBlue);

            using (Brush brush = new SolidBrush(iconColor))
            {
                int spacing = 80;
                for (int x = 10; x < this.Width; x += spacing)
                {
                    string icon = officeIcons[(x / spacing) % officeIcons.Length];
                    g.DrawString(icon, iconFont, brush, x, 10);

                    //// Segunda fila
                    //g.DrawString(officeIcons[(x / spacing + 1) % officeIcons.Length],
                    //             new Font("Segoe MDL2 Assets", 12), brush, x + 40, 40);
                }
            }

        }
     
        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

    }
}
