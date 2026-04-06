using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Managments;
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

namespace ERP_NOMINAS.Views.Master.Managements
{
    public partial class ManagmentShow : BaseForm
    {
        Utilities Util = new Utilities();
        ManagmentRepository _repository = new ManagmentRepository();

        private ManagmentMain _managMain;
        public int IdManagment = 0;
        public bool Edit;

        public ManagmentShow(ManagmentMain managmentMain)
        {
            InitializeComponent();
            _managMain = managmentMain;
        }

        private void ManagmentShow_Load(object sender, EventArgs e)
        {
           
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                var mg = _repository.GetManagment(IdManagment);
                IdManagment = mg.Id;
                textBox1.Text = mg.Name;
                textBox2.Text = mg.Responsible;

            }
            else
            {
                IdManagment = _repository.CheckNextId();
            }

           
        }

        
        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Managment mg = ShowManagment();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createCategory = _repository.CreateManagment(mg);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _managMain.LoadGrid();
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
            Managment mg = ShowManagment();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateCategory = _repository.UpdateManagment(mg);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _managMain.LoadGrid();

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

        private Managment ShowManagment()
        {
            return new Managment
            {
                Id = Convert.ToInt32(IdManagment),
                ManagmentId = Convert.ToInt32(IdManagment),
                Name = Convert.ToString(textBox1.Text),
                Responsible = Convert.ToString(textBox2.Text)
            };
        }
    }
}
