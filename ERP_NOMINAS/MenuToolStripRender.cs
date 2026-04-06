using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace ERP_NOMINAS
{
    public class MenuToolStripRender : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            // VALIDACIÓN: ¿Es un ítem de la barra principal?
            // Si el Owner es el MenuStrip, aplicamos el diseño especial.
            // Si es un submenú (DropDown), usamos el diseño estándar.
            if (e.ToolStrip is MenuStrip)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(1, 1, e.Item.Width - 3, e.Item.Height - 3);

                Color colorFondo;
                Color colorBorde;

                if (e.Item.Selected || e.Item.Pressed)
                {
                    // Color intenso al pasar el mouse
                    colorFondo = Color.FromArgb(200, 135, 206, 250);
                    colorBorde = Color.DeepSkyBlue;
                }
                else
                {
                    // Color sutil permanente al cargar
                    colorFondo = Color.FromArgb(50, 135, 206, 250);
                    colorBorde = Color.LightSkyBlue;
                }

                using (SolidBrush brush = new SolidBrush(colorFondo))
                {
                    g.FillRectangle(brush, rect);
                }

                using (Pen pen = new Pen(colorBorde, 1f))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
            else
            {
                // Esto mantiene el comportamiento original para los submenús (hijos)
                base.OnRenderMenuItemBackground(e);
            }
        }

        public MenuToolStripRender() : base(new MisColoresBlancos()) { }

        // Quita el borde nativo que pone Windows
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }

        // Personaliza el texto (Negro para que resalte sobre el blanco)
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Color.FromArgb(0, 0, 0); // Gris muy oscuro/negro
            base.OnRenderItemText(e);
        }
    }

    public class MisColoresBlancos : ProfessionalColorTable
    {

        Color azulTransparente = Color.FromArgb(180, 135, 206, 250);
        Color seleccionAzul = Color.FromArgb(220, 135, 206, 250); 

        public override Color ToolStripGradientBegin => azulTransparente;
        public override Color ToolStripGradientEnd => azulTransparente;
        public override Color ToolStripDropDownBackground => Color.White; 

        public override Color ImageMarginGradientBegin => Color.White;
        public override Color ImageMarginGradientMiddle => Color.White;
        public override Color ImageMarginGradientEnd => Color.White;

        public override Color MenuItemSelected => seleccionAzul;
        public override Color MenuItemSelectedGradientBegin => seleccionAzul;
        public override Color MenuItemSelectedGradientEnd => seleccionAzul;

        public override Color MenuItemBorder => Color.Transparent;
        public override Color MenuBorder => Color.FromArgb(200, 225, 245);

    }


}
