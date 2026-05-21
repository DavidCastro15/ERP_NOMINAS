using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Category;
using ERP_NOMINAS.Repositorys;
using ERP_NOMINAS.Views;
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

namespace ERP_NOMINAS
{
    public partial class Form1 : BaseForm
    {
        Utilities Util = new Utilities();
        CategoryRepository _repository = new CategoryRepository();

        public Form1()
        {
            InitializeComponent();
            //ApplyDesingGrid(dataGridView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }

        public void LoadGrid()
        {
            var ListCategories = _repository.GetCategories();
            Util.ConfigGrid<Category>(dataGridView1);
            dataGridView1.DataSource = new BindingList<Category>(ListCategories);
        }

        public void ApplyDesingGrid(DataGridView dgv)
        {
            // Fuentes y Tipografía (Estilo font-sans de Tailwind)
            dgv.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.BackgroundColor = Color.White; // bg-white
            dgv.BorderStyle = BorderStyle.None;

            // Bordes limpios estilo border-slate-200
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240); // slate-200

            // Configuración de Comportamiento e Interfaz moderna
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Alturas generosas tipo padding py-3 y py-4 de Tailwind
            dgv.RowTemplate.Height = 44;       // Filas más altas y delgadas
            dgv.ColumnHeadersHeight = 40;     // Cabecera estilizada

            // 1. Encabezados Estilo Tailwind (bg-slate-50 / text-slate-500 / font-semibold)
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(248, 250, 252); // slate-50
            headerStyle.ForeColor = Color.FromArgb(100, 116, 139); // slate-500
            headerStyle.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;

            // 2. Estilo de Filas de Datos (text-slate-700 / bg-white)
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.BackColor = Color.White;
            rowStyle.ForeColor = Color.FromArgb(51, 65, 85); // slate-700

            // SELECCIÓN MODERNA: Paleta Tailwind 'sky'
            rowStyle.SelectionBackColor = Color.FromArgb(240, 249, 255); // sky-50 (Celeste pastel ultra claro)
            rowStyle.SelectionForeColor = Color.FromArgb(3, 105, 161);   // sky-700 (Azul corporativo legible)
            dgv.DefaultCellStyle = rowStyle;

            // 3. Filas Alternas (bg-slate-50/30 para un contraste mínimo pero limpio)
            DataGridViewCellStyle altRowStyle = new DataGridViewCellStyle();
            altRowStyle.BackColor = Color.FromArgb(252, 254, 255);

            // Mantiene el mismo enfoque sky al seleccionar filas alternas
            altRowStyle.SelectionBackColor = Color.FromArgb(240, 249, 255); // sky-50
            altRowStyle.SelectionForeColor = Color.FromArgb(3, 105, 161);   // sky-700
            dgv.AlternatingRowsDefaultCellStyle = altRowStyle;
        }
    }
}
