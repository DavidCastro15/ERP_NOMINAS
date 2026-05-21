using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.ToolWears;
using ERP_NOMINAS.Reports.NonAutomaticPerception.ToolWears;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.ToolWears
{
    public partial class ToolWearMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private ToolWearRepository _repository = new ToolWearRepository();
        private BindingList<ToolWear> _toolWearList;

        public ToolWearMain()
        {
            InitializeComponent();
        }

        private void ToolWearMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            buttonSave1.VisibleBotonSave = true;
        }

        public void LoadGrid()
        {
            var res = _repository.GetToolWears(dateRange1.StartDate, dateRange1.EndDate);
            Util.ConfigGrid<ToolWear>(dataGridView1);
            _toolWearList = new BindingList<ToolWear>(res);
            dataGridView1.DataSource = _toolWearList;
        }

        private void buttonImportDb1_OnBotonImportDbClick(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            ToolWear toolEmpty = new ToolWear();

            using (var formEdit = new ToolWearShow(this, toolEmpty))
            {
                formEdit.Edit = false;

                if (formEdit.ShowDialog() == DialogResult.Yes)
                {
                    ToolWear nuevoRegistro = formEdit.GetCreateToolWear();
                    _toolWearList.Add(nuevoRegistro);
                }
            }

        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is ToolWear _toolWearSelect)
            {
                string message = $"¿Está seguro de que desea eliminar el registro del empleado: {_toolWearSelect.FullName}?";
                string title = "Confirmar Eliminación";

                DialogResult respuesta = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        _toolWearList.Remove(_toolWearSelect);

                        MessageBox.Show("Registro eliminado de la lista con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un registro de la lista para poder eliminarlo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void parametersEdit()
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is ToolWear toolWearSelect)
            {
                using (var formEdit = new ToolWearShow(this,toolWearSelect))
                {
                    formEdit.Edit = true;
                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        _toolWearList.ResetBindings();
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void buttonSave1_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar el desgaste de herramienta?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    List<ToolWear> list = _toolWearList.ToList();
                    var createAwards = _repository.SaveToolWears(list, Util.PayWeekNow(Convert.ToInt32(selectPayWeek1.PayWeek), Convert.ToInt32(selectPayWeek1.PayWeekType)));
                    MessageBox.Show("Registros guardados con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {
            List<ToolWearReport> list = _toolWearList.Select(tw => new ToolWearReport
            {
                NumberEmployee = tw.NumberEmployee,
                FullName = tw.FullName,
                Days = tw.Days,
                CategoryId = tw.CategoryId,
                CategoryName = tw.CategoryName,
                Amount = tw.Amount
            }).ToList();

            var res = _repository.SaveToolWearsTemp(list);
     
            var report = new toolWearListView();
            report.Temp = true;
            report.ShowDialog();
        }
    }
}
