using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.FoodVauchers;
using ERP_NOMINAS.Reports.NonAutomaticPerception.FoodVaucherSupportTransportation;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.FoodVauchers
{
    public partial class FoodVaucherMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private FoodVaucherRepository _repository = new FoodVaucherRepository();
        private int Id;
        private int NumberEmployee;
        private BindingList<FoodVaucher> _vouchersBindingList;

        public FoodVaucherMain()
        {
            InitializeComponent();
        }

        private void FoodVaucherMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            buttonSave1.VisibleBotonSave = true;
        }

        public void LoadGrid()
        {
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Ingrese dias habiles");
                return;
            }

            var res = _repository.GetListAttend(dateRange1.StartDate,dateRange1.EndDate,Convert.ToInt32(numericUpDown1.Value));
            Util.ConfigGrid<FoodVaucher>(dataGridView1);
            _vouchersBindingList = new BindingList<FoodVaucher>(res);
            dataGridView1.DataSource = _vouchersBindingList;
        }

        private void buttonImportDb1_OnBotonImportDbClick(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells[0].Value);
                NumberEmployee = Convert.ToInt32(row.Cells[1].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is FoodVaucher voucherSeleccionado)
            {
                using (var formEditar = new FoodVaucherShow(this, voucherSeleccionado))
                {
                    formEditar.Edit = true;
                    if (formEditar.ShowDialog() == DialogResult.OK)
                    {
                        _vouchersBindingList.ResetBindings();
                    }
                }
            }
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            FoodVaucher voucherVacio = new FoodVaucher();

            using (var formEditar = new FoodVaucherShow(this, voucherVacio))
            {
                formEditar.Edit = false;

                if (formEditar.ShowDialog() == DialogResult.Yes)
                {
                    FoodVaucher nuevoRegistro = formEditar.GetCreatedVoucher();
                    _vouchersBindingList.Add(nuevoRegistro);
                }
            }
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is FoodVaucher voucherSeleccionado)
            {
                string mensaje = $"¿Está seguro de que desea eliminar el registro del empleado: {voucherSeleccionado.FullName}?";
                string titulo = "Confirmar Eliminación";

                DialogResult respuesta = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        _vouchersBindingList.Remove(voucherSeleccionado);

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

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void buttonSave1_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar los premios?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    List<FoodVaucher> listaAPersistir = _vouchersBindingList.ToList();
                    var createAwards = _repository.SaveFoodVauchers(listaAPersistir, Util.PayWeekNow(Convert.ToInt32(selectPayWeek1.PayWeek), Convert.ToInt32(selectPayWeek1.PayWeekType)));
                    MessageBox.Show("Premios guardados con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void buttonExport1_OnBotonExportClick(object sender, EventArgs e)
        {
            try
            {
                var data = _repository.GetDataFoodVaucherExcel(Util.PayWeekNow(Convert.ToInt32(selectPayWeek2.PayWeek), Convert.ToInt32(selectPayWeek2.PayWeekType)));

                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("No hay información para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                    sfd.FileName = $"Reporte_Vales_Despensa_{DateTime.Now:ddMMyyyy}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        _repository.ExportToExcel(data, sfd.FileName);

                        MessageBox.Show("Archivo Excel generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {
            var report = new foodVaucherSuportTransportationListView();
            report.PayWeek = Util.PayWeekNow(Convert.ToInt32(selectPayWeek2.PayWeek), Convert.ToInt32(selectPayWeek2.PayWeekType));
            report.FVaucher = false;
            report.ShowDialog();
        }
    }
}
