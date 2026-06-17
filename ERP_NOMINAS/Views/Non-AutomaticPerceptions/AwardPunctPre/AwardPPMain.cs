using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.AwardPunctPre;
using ERP_NOMINAS.Reports.NonAutomaticPerception.AwardsPPF;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.AwardPunctPre
{
    public partial class AwardPPMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private AwardPPRepository _repository = new AwardPPRepository();
        private BindingSource _bsDetails = new BindingSource();
        private List<ListAttend> _ListAttendDetails;
        private int IdAwardTemp;

        public AwardPPMain()
        {         
            InitializeComponent();
            _ListAttendDetails = new List<ListAttend>();
        }

        private void AwardPPMain_Load(object sender, EventArgs e)
        {
 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView3.AutoGenerateColumns = false;

            dataGridView2.Columns["Column9"].DefaultCellStyle.Format = "yyyy-MM-dd";
            buttonSave1.VisibleBotonSave = true;
            buttonSave2.VisibleBotonSave = true;
            dataGridView1.AutoGenerateColumns = false;

            LoadAwards();

        }

        private void LoadLastAward()
        {
            var res = _repository.GetLastAward();
            Util.ConfigGrid<LastAward>(dataGridView1);
            dataGridView1.DataSource = new BindingList<LastAward>(res);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadLastAward();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numericUpDown2.Value<= 0)
            {
                MessageBox.Show("Ingresa los dias habiles");
                return;
            }
            int days = Util.PublicHoliday(dateRange1.StartDate, dateRange1.EndDate);
            numericUpDown1.Value = days;
            LoadDetailsAttend();
        }

        private void LoadDetailsAttend()
        {
            
            dataGridView2.DataSource = _bsDetails;
            var res = _repository.GetDetailAttend(dateRange1.StartDate, dateRange1.EndDate);
            Util.ConfigGrid<ListAttend>(dataGridView2);
            dataGridView2.DataSource = new BindingList<ListAttend>(res);
            _ListAttendDetails = res;
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Esto evita que aparezca el cuadro de diálogo del error
            e.ThrowException = false;

            // Opcional: ver qué error es en la consola de depuración
            Console.WriteLine("Error en fila: " + e.RowIndex + " Columna: " + e.ColumnIndex);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var res = _repository.FilterByNameListAttend(Convert.ToInt32(searchCatalog1.SelectedValue));
            Util.ConfigGrid<ListAttend>(dataGridView2);
            dataGridView2.DataSource = new BindingList<ListAttend>(res);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var res = _repository.ClearFilterByNameListAttend();
            Util.ConfigGrid<ListAttend>(dataGridView2);
            dataGridView2.DataSource = new BindingList<ListAttend>(res);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var pieceShow = new PieceworkAwardMain();
            pieceShow.ShowDialog();
        }

        private void buttonSave1_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar los registros?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var res = _repository.UpdateListAttend(_ListAttendDetails);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


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

        private void button4_Click(object sender, EventArgs e)
        {         
            _repository.AwardsOverview(Convert.ToInt32(numericUpDown2.Value));
            LoadAwards();
        }

        public void LoadAwards()
        {
            try
            {           
                var res = _repository.GetAwardsOverview();
                Util.ConfigGrid<AwardPPF>(dataGridView3);
                dataGridView3.DataSource = new BindingList<AwardPPF>(res);

            }
            catch (Exception)
            {

                MessageBox.Show("Error en cargar los premios, no hay informacion existente");
            } 
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                IdAwardTemp = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (IdAwardTemp <= 0)
            {
                MessageBox.Show("Seleccione un Empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var awardShow = new AwardPPShow(this);
            awardShow.Edit = true;
            awardShow.Id = IdAwardTemp;
            awardShow.ShowDialog();
        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var awardShow = new AwardPPShow(this);
            awardShow.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdAwardTemp <= 0)
            {
                MessageBox.Show("Seleccione un Empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {

                var deleteEmployee = _repository.DeleteAwardEmployee(IdAwardTemp);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAwards();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void buttonSave2_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar los premios?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createAwards = _repository.SaveAwardOverview(Util.PayWeekNow(Convert.ToInt32(selectPayWeek1.PayWeek), Convert.ToInt32(selectPayWeek1.PayWeekType)));
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

        private void buttonPrint1_OnBotonPrintClick(object sender, EventArgs e)
        {
            var report = new awardsPPFListView();
            report.Temp = false;
            report.Payweek = Convert.ToInt32(searchPayWeek1.SelectedValue);
            report.ShowDialog();
        }

        private void buttonPrint2_OnBotonPrintClick(object sender, EventArgs e)
        {
            var report = new awardsPPFListView();
            report.Temp = true;
            report.Payweek = 0;
            report.ShowDialog();
        }
    }
}
