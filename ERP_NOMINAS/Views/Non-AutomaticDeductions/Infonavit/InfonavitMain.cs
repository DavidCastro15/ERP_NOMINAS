using ERP_NOMINAS.Models.Deductions.Infonavit;
using ERP_NOMINAS.Repositorys.Deductions;
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

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.Infonavit
{
    public partial class InfonavitMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private InfonavitRepository _repository = new InfonavitRepository();
        private int Id;

        public InfonavitMain()
        {
            InitializeComponent();
        }

        private void InfonavitMain_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["Column7"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns["Column8"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var listInf = _repository.GetDInfonavits();
            Util.ConfigGrid<DInfonavit>(dataGridView1);
            dataGridView1.DataSource = new BindingList<DInfonavit>(listInf);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
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
            var ifs = new InfonavitShow(this);
            ifs.Id = Id;
            ifs.Edit = true;
            ifs.ShowDialog();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var ifs = new InfonavitShow(this);
            ifs.ShowDialog();
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
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCs = _repository.DeleteInfonavit(Id);
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
