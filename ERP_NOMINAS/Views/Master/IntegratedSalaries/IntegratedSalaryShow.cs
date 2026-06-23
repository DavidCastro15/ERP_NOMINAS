using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.IntegratedSalaries;
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

namespace ERP_NOMINAS.Views.Master.IntegratedSalaries
{
    public partial class IntegratedSalaryShow : BaseForm
    {
        private Utilities Util = new Utilities();
        private IntegratedSalaryRepository _repository = new IntegratedSalaryRepository();
        private IntegratedSalaryMain _integratedSalaryMain;
        public int IdIntegratedSalary;
        public bool Edit;

        public IntegratedSalaryShow(IntegratedSalaryMain integrated)
        {
            InitializeComponent();
            _integratedSalaryMain = integrated;
        }

        private void IntegratedSalaryShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);

                IntegratedSalary i = _repository.GetIntegratedSalary(IdIntegratedSalary);

                searchCatalog1.SelectedValue = i.NumberEmployee;
                numericUpDown1.Value = i.SalaryIntegratedImss;
                numericUpDown2.Value = i.SalaryIntegratedInfonavit;
            }
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            IntegratedSalary isalary = ShowIntegratedSalary();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createIntegrated= _repository.CreateIntegratedSalary(isalary);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _integratedSalaryMain.LoadGrid();
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

            IntegratedSalary isalary = ShowIntegratedSalary();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var editIntegrated = _repository.UpdateIntegratedSalary(isalary);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _integratedSalaryMain.LoadGrid();

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

        private IntegratedSalary ShowIntegratedSalary()
        {
            return new IntegratedSalary
            {
                Id = Convert.ToInt32(IdIntegratedSalary),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                SalaryIntegratedImss = Convert.ToDecimal(numericUpDown1.Value),
                SalaryIntegratedInfonavit = Convert.ToDecimal(numericUpDown2.Value),

            };
        }
    }
}
