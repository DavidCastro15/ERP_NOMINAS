using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.HeightsTemperatures;
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

namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.HeightsTemperatures
{
    public partial class HeightTemperatureShow : BaseForm
    {
        private HeightTemperatureMain _loadHeightTemp;
        private Utilities Util = new Utilities();
        private HeightTemperatureRepository _repository = new HeightTemperatureRepository();
        public int RefetenceN;
        public bool Edit;
        private BindingSource _bsDetails = new BindingSource();
        private HeightTemperature _res;

        public HeightTemperatureShow(HeightTemperatureMain loadHeightTemp)
        {
            InitializeComponent();
            _loadHeightTemp = loadHeightTemp;
            _res = new HeightTemperature();
            _res.Details = new List<HeightTemperatureDetail>();
            _bsDetails.DataSource = _res.Details;        
        }

        private void searchCatalog1_OnItemSelected(object sender, EventArgs e)
        {
            if (searchCatalog1.SelectedValue != null)
            {
                searchCatalog3.FilterByEmployee(Convert.ToInt32(searchCatalog1.SelectedValue));
            }
        }

        private void HeightTemperatureShow_Load(object sender, EventArgs e)
        {
            searchCatalog1.OnItemSelected += searchCatalog1_OnItemSelected;
            dataGridView1.DataSource = _bsDetails;
            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                groupBox3.Visible = true;
                kitCrud1.VisibleBotonCrud(false);
                _res = _repository.GetHeightTemperature(RefetenceN);
                if (_res.Details == null) _res.Details = new List<HeightTemperatureDetail>();

                dateTimePicker1.Value = _res.Date;
                selectPayWeek1.PayWeek = Util.GetPayWeek(_res.PayWeek);
                selectPayWeek1.PayWeekType = Util.GetPayWeekType(_res.PayWeek);
                radioButton1.Checked = _res.Cycle == "Zafra";
                radioButton2.Checked = _res.Cycle != "Zafra";
                textBox1.Text = RefetenceN.ToString();

                _bsDetails.DataSource = _res.Details;
                textBox2.Text = _res.Details.Count.ToString();
            }

            if (searchCatalog1.SelectedValue != null)
            {
                searchCatalog3.FilterByEmployee(Convert.ToInt32(searchCatalog1.SelectedValue));
            }
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    HeightTemperature ht = ShowHeightTemperatur();
                    var res = _repository.CreateHeightTemperature(ht);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadHeightTemp.LoadGrid();
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

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    HeightTemperature ht = ShowHeightTemperatur();
                    var res = _repository.UpdateHeightTemperature(ht);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadHeightTemp.LoadGrid();

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {

                var detail = (HeightTemperatureDetail)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                _bsDetails.Remove(detail);
                textBox2.Text = (int.Parse(textBox2.Text) - 1).ToString();

            }
        }

        private void buttonAdd1_OnBotonAddClick(object sender, EventArgs e)
        {
            if (searchCatalog1.SelectedValue == null) return;

            HeightTemperatureDetail res = _repository.GetDetailEmployee(Convert.ToInt32(searchCatalog1.SelectedValue));
            res.Use = Convert.ToInt32(searchCatalog2.SelectedValue);
            res.CategoryId = Convert.ToInt32(searchCatalog3.SelectedValue);
            res.Hours = Convert.ToDecimal(numericUpDown1.Value);

            if (res != null)
            {
                _bsDetails.Add(res);

                int count = _bsDetails.Count;
                textBox2.Text = count.ToString();
                searchCatalog1.Focus();
            }
        }

        private HeightTemperature ShowHeightTemperatur()
        {
            string _cycle = groupBox1.Controls.OfType<RadioButton>()
                               .FirstOrDefault(r => r.Checked)?.Text;
            return new HeightTemperature
            {
                ReferenceNumber = Convert.ToInt32(RefetenceN),
                PayrollId = Convert.ToInt32(1),
                PayWeek = Convert.ToInt32(Util.PayWeekNow(selectPayWeek1.PayWeek, selectPayWeek1.PayWeekType)),
                Cycle = Convert.ToString(_cycle),
                Date = Convert.ToDateTime(dateTimePicker1.Value),
                IdConcept = Convert.ToInt32(40),
                Details = _res.Details
            };
        }

    }
}
