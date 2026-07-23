using ERP_NOMINAS.Models.PayRollProcess.ApplyDeductions;
using ERP_NOMINAS.Repositorys.PayRollProcess;
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

namespace ERP_NOMINAS.Views.PayRollProcess.ApplyDeductions
{
    public partial class ApplyDeductionsMain : BaseForm
    {
        private ApplyDeductionRepository _repository = new ApplyDeductionRepository();
        private Utilities Util = new Utilities();
        private int Id;
        private string Status;

        public ApplyDeductionsMain()
        {
            InitializeComponent();
        }

        private void ApplyDeductionsMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var list = _repository.GetAPDeductions();
            Util.ConfigGrid<APDeduction>(dataGridView1);
            dataGridView1.DataSource = new BindingList<APDeduction>(list);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
                Status = Convert.ToString(row.Cells[3].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Id <= 0)
            {
                MessageBox.Show("Seleccione una deduccion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea cambiar el estado de este registro?";
            string title = "Confirmar Cambio";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var updateAll = _repository.CheckedOne(Id,Status);
                MessageBox.Show("Registro actualizado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Id <= 0)
            {
                MessageBox.Show("Seleccione una deduccion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea cambiar el estado de los registros?";
            string title = "Confirmar Cambios";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var updateAll = _repository.CheckedAll();
                MessageBox.Show("Registros actualizados con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
