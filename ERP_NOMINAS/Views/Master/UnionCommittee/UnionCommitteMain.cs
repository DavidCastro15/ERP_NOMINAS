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
    public partial class UnionCommitteMain : BaseForm
    {
        Utilities Util = new Utilities();
        UnionCommitteeRepository _repository = new UnionCommitteeRepository();
        int IdCommittee = 0;

        public UnionCommitteMain()
        {
            InitializeComponent();

            filterByT1.FilterBy = (text) =>
            {
                dataGridView1.DataSource = new BindingList<UnionCom>(_repository.FilterByName(text));
            };
        }

        private void UnionCommitteMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListUnions = _repository.GetUnionCommittees();
            Util.ConfigGrid<UnionCom>(dataGridView1);
            dataGridView1.DataSource = new BindingList<UnionCom>(ListUnions);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdCommittee = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (IdCommittee <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var catShow = new UnionCommitteShow(this);
            catShow.IdCommittee = IdCommittee;
            catShow.Edit = true;
            catShow.ShowDialog();

        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var UnionC = new UnionCommitteShow(this);
            UnionC.ShowDialog();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdCommittee <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteCategorory = _repository.DeleteUnionCommitte(IdCommittee);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

    }
}
