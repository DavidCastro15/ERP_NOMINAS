using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.IntegratedSalaries;
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

namespace ERP_NOMINAS.Views.Master.IntegratedSalaries
{
    public partial class IntegratedSalaryMain : BaseForm
    {
        Utilities Util = new Utilities();
        IntegratedSalaryRepository _repository = new IntegratedSalaryRepository();
        private int IdintegratedSalary = 0;
        private int IdEmployee = 0;

        public IntegratedSalaryMain()
        {
            InitializeComponent();
            filterByT1.FilterBy = (text) =>
            {
                // 1. Usar string.IsNullOrWhiteSpace es más seguro que ""
                if (string.IsNullOrWhiteSpace(text))
                {
                    LoadGrid();
                    return;
                }

                // 2. TryParse evita que la app se cierre si escriben letras
                if (int.TryParse(text, out int id))
                {
                    var res = _repository.FilterByNumberEmployee(id);

                    // 3. Evitamos el NullReferenceException si el repo devuelve null
                    // y refrescamos el DataSource de forma eficiente
                    dataGridView1.DataSource = new BindingList<IntegratedSalary>(res ?? new List<IntegratedSalary>());
                }
                else
                {
                    // Opcional: Si no es un número, podrías limpiar el grid o no hacer nada
                    dataGridView1.DataSource = new BindingList<IntegratedSalary>();
                }
            };
        }

        private void IntegratedSalaryMain_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "El .CSV solo ingresar datos en este formato id_empleado,salario_integrado_imss,salario_integrado_infonavit");
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListIntegratedSalary = _repository.GetIntegratedSalaries();
            Util.ConfigGrid<IntegratedSalary>(dataGridView1);
            dataGridView1.DataSource = new BindingList<IntegratedSalary>(ListIntegratedSalary);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdintegratedSalary = Convert.ToInt32(row.Cells[0].Value);
                IdEmployee = Convert.ToInt32(row.Cells[1].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametersEdit();
        }

        private void parametersEdit()
        {

            if (IdintegratedSalary <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var integratedShow = new IntegratedSalaryShow(this);
            integratedShow.IdIntegratedSalary = IdintegratedSalary;
            integratedShow.Edit = true;
            integratedShow.ShowDialog();

        }

        private void kitForm1_Add(object sender, EventArgs e)
        {
            var integratedShow = new IntegratedSalaryShow(this);
            integratedShow.ShowDialog();
        }

        private void kitForm1_MEdit(object sender, EventArgs e)
        {
            parametersEdit();
        }

        private void kitForm1_MDelete(object sender, EventArgs e)
        {
            if (IdintegratedSalary <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea eliminar este registro?";
            string title = "Confirmar Eliminacion";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                var deleteIntegrated = _repository.DeleteIntegratedSalary(IdintegratedSalary);
                MessageBox.Show("Registro eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IdintegratedSalary <= 0)
            {
                MessageBox.Show("Seleccione un empleado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string message = "¿Está seguro de que desea inhabilitar este empleado?";
            string title = "Confirmar";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                var disabledEmployee = _repository.StatusEmployee(true,IdEmployee);
                MessageBox.Show("Empleado inhabilitado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            openFileDialog1 = new OpenFileDialog { Filter = "CSV Files|*.csv" };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox2.Text = openFileDialog1.FileName;                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void buttonExport1_Load(object sender, EventArgs e)
        {
            try
            {
                // Obtener datos del repositorio
                var data = _repository.GetDataPrintIntegratedSalary();

                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("No hay información para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                    sfd.FileName = $"Reporte_Salarios_Integrados_{DateTime.Now:ddMMyyyy}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // Llamar al método del repositorio
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

        private void buttonImport1_OnBotonImportClick(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que importar este archivo? \nTodos los registros anteriores seran borrados!!!!!!!";
            string title = "Importar Salarios Integrados";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ress == DialogResult.Yes)
            {
                try
                {
                    DataTable dt = Util.GetDataCSV(openFileDialog1.FileName);
                    _repository.ImportEmployees(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                MessageBox.Show("Importacion masiva con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
