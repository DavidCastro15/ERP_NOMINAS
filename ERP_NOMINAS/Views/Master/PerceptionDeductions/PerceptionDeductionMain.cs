using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.PerceptionDeduction;
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
namespace ERP_NOMINAS.Views.Master.PerceptionDeductions
{
    public partial class PerceptionDeductionMain : BaseForm
    {
        Utilities Util = new Utilities();
        PerceptionDeductionRepository _repository = new PerceptionDeductionRepository();
        private int IdPd = 0;
        
        public PerceptionDeductionMain()
        {
            InitializeComponent();

            filterByT1.FilterBy = (text) =>
            {
                // 1. Usar string.IsNullOrWhiteSpace es más seguro que ""
                if (string.IsNullOrWhiteSpace(text))
                {
                    LoadGrid();
                    return;
                }

                // 2. TryParse evita que la app se cierre si escriben letras
                if (int.TryParse(text, out int id))
                {
                    var res = _repository.FilterById(id);

                    // 3. Evitamos el NullReferenceException si el repo devuelve null
                    // y refrescamos el DataSource de forma eficiente
                    dataGridView1.DataSource = new BindingList<PerceptDeduction>(res ?? new List<PerceptDeduction>());
                }
                else
                {
                    // Opcional: Si no es un número, podrías limpiar el grid o no hacer nada
                    dataGridView1.DataSource = new BindingList<PerceptDeduction>();
                }
            };        
        }

        private void PerceptionDeductionMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListPD = _repository.GetPerceptionDeductions();
            Util.ConfigGrid<PerceptDeduction>(dataGridView1);         
            dataGridView1.DataSource = new BindingList<PerceptDeduction>(ListPD);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el caracter presionado es un número o la tecla de borrar
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignora la tecla presionada
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                return;
            }
            List<PerceptDeduction> filterById = _repository.FilterById(Convert.ToInt32(textBox1.Text));
            dataGridView1.DataSource = new BindingList<PerceptDeduction>(filterById);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdPd = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (IdPd <= 0)
            {
                MessageBox.Show("Seleccione una Percepcion o Deduccion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var pdShow = new PerceptionDeductionShow(this);
            pdShow.IdPd = IdPd;
            pdShow.Edit = true;
            pdShow.ShowDialog();

        }
 
        private void kitForm1_Add(object sender, EventArgs e)
        {
            var showPD = new PerceptionDeductionShow(this);
            showPD.ShowDialog();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdPd <= 0)
            {
                MessageBox.Show("Seleccione una percepcion o deduccion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deletePd = _repository.DeletePerceptionDeduction(IdPd);
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
