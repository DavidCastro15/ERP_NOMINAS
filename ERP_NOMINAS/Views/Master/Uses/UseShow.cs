using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Uses;
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


namespace ERP_NOMINAS.Views.Master.Uses
{
    public partial class UseShow : BaseForm
    {
        Utilities Util = new Utilities();
        UseRepository _repository = new UseRepository();

        private UseMain _useMain;
        public int IdUse = 0;
        public bool Edit;

        public UseShow(UseMain useMain)
        {
            InitializeComponent();
            _useMain = useMain;
        }

        private void UseShow_Load(object sender, EventArgs e)
        {

            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                Use eu= _repository.GetUse(IdUse);

                textBox1.Text = Convert.ToString(eu._Use);
                textBox2.Text = Convert.ToString(eu.Description);
                searchCatalog1.SelectedValue = Convert.ToInt32(eu.IdManagment);
                searchCatalog2.SelectedValue = Convert.ToInt32(eu.IdDepartment);
                searchCatalog3.SelectedValue = Convert.ToInt32(eu.Group);
                searchCatalog4.SelectedValue = Convert.ToInt32(eu.Equipment);
                textBox3.Text = Convert.ToString(eu.AccountingAccount);
            }
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que  desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Use u = ShowUse();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createUse = _repository.CreateUse(u);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _useMain.LoadGrid();
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
            Use u = ShowUse();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateUse = _repository.UpdateUse(u);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _useMain.LoadGrid();

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

        private Use ShowUse()
        {
            return new Use
            {
                Id = Convert.ToInt32(IdUse),
                _Use = Convert.ToInt64(textBox1.Text),
                Description = Convert.ToString(textBox2.Text),
                IdManagment = Convert.ToInt32(searchCatalog1.SelectedValue),
                IdDepartment = Convert.ToInt32(searchCatalog2.SelectedValue),
                Group = Convert.ToInt32(searchCatalog3.SelectedValue),
                Equipment = Convert.ToInt32(searchCatalog4.SelectedValue),
                AccountingAccount = Convert.ToString(textBox3.Text)
                
            };
        }
    }
}
