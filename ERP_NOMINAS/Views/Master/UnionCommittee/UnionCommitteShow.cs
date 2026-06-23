using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.UnionCommittee;
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

namespace ERP_NOMINAS.Views.Master.UnionCommittee
{
    public partial class UnionCommitteShow : BaseForm
    {
        Utilities Util = new Utilities();
        UnionCommitteeRepository _repository = new UnionCommitteeRepository();
        private UnionCommitteMain _UnionCommitteMain;
        public int IdCommittee=0;
        public bool Edit = false;

        public UnionCommitteShow(UnionCommitteMain unionCommitteMain)
        {
            InitializeComponent();
            _UnionCommitteMain = unionCommitteMain;
        }

        private void UnionCommitteShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                var un = _repository.GetGetUnionCommittee(IdCommittee);
                searchCatalog1.SelectedValue = un.NumberEmployee;
                numericUpDown1.Value = un.Percentage;
                searchCatalog2.SelectedValue = un.CategoryHarvest;
                searchCatalog3.SelectedValue = un.CategoryRepair;
                searchCatalog4.SelectedValue = un.IdConcept;
                searchCatalog5.SelectedValue = un.Use;

            }
        }

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            UnionCom uc = ShowUnionCommitte();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateUnion = _repository.UpdateUnionCommitte(uc);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _UnionCommitteMain.LoadGrid();

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

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            UnionCom uc = ShowUnionCommitte();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createUnionC = _repository.CreateUnionCommitte(uc);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _UnionCommitteMain.LoadGrid();
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

        private UnionCom ShowUnionCommitte()
        {
            return new UnionCom
            {
                Id = Convert.ToInt32(IdCommittee),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                Percentage = Convert.ToDecimal(numericUpDown1.Value),
                CategoryHarvest = Convert.ToInt32(searchCatalog2.SelectedValue),
                CategoryRepair = Convert.ToInt32(searchCatalog3.SelectedValue),
                IdConcept = Convert.ToInt32(searchCatalog4.SelectedValue),
                Use = Convert.ToInt32(searchCatalog5.SelectedValue)
            };
        }
    }
}
