using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Components.Buttons
{
    public partial class KitCrud : UserControl
    {
        public event EventHandler Save;
        public event EventHandler Edit;
       
        public KitCrud()
        {
            InitializeComponent();
            buttonSave1.OnBotonSaveClick += (s, e) => Save?.Invoke(s, e);
            buttonFEdit1.OnBotonFEditClick += (s, e) => Edit?.Invoke(s, e);
        }

        public void VisibleBotonCrud(bool visible)
        {
            if (visible)
            {
                buttonSave1.VisibleBotonSave = true;
                buttonSave1.BringToFront();
            }
            else
            {
                buttonSave1.VisibleBotonSave = false;
                buttonFEdit1.Visible = true;
                buttonFEdit1.BringToFront();
            }
            
        }
    }
}
