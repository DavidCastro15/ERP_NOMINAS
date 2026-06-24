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

namespace ERP_NOMINAS
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
            // 1. 🔥 PROTECCIÓN ANTIDESIGNER NIVEL 1: 
            // Si Visual Studio está intentando dibujar la pantalla en el editor,
            // ejecutamos ÚNICAMENTE los componentes visuales mínimos y salimos de inmediato.
            // Esto evita que choquen los servicios internos de Windows Forms.
            bool modoDiseñoActivo = LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode;

            if (modoDiseñoActivo)
            {
                InitializeComponent();
                return; // Detiene la ejecución para que el diseñador visual no truene
            }

            // 2. Ejecución Normal (Esto solo correrá cuando el usuario final abra el ERP real)
            this.Load += BaseForm_Load;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.White;

            this.Paint += GlobalForm_Paint;
            this.HeaderColor = Color.LightGreen;
            ChangeColorHead(ColorSelect);
            SetDefaultBorderColor(ColorSelect);
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            // 🔥 PROTECCIÓN ANTIDESIGNER NIVEL 2:
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            try
            {
                // Forzamos la inyección directa de auditoría de forma segura
                ERP_SHARED.GlobalFunctions.Logs.Auditor.RegisterChildScreen(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fallo de auditoría en la pantalla {this.GetType().Name}:\n\n{ex.Message}\n\n{ex.StackTrace}",
                   "Alerta de Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Tu lógica de diseño y centrado original permanece intacta
            AutoDetectDesignGrids(this);

            if (this.StartPosition == FormStartPosition.CenterScreen)
            {
                Screen screen = Screen.FromControl(this);
                Rectangle workingArea = screen.WorkingArea;

                this.Location = new Point(
                    workingArea.Left + (workingArea.Width - this.Width) / 2,
                    workingArea.Top + (workingArea.Height - this.Height) / 2
                );
            }
        
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
     
        private void AutoDetectDesignGrids(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control is DataGridView dgv)
                {
                    ApplyTailwindGrid(dgv);
                }
                //else if (control is TextBox txt)
                //{
                //    ApplyModernTextBoxStyle(txt); // NUEVO: Aplica el efecto de línea inferior al TextBox
                //}
                else if (control.HasChildren)
                {
                    AutoDetectDesignGrids(control);
                }
            }
        }

        private void ApplyModernTextBoxStyle(TextBox txt)
        {
            // 1. Quitamos el borde 3D antiguo de Windows para evitar la línea azul marino
            txt.BorderStyle = BorderStyle.None;

            Control contenedorLinea = txt.Parent;

            // 2. IMPORTANTE: Dejamos un margen sutil para que el texto no se pegue a los nuevos bordes planos
            txt.Margin = new Padding(6);

            // Al tomar el foco (Hacer Click / Escribir)
            txt.Enter += (sender, e) =>
            {
                if (contenedorLinea != null && contenedorLinea != this)
                {
                    contenedorLinea.BackColor = Color.FromArgb(248, 250, 252); // Fondo Slate-50 muy sutil
                }
                contenedorLinea.Invalidate(); // Fuerza el redibujado inmediato del borde activo
            };

            // Al perder el foco (Salir del campo)
            txt.Leave += (sender, e) =>
            {
                if (contenedorLinea != null && contenedorLinea != this)
                {
                    contenedorLinea.BackColor = Color.White;
                }
                contenedorLinea.Invalidate(); // Fuerza el redibujado inmediato del borde en reposo
            };

            // 3. Dibujado inteligente de un contenedor perimetral plano (Estilo Tailwind Input)
            contenedorLinea.Paint += (sender, e) =>
            {
                // Si tiene el foco usamos LightSkyBlue (grosor 2px). Si no lo tiene, un gris Slate-300 muy claro (grosor 1px).
                Color colorBorde = txt.Focused ? ColorSelect : Color.FromArgb(203, 213, 225);
                int grosor = txt.Focused ? 2 : 1;

                using (Pen pen = new Pen(colorBorde, grosor))
                {
                    if (contenedorLinea == this)
                    {
                        // CASO A: TextBox sueltos en otros formularios.
                        // Dibujamos un rectángulo perfecto a su alrededor para simular un borde plano moderno.
                        int x = txt.Left - 4;
                        int y = txt.Top - 4;
                        int ancho = txt.Width + 8;
                        int alto = txt.Height + 8;

                        e.Graphics.DrawRectangle(pen, x, y, ancho, alto);
                    }
                    else
                    {
                        // CASO B: En tu Login (donde el TextBox está protegido dentro de un Panel).
                        // Dibujamos el rectángulo plano siguiendo exactamente el límite exterior de tu Panel.
                        e.Graphics.DrawRectangle(pen, 0, 0, contenedorLinea.Width - 1, contenedorLinea.Height - 1);
                    }
                }
            };

            // 4. Provocamos que el formulario se entere de los nuevos bordes inmediatamente al abrirse
            contenedorLinea.Invalidate();
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
