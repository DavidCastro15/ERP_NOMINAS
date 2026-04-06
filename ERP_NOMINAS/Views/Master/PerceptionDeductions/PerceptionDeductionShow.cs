using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.PerceptionDeduction;
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

namespace ERP_NOMINAS.Views.Master.PerceptionDeductions
{
    public partial class PerceptionDeductionShow : BaseForm
    {
        Utilities utilities = new Utilities();
        PerceptionDeductionRepository _repository = new PerceptionDeductionRepository();
        public int IdPd;
        public bool Edit;

        private PerceptionDeductionMain _pd = new PerceptionDeductionMain();

        public PerceptionDeductionShow(PerceptionDeductionMain pd)
        {
            InitializeComponent();
            _pd = pd;
        }

        private void PerceptionDeductionShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);
            numericUpDown1.Value = _repository.CheckNextId();

            if (Edit)
            {
                try
                {
                    kitCrud1.VisibleBotonCrud(false);
                    PerceptDeduction pd = _repository.GetPerceptionDeduction(IdPd);
                    numericUpDown1.Value = pd.IdConcept;
                    textBox1.Text = pd.NameConcept;
                    textBox2.Text = pd.Accumulate;
                    radioButton1.Checked = pd.Type == "Percepción" ?  true : false;
                    radioButton2.Checked = pd.Type == "Deducción" ? true : false;
                    radioButton3.Checked = pd.Status == "Activo" ? true : false;
                    radioButton4.Checked = pd.Status == "Cancelado" ? true : false;
                    checkBox1.Checked = pd.Apply1 == 1 ? true : false;                    
                    checkBox2.Checked = pd.Apply2 == 1 ? true : false;                    
                    checkBox3.Checked = pd.Apply3 == 1 ? true : false;                    
                    checkBox4.Checked = pd.Apply4 == 1 ? true : false;                    
                    checkBox5.Checked = pd.Apply5 == 1 ? true : false;                    
                    checkBox6.Checked = pd.Apply6 == 1 ? true : false;                    
                    checkBox7.Checked = pd.Apply7 == 1 ? true : false;                    
                    checkBox8.Checked = pd.Apply8 == 1 ? true : false;                    
                    checkBox9.Checked = pd.Apply9 == 1 ? true : false;                    
                    checkBox10.Checked = pd.Apply10 == 1 ? true : false;                    

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
      
        private void numericUpDown1_Click(object sender, EventArgs e)
        {
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Focus();
        }
      
        private PerceptDeduction ShowDataGrid(string type, string status)
        {
            return new PerceptDeduction
            {
                Id = Convert.ToInt32(IdPd),
                Type = Convert.ToString(type),
                IdConcept = Convert.ToInt32(numericUpDown1.Value),
                Status = Convert.ToString(status),
                NameConcept = Convert.ToString(textBox1.Text),
                Accumulate = textBox2.Text == "" ? "No" : textBox2.Text,
                Apply1 = Convert.ToInt32(checkBox1.Checked),
                Apply2 = Convert.ToInt32(checkBox2.Checked),
                Apply3 = Convert.ToInt32(checkBox3.Checked),
                Apply4 = Convert.ToInt32(checkBox4.Checked),
                Apply5 = Convert.ToInt32(checkBox5.Checked),
                Apply6 = Convert.ToInt32(checkBox6.Checked),
                Apply7 = Convert.ToInt32(checkBox7.Checked),
                Apply8 = Convert.ToInt32(checkBox8.Checked),
                Apply9 = Convert.ToInt32(checkBox9.Checked),
                Apply10 = Convert.ToInt32(checkBox10.Checked),
                Order = 0,
                GraParcExe = 0
            };
        }

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string type = radioButton1.Checked ? "Percepción" : "Deducción";
            string status = radioButton3.Checked ? "Activo" : "Cancelado";
            PerceptDeduction pd = ShowDataGrid(type, status);

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updatePd = _repository.UpdatePerceptionDeduction(pd);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _pd.LoadGrid();
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

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            string type = radioButton1.Checked ? "Percepción" : "Deducción";
            string status = radioButton3.Checked ? "Activo" : "Cancelado";

            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            PerceptDeduction pd = ShowDataGrid(type, status);

            try
            {
                if (ress == DialogResult.Yes)
                {

                    bool idExists = _repository.ExistsId(Convert.ToInt32(pd.IdConcept));
                    if (idExists)
                    {
                        MessageBox.Show("Concepto ya existente !!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    var createPd = _repository.CreatePerceptionDeduction(pd);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _pd.LoadGrid();
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
