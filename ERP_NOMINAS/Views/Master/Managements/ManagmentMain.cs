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
    public partial class ManagmentMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private ManagmentRepository _repository = new ManagmentRepository();
        private int IdManagment = 0;

        public ManagmentMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                var res = _repository.FilterByManagment(text);
                dataGridView1.DataSource = new BindingList<Managment>(res);
            };
        }

        private void ManagmentMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListManagment = _repository.GetManagments();
            Util.ConfigGrid<Managment>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Managment>(ListManagment);
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var manag = new ManagmentShow(this);
            manag.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdManagment <= 0)
            {
                MessageBox.Show("Seleccione una Gerencia", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteManag = _repository.DeleteManagment(IdManagment);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdManagment = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (IdManagment <= 0)
            {
                MessageBox.Show("Seleccione una gerencia", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var useShow = new ManagmentShow(this);
            useShow.IdManagment = IdManagment;
            useShow.Edit = true;
            useShow.ShowDialog();

        }
    }
}
