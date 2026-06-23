using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Groups;
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


namespace ERP_NOMINAS.Views.Master.Groups
{
    public partial class GroupShow : BaseForm
    {
        Utilities Util = new Utilities();
        GroupRepository _repository = new GroupRepository();
        private GroupMain _groupMain;
        public bool Edit;
        public int Id;

        public GroupShow(GroupMain groupMain)
        {
            InitializeComponent();
            _groupMain = groupMain;
        }

        private void GroupShow_Load(object sender, EventArgs e)
        {
           
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                var gr = _repository.GetGroup(Id);
                textBox1.Text =Convert.ToString(gr._Group);
                textBox2.Text =Convert.ToString(gr.Description);
                searchCatalog1.SelectedValue = Convert.ToInt32(gr.ManagmentId);
                searchCatalog2.SelectedValue = Convert.ToInt32(gr.DepartmentId);
                
            }
        }
        
        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que  desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Group gr = ShowGroup();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var checkEx = _repository.ExistsGroup(gr._Group);
                    if (checkEx)
                    {
                        MessageBox.Show("Este grupo ya existe, valide su informacion", "Grupo existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var createGroup = _repository.CreateGroup(gr);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _groupMain.LoadGrid();
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
            Group gr = ShowGroup();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateGroup = _repository.UpdateGroup(gr);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _groupMain.LoadGrid();

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

        private Group ShowGroup()
        {
            return new Group
            {
                Id = Convert.ToInt32(Id),
                _Group = Convert.ToInt32(textBox1.Text),
                Description = Convert.ToString(textBox2.Text),
                ManagmentId = Convert.ToInt32(searchCatalog1.SelectedValue),
                DepartmentId = Convert.ToInt32(searchCatalog2.SelectedValue)
            };
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.OnlyNumber(sender, e);
        }
    }
}
