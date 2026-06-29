using ERP_NOMINAS.Models.Deductions.ChildSupport;
using ERP_NOMINAS.Repositorys.Deductions;
using ERP_SHARED.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.ChildSupports
{ 
    public partial class ChildSupportShow : BaseForm
    {
        private ChildSupportMain _loadMain;
        private Utilities Util = new Utilities();
        private ChildSupportRepository _repository = new ChildSupportRepository();
        public int Id;
        public bool Edit;

        public ChildSupportShow(ChildSupportMain loadMain)
        {
            InitializeComponent();
            _loadMain = loadMain;
        }

        private void ChildSupportShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                searchCatalog1.Enabled = false;
                kitCrud1.VisibleBotonCrud(false);
                var res = _repository.GetCSupport(Id);
                searchCatalog1.SelectedValue = res.NumberEmployee;
                numericUpDown1.Value = Convert.ToDecimal(res.SupportAmount);
                numericUpDown2.Value = Convert.ToDecimal(res.SupportPercentage);

                string[] fullNameBeneficiaryName = res.BeneficiaryName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                textBox1.Text = Convert.ToString(fullNameBeneficiaryName[0]);
                textBox2.Text = Convert.ToString(fullNameBeneficiaryName[1]);
                textBox3.Text = Convert.ToString(fullNameBeneficiaryName[2]);
                textBox4.Text = Convert.ToString(res.BankAccount);
            }
            
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    CSupport cs = ShowCSupport();
                    var res = _repository.CreateCSupport(cs);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadMain.LoadGrid();
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

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    CSupport cs = ShowCSupport();
                    var res = _repository.UpdateCSupport(cs);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadMain.LoadGrid();

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

        private CSupport ShowCSupport()
        {
            return new CSupport
            {
                Id = Convert.ToInt32(Id),
                NumberEmployee= Convert.ToInt32(searchCatalog1.SelectedValue),
                SupportAmount = Convert.ToDecimal(numericUpDown1.Value),
                SupportPercentage = Convert.ToDecimal(numericUpDown2.Value),
                BeneficiaryName = Convert.ToString($"{textBox1.Text} {textBox2.Text} {textBox3.Text}"),
                BankAccount = Convert.ToString(textBox4.Text)
            };
        }
    }
}
