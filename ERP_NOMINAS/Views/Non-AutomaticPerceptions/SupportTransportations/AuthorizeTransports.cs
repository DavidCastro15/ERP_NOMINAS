using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.SupportTransportations;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.SupportTransportations
{
    public partial class AuthorizeTransports : BaseForm
    {
        private Utilities Util = new Utilities();
        private SupportTransportationRepository _repository = new SupportTransportationRepository();
        private int Id;

        public AuthorizeTransports()
        {
            InitializeComponent();
        }

        private void AuthorizeTransports_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            buttonSave1.VisibleBotonSave = true;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var res = _repository.GetAuthorizeTransports();
            Util.ConfigGrid<AuthorizeTransport>(dataGridView1);
            dataGridView1.DataSource = new BindingList<AuthorizeTransport>(res);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void buttonSave1_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            AuthorizeTransport res = ShowAuthorizeTransport();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createAuthorize = _repository.AddEmployeeTransport(res);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGrid();
                    groupBox1.Visible = false;
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

        private AuthorizeTransport ShowAuthorizeTransport()
        {
            return new AuthorizeTransport
            {
                Id = Convert.ToInt32(0),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue)
            };
        }

        private void buttonMDelete1_OnBotonMDeleteClick(object sender, EventArgs e)
        {
            if (Id <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteEmployee = _repository.DeleteEmployee(Id);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void buttonAdd1_OnBotonAddClick(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }
    }
}
