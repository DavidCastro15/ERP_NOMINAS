using ERP_NOMINAS.Models.Deductions.Infonavit;
using ERP_NOMINAS.Repositorys.Deductions;
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

namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.Infonavit
{
    public partial class InfonavitShow : BaseForm
    {
        private InfonavitMain _loadMain;
        private Utilities Util = new Utilities();
        private InfonavitRepository _repository = new InfonavitRepository();
        public bool Edit;
        public int Id;

        public InfonavitShow(InfonavitMain loadMain)
        {
            InitializeComponent();
            _loadMain = loadMain;
        }

        private void InfonavitShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                searchCatalog1.Enabled = false;
                DInfonavit res = _repository.GetDInfonavit(Id);
                searchCatalog1.SelectedValue = res.NumberEmployee;
                numericUpDown1.Value = res.TotalAmount;
                dateTimePicker1.Value = (DateTime)res.CreditDate;
                numericUpDown2.Value = res.AmountPaid;
                dateTimePicker2.Value= (DateTime)res.LastDatePay;
                numericUpDown3.Value = res.CreditContributions;
                bool usesPeriodicDeduction = res.PercentageToBepaid <= 0;

                radioButton1.Checked = usesPeriodicDeduction;
                radioButton2.Checked = !usesPeriodicDeduction;
                numericUpDown4.Value = res.PercentageToBepaid <= 0 ? res.PeriodicDeduction : res.PercentageToBepaid;
                richTextBox1.Text = res.Comments;
            }
        }

        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null && rb.Checked)
            {
                if (rb.Name == "radioButton1")
                {
                    label6.Text = "Descuento Diario:";
                }
                else if (rb.Name == "radioButton2")
                {
                    label6.Text = "Porcentaje Pago:";
                }
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
                    DInfonavit dif = ShowDInfonavit();
                    var res = _repository.CreateDInfonavit(dif);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadMain.LoadGrid();
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
                    DInfonavit dif = ShowDInfonavit();
                    var res = _repository.UpdateDInfonavit(dif);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _loadMain.LoadGrid();

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
        
        private DInfonavit ShowDInfonavit()
        {
            return new DInfonavit
            {
                Id = Convert.ToInt32(Id),
                NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                TotalAmount = Convert.ToDecimal(numericUpDown1.Value),
                CreditDate = Convert.ToDateTime(dateTimePicker1.Value),
                AmountPaid = Convert.ToDecimal(numericUpDown2.Value),
                LastDatePay = Convert.ToDateTime(dateTimePicker2.Value),
                CreditContributions = Convert.ToInt32(numericUpDown3.Value),
                PeriodicDeduction = Convert.ToDecimal(radioButton1.Checked ? numericUpDown4.Value : 0),
                PercentageToBepaid = Convert.ToDecimal(radioButton2.Checked ? numericUpDown4.Value : 0),
                Comments = Convert.ToString(richTextBox1.Text),
                CalculatePercentage = Convert.ToString("Si"),
                Status = Convert.ToInt16(1)
            };
        }
    }
}
