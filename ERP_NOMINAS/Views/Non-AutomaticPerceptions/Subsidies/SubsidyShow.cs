using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Subsidies;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.Subsidies
{
    public partial class SubsidyShow : BaseForm
    {
        private SubsidyMain _loadMain;
        private Utilities Util = new Utilities();
        private SubsidyRepository _repository = new SubsidyRepository();
        public int Id;
        public bool Edit;

        public SubsidyShow(SubsidyMain loadMain)
        {
            InitializeComponent();
            _loadMain = loadMain;
        }

        private void SubsidyShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                searchCatalog1.Enabled = false;
                kitCrud1.VisibleBotonCrud(false);

                Subsidy res = _repository.GetSubsidy(Id);
                searchCatalog1.SelectedValue = res.NumberEmployee;
                searchCatalog2.SelectedValue = res.Use;
                selectCycle1.SelectValue = res.Cycle;
                selectPayWeek1.PayWeek = Util.GetPayWeek(res.PayWeek);
                selectPayWeek1.PayWeekType = Util.GetPayWeekType(res.PayWeek);
                numericUpDown1.Value = res.Amount;
                richTextBox1.Text = res.Comments;
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
                    Subsidy sub = ShowSubsidy();
                    var res = _repository.CreateSubsidy(sub);
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
                    Subsidy sub = ShowSubsidy();
                    var res = _repository.UpdateSubsidy(sub);
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

        private Subsidy ShowSubsidy()
        {
            return new Subsidy
            {
                Id = Convert.ToInt32(Id),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                Cycle = Convert.ToString(selectCycle1.SelectValue),
                PayWeek = Convert.ToInt32(Util.PayWeekNow(selectPayWeek1.PayWeek,selectPayWeek1.PayWeekType)),
                Amount = Convert.ToDecimal(numericUpDown1.Value),
                Use = Convert.ToInt32(searchCatalog2.SelectedValue),
                Comments = Convert.ToString(richTextBox1.Text),
                ConceptId = Convert.ToInt32(25),
                PayrollId = Convert.ToInt32(1),

            };
        }
    }
}
