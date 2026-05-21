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
            // Ejecuta el escáner automático en todo el formulario
            AutoDetectDesignGrids(this);
        }

        private void AutoDetectDesignGrids(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control is DataGridView dgv)
                {
                    ApplyTailwindGrid(dgv); // Si encuentra uno, le aplica el diseño
                }
                else if (control.HasChildren)
                {
                    // Si el control tiene hijos (ej. un Panel), busca también adentro de él
                    AutoDetectDesignGrids(control);
                }
            }
        }

        private void ApplyTailwindGrid(DataGridView dgv)
        {
            // Fuentes y Tipografía (Estilo font-sans de Tailwind)
            dgv.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.BackgroundColor = Color.White;

            // Borde exterior plano
            dgv.BorderStyle = BorderStyle.FixedSingle;
            Color colorBordeTailwind = Color.FromArgb(226, 232, 240); // slate-200

            // Bordes de las celdas
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = colorBordeTailwind;

            // Configuración de Comportamiento e Interfaz moderna
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // CORRECCIÓN CLAVE: Desactivar el autorrelleno global para RESPETAR tus Sizes fijos
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Alturas estilo padding de Tailwind
            dgv.RowTemplate.Height = 44;
            dgv.ColumnHeadersHeight = 40;

            // 1. Encabezados Estilo Tailwind (bg-slate-50 / text-slate-500)
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(248, 250, 252);
            headerStyle.ForeColor = Color.FromArgb(100, 116, 139);
            headerStyle.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;

            // 2. Estilo de Filas de Datos (text-slate-700 / bg-white)
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.BackColor = Color.White;
            rowStyle.ForeColor = Color.FromArgb(51, 65, 85);

            // Selección moderna (sky-50 / sky-700)
            rowStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            rowStyle.SelectionForeColor = Color.FromArgb(3, 105, 161);
            dgv.DefaultCellStyle = rowStyle;

            // 3. Filas Alternas
            DataGridViewCellStyle altRowStyle = new DataGridViewCellStyle();
            altRowStyle.BackColor = Color.FromArgb(252, 254, 255);
            altRowStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            altRowStyle.SelectionForeColor = Color.FromArgb(3, 105, 161);
            dgv.AlternatingRowsDefaultCellStyle = altRowStyle;

            // 4. Pintar el borde exterior gris
            dgv.Paint += (sender, e) =>
            {
                using (Pen pen = new Pen(colorBordeTailwind, 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, dgv.Width - 1, dgv.Height - 1);
                }
            };

        }
    }
}
