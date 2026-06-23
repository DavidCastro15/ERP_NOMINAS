using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.AwardPunctPre;
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
    public partial class PieceworkAwardMain : BaseForm
    {
        private Utilities Util = new Utilities();
        private AwardPPRepository _repository = new AwardPPRepository();
        private BindingSource _bsDetails = new BindingSource();
        private List<PieceworkAward> _Details;

        public PieceworkAwardMain()
        {
            InitializeComponent();
            _Details = new List<PieceworkAward>();
        }

        private void PieceworkAwardMain_Load(object sender, EventArgs e)
        {
            buttonSave1.VisibleBotonSave = true;
            dataGridView1.AutoGenerateColumns = false;
        }

        public void LoadGrid()
        {
            dataGridView1.DataSource = _bsDetails;
            var res = _repository.GetPieceworkAwards();
            Util.ConfigGrid<PieceworkAward>(dataGridView1);
            dataGridView1.DataSource = new BindingList<PieceworkAward>(res);
            _Details = res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog { Filter = "CSV Files|*.csv" };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonImport1_OnBotonImportClick(object sender, EventArgs e)
        {
            
            string message = "¿Está seguro de que importar este archivo? \nTodos los registros anteriores seran borrados!!!!!!!";
            string title = "Importar Empleados";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                try
                {
                    if (numericUpDown1.Value <= 0)
                    {
                        MessageBox.Show("Ingreso el monto del destajo");
                        return;
                    }

                    DataTable dt = Util.GetDataCSV(openFileDialog1.FileName); // Método para leer el archivo
                    _repository.ImportEmployees(dt,numericUpDown1.Value);
                    MessageBox.Show("Importacion masiva con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonSave1_OnBotonSaveClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var res = _repository.SavePiecewokAward(_Details);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
