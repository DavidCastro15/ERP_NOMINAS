using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Employees;
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
using static ERP_NOMINAS.GlobalFunctions.Utilities;

namespace ERP_NOMINAS.Views.Master.Employees
{
    public partial class EmployeeShow : BaseForm
    {
        Utilities Util = new Utilities();
        EmployeeRepository _repository = new EmployeeRepository();

        private EmployeeMain _EmployeeMain;
        public int IdEmployee = 0;
        public bool Edit = false;

        public EmployeeShow(EmployeeMain employeeMain)
        {
            InitializeComponent();
            _EmployeeMain = employeeMain;
        }

        private void EmployeeShow_Load(object sender, EventArgs e)
        {
           
            kitCrud1.VisibleBotonCrud(true);    

            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                Employee em = _repository.GetEmployee(IdEmployee);
                textBox1.Text =Convert.ToString(em.NumberEmployee);
                textBox2.Text =Convert.ToString(em.NumberCredential);
                textBox3.Text =Convert.ToString(em.Name);
                textBox4.Text =Convert.ToString(em.LastnameFather);
                textBox5.Text =Convert.ToString(em.LastnameMother);
                textBox6.Text =Convert.ToString(em.RFC);
                textBox7.Text =Convert.ToString(em.Imss);
                textBox10.Text = Convert.ToString(em.CURP);
                
                foreach (RadioButton rb in groupBox1.Controls.OfType<RadioButton>())
                {
                    rb.Checked = (rb.Text.Trim() == em.Type.Trim());
                }
                foreach (RadioButton rb in groupBox2.Controls.OfType<RadioButton>())
                {
                    rb.Checked = (rb.Text.Trim() == em.Classification.Trim());
                }
                foreach (RadioButton rb in groupBox8.Controls.OfType<RadioButton>())
                {
                    rb.Checked = (rb.Text.Trim() == em.Area.Trim());
                }

                radioButton8.Checked = em.Sex == "Masculino" ? true : false;
                radioButton9.Checked = em.Sex == "Femenino" ? true : false;
                radioButton10.Checked = em.MaritalStatus == "Solter@" ? true : false;
                radioButton11.Checked = em.MaritalStatus == "Casad@" ? true : false;
                radioButton12.Checked = em.Status == "Activo" ? true : false;
                radioButton13.Checked = em.Status == "Inactivo" ? true : false;
                radioButton14.Checked = em.Absenteeism == "Zafra" ? true : false;
                radioButton15.Checked = em.Absenteeism == "Reparación" ? true : false;
                radioButton21.Checked = em.WorkedSundayHarvest == 1 ? true : false;
                radioButton22.Checked = em.WorkedSundayHarvest == 0 ? true : false;
                radioButton23.Checked = em.ApplyUnionDues == 1 ? true : false;
                radioButton24.Checked = em.ApplyUnionDues == 0 ? true : false;
                searchCatalog1.SelectedValue = em.CategoryHarvest;
                searchCatalog2.SelectedValue = em.CategoryRepair;
                textBox13.Text = em.PlaceBirth;
                dateTimePicker1.Value = em.DateBirth;
                dateTimePicker2.Value = em.StatusDate;
                textBox15.Text = em.FatherName;
                textBox16.Text = em.MotherName;
                textBox17.Text = em.Address;
                textBox18.Text = em.Cologne;
                textBox19.Text = em.Profession;
                numericUpDown1.Value =Convert.ToInt32( em.ZipCode);
                textBox20.Text = em.State;
                textBox21.Text = em.Municipality;
                textBox22.Text = em.City;
                textBox23.Text = em.CellPhone;
                textBox24.Text = em.Schooling;
                dateTimePicker3.Value = em.DatePlaza;
                textBox25.Text = em.AccountBank;

            }
        }

        private void kitCrud1_Edit(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea editar este registro?";
            string title = "Confirmar Edicion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Employee emp = ShowEmployee();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateEmployee = _repository.UpdateEmployee(emp);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _EmployeeMain.LoadGrid();

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
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            try
            {
                if (ress == DialogResult.Yes)
                {

                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Escriba el numero del empleado");
                        return;
                    }
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("Escriba la credencial del empleado");
                        return;
                    }
                    Employee emp = ShowEmployee();
                    _repository.CreateEmployee(emp);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _EmployeeMain.LoadGrid();
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

        private Employee ShowEmployee()
        {
            var _type = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var _classification = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var _area = groupBox8.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            return new Employee
            {
                Id = Convert.ToInt32(IdEmployee),
                PayrollId = Convert.ToInt32(1),
                NumberEmployee = Convert.ToInt64(textBox1.Text),
                NumberCredential = Convert.ToInt32(textBox2.Text),
                Name = Convert.ToString(textBox3.Text),
                LastnameFather = Convert.ToString(textBox4.Text),
                LastnameMother = Convert.ToString(textBox5.Text),
                RFC = Convert.ToString(textBox6.Text),
                Imss = Convert.ToString(textBox7.Text),
                Module18 = Convert.ToInt32(0),
                CategoryHarvest = Convert.ToInt32(searchCatalog1.SelectedValue),
                CategoryRepair = Convert.ToInt32(searchCatalog2.SelectedValue),
                Status = Convert.ToString(radioButton12.Checked == true ? "Activo" : "Inactivo"),
                StatusDate = Convert.ToDateTime(dateTimePicker2.Value),
                Type = Convert.ToString(_type.Text),
                Classification = Convert.ToString(_classification.Text),
                Area = Convert.ToString(_area.Text),
                Sex = Convert.ToString(radioButton8.Checked == true ? "Masculino" : "Femenino"),
                MaritalStatus = Convert.ToString(radioButton10.Checked == true ? "Solter@" : "Casad@"),
                PlaceBirth = Convert.ToString(textBox13.Text),
                DateBirth = Convert.ToDateTime(dateTimePicker1.Value),
                FatherName = Convert.ToString(textBox15.Text),
                MotherName = Convert.ToString(textBox16.Text),
                Address = Convert.ToString(textBox17.Text),
                Cologne = Convert.ToString(textBox18.Text),
                ZipCode = Convert.ToString(numericUpDown1.Value),
                City = Convert.ToString(textBox22.Text),
                Municipality = Convert.ToString(textBox21.Text),
                State = Convert.ToString(textBox20.Text),
                CellPhone = Convert.ToString(textBox23.Text),
                Schooling = Convert.ToString(textBox24.Text),
                Profession = Convert.ToString(textBox19.Text),
                YearRepair = Convert.ToInt32(numericUpDown7.Value),
                YearHarvest = Convert.ToInt32(numericUpDown8.Value),
                BonusDaysRepair = Convert.ToInt32(numericUpDown2.Value),
                BonusDaysHarvest = Convert.ToInt32(numericUpDown3.Value),
                VacationDaysRepair = Convert.ToInt32(numericUpDown4.Value),
                VacationDaysHarvest = Convert.ToInt32(numericUpDown5.Value),
                DaysCommission = Convert.ToInt32(0),
                Declare = Convert.ToString("0"),
                Photo = Convert.ToString("0"),
                Absenteeism = Convert.ToString(radioButton14.Checked == true ? "Zafra" : "Reparación"),
                CURP = Convert.ToString(textBox10.Text),
                Pattern = Convert.ToString("0"),
                PresenceDaysPrevious = Convert.ToInt32(0),
                PunctualityDaysPrevious = Convert.ToInt32(0),
                PresenceDaysCurrent = Convert.ToInt32(0),
                PunctualityDaysCurrent = Convert.ToInt32(0),
                WorkedSundayHarvest = Convert.ToInt32(0),
                AccountBank = Convert.ToString(textBox25.Text),
                InterbankKey = Convert.ToString("0"),
                ApplyUnionDues = Convert.ToInt32(radioButton23.Checked == true ? 1 : 0),
                DatePlaza = Convert.ToDateTime(dateTimePicker3.Value)

            };
        }
    }
}
