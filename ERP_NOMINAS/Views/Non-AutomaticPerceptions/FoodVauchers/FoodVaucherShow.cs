using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.FoodVauchers;
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
    public partial class FoodVaucherShow : BaseForm
    {
        private Utilities Util = new Utilities();
        private FoodVaucherRepository _repository = new FoodVaucherRepository();
        private FoodVaucher _voucher;
        private FoodVaucherMain _loadMain;
        public bool Edit;

        public FoodVaucherShow(FoodVaucherMain loadMain, FoodVaucher voucher)
        {
            InitializeComponent();
            _loadMain = loadMain;
            _voucher = voucher;
        }

        private void FoodVaucherShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                searchCatalog1.Enabled = false;
                numericUpDown1.Enabled = false;
                searchCatalog1.SelectedValue = _voucher.NumberEmployee;
                numericUpDown1.Value = _voucher.BusinessDays;
                numericUpDown2.Value = _voucher.WorkedDays;
                numericUpDown3.Value = _voucher.Amount;

                for (int i = 1; i <= 31; i++)
                {
                    Control[] found = GroupBox1.Controls.Find($"CheckBox{i}", true);

                    if (found.Length > 0 && found[0] is CheckBox chk)
                    {
                        var propertyInfo = _voucher.GetType().GetProperty($"D{i}");
                        if (propertyInfo != null)
                        {
                            byte valor = (byte)propertyInfo.GetValue(_voucher);

                            chk.Checked = (valor > 0);
                        }
                    }
                }
            }
            else
            {

                searchCatalog1.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                GroupBox1.Enabled = true;
                searchCatalog1.SelectedValue = null;
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;

                for (int i = 1; i <= 31; i++)
                {
                    Control[] found = GroupBox1.Controls.Find($"CheckBox{i}", true);
                    if (found.Length > 0 && found[0] is CheckBox chk)
                    {
                        chk.Checked = false;
                    }
                }
            }

        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            try
            {
                string fullName = "";
                string txtCatalog = searchCatalog1.SelectedText;

                if (!string.IsNullOrEmpty(txtCatalog) && txtCatalog.Contains("--"))
                {
                    string[] parts = txtCatalog.Split(new string[] { "--" }, StringSplitOptions.None);

                    if (parts.Length > 1)
                    {
                        fullName = parts[1].Trim().ToString();
                    }
                }
                else
                {
                    fullName = "Empleado Nuevo";
                }

                _voucher = new FoodVaucher
                {
                    Id = 0,
                    PayrollId = 1,
                    Payweek = 0,
                    NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                    FullName = fullName,
                    BusinessDays = Convert.ToInt32(numericUpDown1.Value),
                    WorkedDays = Convert.ToInt32(numericUpDown2.Value),
                    Amount = numericUpDown3.Value
                };

                for (int i = 1; i <= 31; i++)
                {
                    Control[] found = GroupBox1.Controls.Find($"CheckBox{i}", true);
                    if (found.Length > 0 && found[0] is CheckBox chk)
                    {
                        var propertyInfo = _voucher.GetType().GetProperty($"D{i}");
                        if (propertyInfo != null)
                        {
                            byte nuevoValor = chk.Checked ? (byte)8 : (byte)0;
                            propertyInfo.SetValue(_voucher, nuevoValor);
                        }
                    }
                }

                MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.Yes;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            if (_voucher == null)
            {
                MessageBox.Show("No hay ningún empleado seleccionado para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    _voucher.NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue);
                    _voucher.BusinessDays = Convert.ToInt32(numericUpDown1.Value);
                    _voucher.WorkedDays = Convert.ToInt32(numericUpDown2.Value);
                    _voucher.Amount = numericUpDown3.Value;

                    for (int i = 1; i <= 31; i++)
                    {
                        Control[] found = GroupBox1.Controls.Find($"CheckBox{i}", true);
                        if (found.Length > 0 && found[0] is CheckBox chk)
                        {
                            var propertyInfo = _voucher.GetType().GetProperty($"D{i}");
                            if (propertyInfo != null)
                            {
                                byte valorOriginal = (byte)propertyInfo.GetValue(_voucher);
                                byte nuevoValor = chk.Checked ? (valorOriginal > 0 ? valorOriginal : (byte)8) : (byte)0;

                                propertyInfo.SetValue(_voucher, nuevoValor);
                            }
                        }
                    }
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    Close();

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

        public FoodVaucher GetCreatedVoucher()
        {
            return _voucher;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            decimal res = (_repository.GetImportFoodVaucher() / numericUpDown1.Value) * numericUpDown2.Value;
            numericUpDown3.Value = res > _repository.GetImportFoodVaucher() ? _repository.GetImportFoodVaucher() : res;
        }
    }
}
