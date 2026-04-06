using ERP_NOMINAS.GlobalFunctions;
using ERP_NOMINAS.Models.Materials;
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

namespace ERP_NOMINAS.Views.Master.Materials
{
    public partial class MaterialShow : BaseForm
    {
        Utilities Util = new Utilities();
        MaterialRepository _repository = new MaterialRepository();
        private MaterialMain _materialMain;
        public int IdMaterial = 0;
        public bool Edit = false;

        public MaterialShow(MaterialMain materialMain)
        {
            InitializeComponent();
            _materialMain = materialMain;
        }

        private void MaterialShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                Material m = _repository.GetMaterial(IdMaterial);

                textBox1.Text = m._Material;
                numericUpDown1.Value = m.PayTon;
            }
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Material m = ShowMaterial();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var createMaterial = _repository.CreateMaterial(m);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _materialMain.LoadGrid();
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
            Material m = ShowMaterial();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateMaterial = _repository.UpdateMaterial(m);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _materialMain.LoadGrid();

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

        private Material ShowMaterial()
        {
            return new Material
            {
                Id = Convert.ToInt32(IdMaterial),
                _Material = Convert.ToString(textBox1.Text),
                PayTon = Convert.ToDecimal(numericUpDown1.Value)
            };
        }
    }
}
