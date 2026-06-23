using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.AwardPunctPre;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.AwardPunctPre
{
    public partial class AwardPPShow : BaseForm
    {
        private Utilities Util = new Utilities();
        private AwardPPRepository _repository = new AwardPPRepository();
        private AwardPPMain _loadMain;
        public int Id;
        public bool Edit;

        public AwardPPShow(AwardPPMain loadMain)
        {
            InitializeComponent();
            _loadMain = loadMain;
        }

        private void AwardPPShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                searchCatalog1.Enabled = false;
                kitCrud1.VisibleBotonCrud(false);
                AwardPPF res = _repository.GetAward(Id);
                searchCatalog1.SelectedValue = res.NumberEmployee;
                searchCatalog2.SelectedValue = res.Use;
                numericUpDown1.Value = Convert.ToInt32(res.DaysPF);
                numericUpDown2.Value = Convert.ToDecimal(res.PF);
                numericUpDown3.Value = Convert.ToInt32(res.DaysPP);
                numericUpDown4.Value = Convert.ToDecimal(res.PP);

            }

        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            AwardPPF award = ShowAwards();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createAward = _repository.CreateAwardEmployee(award);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadMain.LoadAwards();
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
            AwardPPF award = ShowAwards();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateCategory = _repository.UpdateAwardEmployee(award);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadMain.LoadAwards();

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

        private AwardPPF ShowAwards()
        {
            decimal sal = _repository.GetSalaryEmployee(Convert.ToInt32(searchCatalog1.SelectedValue));

            return new AwardPPF
            {
                Id = Convert.ToInt32(Id),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                Salary = Convert.ToDecimal(sal),
                DaysPF = Convert.ToInt32(numericUpDown1.Value),
                PF = Convert.ToDecimal(numericUpDown2.Value),
                AmountPF = Convert.ToDecimal(numericUpDown2.Value * sal),
                DaysPP = Convert.ToInt32(numericUpDown3.Value),
                PP = Convert.ToDecimal(numericUpDown4.Value),
                AmountPP = Convert.ToDecimal(numericUpDown4.Value * sal),
                Use = Convert.ToInt32(searchCatalog2.SelectedValue),
                Comments = Convert.ToString(richTextBox1.Text)
            };
        }
    }
}
