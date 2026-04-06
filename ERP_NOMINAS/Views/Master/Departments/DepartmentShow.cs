using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Departments;
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
using static ERP_NOMINAS.GlobalFunctions.Utilities;

namespace ERP_NOMINAS.Views.Master.Departments
{
    public partial class DepartmentShow : BaseForm
    {
        Utilities Util = new Utilities();
        private DepartmentRepository _repository = new DepartmentRepository();
        private DepartmentMain _deparLoad;
        public int IdDepartment = 0;
        private int idDep = 0;
        
        public bool Edit;

        public DepartmentShow(DepartmentMain deparLoad)
        {
            InitializeComponent();
            _deparLoad = deparLoad;
        }

        private void DepartmentShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                var dp = _repository.GetDepartment(IdDepartment);
                IdDepartment = dp.Id;
                idDep = dp.DepartmentId;
                textBox1.Text = dp.NameDepartment;
                textBox2.Text = dp.Responsible;
                searchCatalog1.SelectedValue = dp.ManagmentId;
            }
            else
            {
                idDep = _repository.CheckNextId();
            }

        } 

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Department dp = ShowDepartment();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createDepartment= _repository.CreateDepartment(dp);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _deparLoad.LoadGrid();
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
            Department dp = ShowDepartment();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateDepartment = _repository.UpdateDepartment(dp);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _deparLoad.LoadGrid();

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

        private Department ShowDepartment()
        {
            return new Department
            {
                Id = Convert.ToInt32(IdDepartment),
                DepartmentId = Convert.ToInt32(idDep),
                ManagmentId = Convert.ToInt32(searchCatalog1.SelectedValue),
                NameDepartment = Convert.ToString(textBox1.Text),
                Responsible = Convert.ToString(textBox2.Text)
            };
        }
    }
}
