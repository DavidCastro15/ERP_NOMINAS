using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.SupportTransportations;
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
    public partial class SupportTransportationShow : BaseForm
    {
        private Utilities Util = new Utilities();
        private SupportTransportationRepository _repository = new SupportTransportationRepository();
        private SupportTransportation _transport;
        private SupportTransportationMain _loadMain;
        public bool Edit;

        public SupportTransportationShow(SupportTransportationMain loadMain, SupportTransportation transport)
        {
            InitializeComponent();
            _loadMain = loadMain;
            _transport = transport;
        }

        private void SupportTransportationShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                searchCatalog1.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                searchCatalog1.SelectedValue = _transport.NumberEmployee;
                numericUpDown1.Value = _transport.WorkedDays;
                numericUpDown2.Value = _transport.PorcAnt;
                numericUpDown3.Value = _transport.PorcAct;
                numericUpDown4.Value = _transport.Amount;

                for (int i = 1; i <= 31; i++)
                {
                    Control[] found = GroupBox1.Controls.Find($"CheckBox{i}", true);

                    if (found.Length > 0 && found[0] is CheckBox chk)
                    {
                        var propertyInfo = _transport.GetType().GetProperty($"D{i}");
                        if (propertyInfo != null)
                        {
                            byte valor = (byte)propertyInfo.GetValue(_transport);

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

                _transport = new SupportTransportation
                {
                    Id = 0,
                    PayrollId = 1,
                    Payweek = 0,
                    NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue),
                    FullName = fullName,
                    WorkedDays = Convert.ToInt32(numericUpDown1.Value),
                    PorcAnt = Convert.ToDecimal(numericUpDown2.Value),
                    PorcAct = Convert.ToDecimal(numericUpDown3.Value),
                    Amount = Convert.ToDecimal(numericUpDown4.Value)
            };

                for (int i = 1; i <= 31; i++)
                {
                    Control[] found = GroupBox1.Controls.Find($"CheckBox{i}", true);
                    if (found.Length > 0 && found[0] is CheckBox chk)
                    {
                        var propertyInfo = _transport.GetType().GetProperty($"D{i}");
                        if (propertyInfo != null)
                        {
                            byte newVal = chk.Checked ? (byte)8 : (byte)0;
                            propertyInfo.SetValue(_transport, newVal);
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
            if (_transport == null)
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
                    _transport.NumberEmployee = Convert.ToInt32(searchCatalog1.SelectedValue);
                    _transport.WorkedDays = Convert.ToInt32(numericUpDown1.Value);
                    _transport.PorcAnt = Convert.ToDecimal(numericUpDown2.Value);
                    _transport.PorcAct = Convert.ToDecimal(numericUpDown3.Value);
                    _transport.Amount = Convert.ToDecimal(numericUpDown4.Value);

                    for (int i = 1; i <= 31; i++)
                    {
                        Control[] found = GroupBox1.Controls.Find($"CheckBox{i}", true);
                        if (found.Length > 0 && found[0] is CheckBox chk)
                        {
                            var propertyInfo = _transport.GetType().GetProperty($"D{i}");
                            if (propertyInfo != null)
                            {
                                byte valueOriginal = (byte)propertyInfo.GetValue(_transport);
                                byte newValue = chk.Checked ? (valueOriginal > 0 ? valueOriginal : (byte)8) : (byte)0;

                                propertyInfo.SetValue(_transport, newValue);
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

        public SupportTransportation GetCreatedTransport()
        {
            return _transport;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            decimal res = (numericUpDown1.Value * numericUpDown3.Value) * _repository.GetImportTransport();
            numericUpDown4.Value = res;     
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal res = (numericUpDown1.Value * numericUpDown3.Value) * _repository.GetImportTransport();
            numericUpDown4.Value = res;
        }
    }
}
