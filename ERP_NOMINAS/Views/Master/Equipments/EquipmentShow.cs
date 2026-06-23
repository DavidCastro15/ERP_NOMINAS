using ERP_SHARED.GlobalFunctions;
using ERP_NOMINAS.Models.Equipments;
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

namespace ERP_NOMINAS.Views.Master.Equipments
{
    public partial class EquipmentShow : BaseForm
    {
        private Utilities Util = new Utilities();
        private EquipmentRepository _repository = new EquipmentRepository();
        private EquipmentMain _equipLoad;
        public bool Edit;
        public int Id;

        public EquipmentShow(EquipmentMain equipmentMain)
        {
            InitializeComponent();
            _equipLoad = equipmentMain;
        }

        private void EquipmentShow_Load(object sender, EventArgs e)
        {
            kitCrud1.VisibleBotonCrud(true);

            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);
                var res = _repository.GetEquipment(Id);
                textBox1.Text = Convert.ToString(res.Equip);
                textBox2.Text = Convert.ToString(res.Description);
                searchCatalog1.SelectedValue = Convert.ToInt32(res.Group);
            }
        }

        private void kitCrud1_Save(object sender, EventArgs e)
        {
            string message = "¿Está seguro de que  desea guardar este registro?";
            string title = "Confirmar Guardado";
            DialogResult ress = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Equipment eq = ShowEquipment();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var checkEx = _repository.ExistsEquipment(eq.Equip);
                    if (checkEx)
                    {
                        MessageBox.Show("Este equipo ya existe, valide su informacion", "Equipo existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var createGroup = _repository.CreateEquipment(eq);
                    MessageBox.Show("Registro guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _equipLoad.LoadGrid();
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
            Equipment gr = ShowEquipment();

            try
            {
                if (ress == DialogResult.Yes)
                {
                    var updateGroup = _repository.UpdateEquipment(gr);
                    MessageBox.Show("Registro editado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    _equipLoad.LoadGrid();

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

        private Equipment ShowEquipment()
        {
            return new Equipment
            {
                Id = Convert.ToInt32(Id),
                Equip = Convert.ToInt32(textBox1.Text),
                Description = Convert.ToString(textBox2.Text),
                Group = Convert.ToInt32(searchCatalog1.SelectedValue)
            };
        }
    }
}
