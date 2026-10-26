using ERP_NOMINAS.Views.PayRollProcess.CalculationsPayRoll.TypePayRoll.UnionWorkers;
using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.PayRollProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Views.PayRollProcess.CalculationsPayRoll
{
    public partial class CalculatedPayRollShow : BaseForm
    {
        private Utilities Util = new Utilities();
        public CalculatedPayRollShow()
        {
            InitializeComponent();
        }

        private void CalculatedPayRollShow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;

            string _cycle = selectCycle1.SelectValue.ToString();
            int _payWeek = Convert.ToInt32(Util.PayWeekNow(selectPayWeek1.PayWeek, selectPayWeek1.PayWeekType));
            DateTime _startDate = dateRange1.StartDate;
            DateTime _endDate = dateRange1.EndDate;
            int _efectiveDays = 0;
            int _noWorkingDays = 0;
            int _nominaExtra = 0;
            listView1.Items.Clear();
            Cursor.Current = Cursors.WaitCursor;
            //btnProcesar.Enabled = false;
                
            try
            {
                // 1. Declarar la lista al inicio del bloque try
                List<MethodResult> resultados = null;
                IPayrollProcessor processor = null;

                // 2. Factory logic: instanciar la estrategia en base a la nómina y el ciclo seleccionados.
                // En el futuro, se puede evaluar también el tipo de nómina (ej. "Obreros", "Empleados", etc.) si está en un combobox.
                switch (_cycle.ToLower())
                {
                    case "reparacion":
                    case "reparación":
                        processor = new ProcessRepairUW();
                        break;

                    case "zafra":
                        processor = new ProcessZafraUW();
                        break;

                    default:
                        MessageBox.Show($"El ciclo '{_cycle}' no tiene un proceso de cálculo configurado para esta nómina.",
                                        "Ciclo No Soportado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; 
                }

                // 3. Ejecutar el procesamiento de forma polimórfica.
                resultados = processor.Process(_cycle, _payWeek, _startDate, _endDate, _noWorkingDays, _efectiveDays, _nominaExtra);

                foreach (var res in resultados)
                {
                    ListViewItem item = new ListViewItem(res.MethodName);

                    if (res.IsSuccess)
                    {
                        item.SubItems.Add("✔ Correcto");
                        item.SubItems.Add("Ejecutado sin errores en la base de datos.");
                          
                        item.ForeColor = Color.DarkGreen;
                    }
                    else
                    {
                        item.SubItems.Add("❌ Falló");
                        item.SubItems.Add(res.ErrorMessage); 

                        item.ForeColor = Color.Red;
                    }

                    listView1.Items.Add(item);
                }

                bool erros = resultados.Exists(r => !r.IsSuccess);
                if (erros) 
                {
                    MessageBox.Show("El proceso concluyó, pero se detectaron errores en algunos conceptos. Revisa la lista en pantalla.",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("¡Proceso de percepciones terminado con éxito total!",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico en el motor de procesamiento: {ex.Message}",
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                //btnProcesar.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itemSeleccionado = listView1.SelectedItems[0];

                if (itemSeleccionado.Tag is MethodResult resultado)
                {
                    if (!resultado.IsSuccess)
                    {
                        MessageBox.Show($"Método: {resultado.MethodName}\n\nDetalle completo del error:\n{resultado.ErrorMessage}",
                                        "Detalle del Fallo SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
