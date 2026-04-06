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
    public partial class KitForm : UserControl
    {

        public event EventHandler Add;
        public event EventHandler MEdit;
        public event EventHandler MDelete;

        public KitForm()
        {
            InitializeComponent();
            buttonAdd1.OnBotonAddClick += (s, e) => Add?.Invoke(s, e);
            buttonMEdit1.OnBotonMEditlick += (s, e) => MEdit?.Invoke(s, e);
            buttonMDelete1.OnBotonMDeleteClick += (s, e) => MDelete?.Invoke(s, e);   
        }

        public void VisibleButtonAdd(bool Hidden = true)
        {
            buttonAdd1.Visible = Hidden;
        }
    }
}
