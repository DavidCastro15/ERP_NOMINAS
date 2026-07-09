using ERP_NOMINAS.Models.Auth;
using ERP_NOMINAS.Repositorys.Auth;
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

namespace ERP_NOMINAS.Views.Auth
{
    public partial class PermissionControlMain : BaseForm
    {
        private ControlPermissionRepository _repository = new ControlPermissionRepository();
        private Utilities Util = new Utilities();
        private int Id;
        private bool isAdmin;

        public PermissionControlMain()
        {
            InitializeComponent();
        }

        private void PermissionControlMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var listC = _repository.GetControlPermissions();
            Util.ConfigGrid<ControlPermission>(dataGridView1);
            dataGridView1.DataSource = new BindingList<ControlPermission>(listC);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
                isAdmin = Convert.ToString(row.Cells[4].Value) == "Administrador" ? true : false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (Id <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var csShow = new PermissionControlShow(this);
            csShow.Id = Id;
            csShow.Edit = true;
            csShow.ShowDialog();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var pc = new PermissionControlShow(this);
            pc.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {

            if (Id <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (isAdmin)
            {
                MessageBox.Show("No puede borrar a un Administrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCs = _repository.DeleteUser(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
