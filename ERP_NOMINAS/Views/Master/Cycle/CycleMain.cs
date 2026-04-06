using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Repositorys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.Master.Cycle
{
    public partial class CycleMain : BaseForm
    {
        Utilities Util = new Utilities();
        CycleRepository _repository = new CycleRepository();
        private string _ChangeCycle = "";
        public CycleMain()
        {
            InitializeComponent();
        }

        private void CycleMain_Load(object sender, EventArgs e)
        {
            LoadCycle();
        }
        private void LoadCycle()
        {
            label1.Text = "";
            var c = _repository.GetCycle();
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text += $"El ciclo actual es: \n \n {c._Cycle}";
            _ChangeCycle = c._Cycle;
        }

        private void button1_Click(object sender, EventArgs e)
        {         
            string message = "¿Está seguro de cambiar el ciclo?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _ChangeCycle = _ChangeCycle == "Zafra" ? "Reparación" : "Zafra";
                    var c = _repository.ChangeCycle(_ChangeCycle);
                    MessageBox.Show("Ciclo modificado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCycle();
                }
                else
                {
                    MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
