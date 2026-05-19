using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.SupportTransportations;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.SupportTransportations
{
    public partial class SupportTransportationMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private SupportTransportationRepository _repository = new SupportTransportationRepository();
        private BindingList<SupportTransportation> _supportsBindingList;

        public SupportTransportationMain()
        {
            InitializeComponent();
        }

        private void SupportTransportationMain_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            buttonSave1.VisibleBotonSave = true;
        }

        public void LoadGrid()
        {
            var res = _repository.GetSupportTransportations(dateRange1.StartDate, dateRange1.EndDate,Convert.ToInt32( numericUpDown1.Value));
            Util.ConfigGrid<SupportTransportation>(dataGridView1);
            _supportsBindingList = new BindingList<SupportTransportation>(res);
            dataGridView1.DataSource = _supportsBindingList;
        }

        private void buttonImportDb1_OnBotonImportDbClick(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var authorize = new AuthorizeTransports();
            authorize.ShowDialog();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            SupportTransportation transportEmpty = new SupportTransportation();

            using (var formEdit = new SupportTransportationShow(this, transportEmpty))
            {
                formEdit.Edit = false;

                if (formEdit.ShowDialog() == DialogResult.Yes)
                {
                    SupportTransportation nuevoRegistro = formEdit.GetCreatedTransport();
                    _supportsBindingList.Add(nuevoRegistro);
                }
            }
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is SupportTransportation _transportSelect)
            {
                string mensaje = $"¿Está seguro de que desea eliminar el registro del empleado: {_transportSelect.FullName}?";
                string titulo = "Confirmar Eliminación";

                DialogResult respuesta = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        _supportsBindingList.Remove(_transportSelect);

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

        private void parametersEdit()
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is SupportTransportation transportSelect)
            {
                using (var formEdit = new SupportTransportationShow(this, transportSelect))
                {
                    formEdit.Edit = true;
                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        _supportsBindingList.ResetBindings();
                    }
                }
            }
        }

        private void buttonSave1_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar el apoyo de transporte?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    List<SupportTransportation> listaAPersistir = _supportsBindingList.ToList();
                    var createAwards = _repository.SaveSupportTransports(listaAPersistir, Util.PayWeekNow(Convert.ToInt32(selectPayWeek3.PayWeek), Convert.ToInt32(selectPayWeek3.PayWeekType)));
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

        private void buttonExport1_OnBotonExportClick(object sender, EventArgs e)
        {
            try
            {
                var data = _repository.GetDataTransportationExcel(Util.PayWeekNow(Convert.ToInt32(selectPayWeek2.PayWeek), Convert.ToInt32(selectPayWeek2.PayWeekType)));

                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("No hay información para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                    sfd.FileName = $"Reporte_Apoyo_Transporte_{DateTime.Now:ddMMyyyy}.xlsx";

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
            report.FVaucher = true;
            report.ShowDialog();
        }
    }
}
