using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Uses;
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

namespace ERP_NOMINAS.Views.Master.Uses
{
    public partial class UseMain : BaseForm
    {
        Utilities Util = new Utilities();
        UseRepository _repository = new UseRepository();
        private int IdUse = 0;

        public UseMain()
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
                    var res = _repository.FilterByUse(id);

                    // 3. Evitamos el NullReferenceException si el repo devuelve null
                    // y refrescamos el DataSource de forma eficiente
                    dataGridView1.DataSource = new BindingList<Use>(res ?? new List<Use>());
                }
                else
                {
                    // Opcional: Si no es un número, podrías limpiar el grid o no hacer nada
                    dataGridView1.DataSource = new BindingList<Use>();
                }
            };
        }

        private void UseMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListUse = _repository.GetUses();
            Util.ConfigGrid<Use>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Use>(ListUse);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.OnlyNumber(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdUse = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var useS = new UseShow(this);
            useS.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdUse <= 0)
            {
                MessageBox.Show("Seleccione un Uso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteUse = _repository.DeleteUse(IdUse);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void parametersEdit()
        {

            if (IdUse <= 0)
            {
                MessageBox.Show("Seleccione un uso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var useShow = new UseShow(this);
            useShow.IdUse = IdUse;
            useShow.Edit = true;
            useShow.ShowDialog();

        }
    }
}
