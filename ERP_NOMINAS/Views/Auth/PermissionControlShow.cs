using ERP_NOMINAS.Models.Auth;
using ERP_NOMINAS.Repositorys.Auth;
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

namespace ERP_NOMINAS.Views.Auth
{
    public partial class PermissionControlShow : BaseForm
    {
        private ControlPermissionRepository _repository = new ControlPermissionRepository();
        private Utilities Util = new Utilities();
        private PermissionControlMain _loadMain;
        public int Id;
        public bool Edit;
        private List<Catalog> listCatalogBd = new List<Catalog>();

        public PermissionControlShow(PermissionControlMain loadMain)
        {
            InitializeComponent();
            _loadMain = loadMain;
        }

        private void PermissionControlShow_Load(object sender, EventArgs e)
        {
            listCatalogBd = _repository.GetCatalogs();
            kitCrud1.VisibleBotonCrud(true);
            if (Edit)
            {
                kitCrud1.VisibleBotonCrud(false);

                ControlPermission res = _repository.GetControlPermission(Id);
                textBox1.Text = res.NameUser;
                textBox2.Text = res.Password;
                radioButton1.Checked = res.IdRole == 1 ? true : false;
                radioButton2.Checked = res.IdRole == 2 ? true : false;

                if (res.Catalogs != null && res.Catalogs.Count > 0)
                {
                    AsignarPermisosALosCheckboxes(this, res.Catalogs);
                }
            }
        }

        private void AsignarPermisosALosCheckboxes(Control contenedorPadre, List<Catalog> catalogosPermitidos)
        {
            foreach (Control control in contenedorPadre.Controls)
            {
                if (control is CheckBox checkbox)
                {
                    bool tienePermiso = catalogosPermitidos.Any(c => c.Name.Trim().Equals(checkbox.Text.Trim(), StringComparison.OrdinalIgnoreCase));

                    checkbox.Checked = tienePermiso;
                }

                if (control.HasChildren)
                {
                    AsignarPermisosALosCheckboxes(control, catalogosPermitidos);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                CheckedCheckBox(this, true);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                CheckedCheckBox(this, false);
            }
        }

        private void CheckedCheckBox(Control conte, bool stateNew)
        {
            foreach (Control control in conte.Controls)
            {
                if (control is CheckBox checkbox)
                {
                    checkbox.Checked = stateNew;
                }

                if (control.HasChildren)
                {
                    CheckedCheckBox(control, stateNew);
                }
            }
        }

        private void GetCatalogsChecked(Control container, List<Catalog> listFinal)
        {
            foreach (Control c in container.Controls)
            {
                if (c is CheckBox checkbox && checkbox.Checked)
                {
                    string textCheckbox = checkbox.Text.Trim();

                    var catalogFind = listCatalogBd.FirstOrDefault(db => db.Name.Equals(textCheckbox, StringComparison.OrdinalIgnoreCase));

                    if (catalogFind != null)
                    {
                        listFinal.Add(new Catalog
                        {
                            Id = catalogFind.Id, 
                            Name = catalogFind.Name
                        });
                    }
                }

                if (c.HasChildren)
                {
                    GetCatalogsChecked(c, listFinal);
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
                    var ctrlp = ShowPermission();
                    GetCatalogsChecked(this, ctrlp.Catalogs);
                    var res = _repository.CreateUser(ctrlp);
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
                    var ctrlp = ShowPermission();
                    GetCatalogsChecked(this, ctrlp.Catalogs);
                    var res = _repository.UpdateUser(ctrlp);
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

        private ControlPermission ShowPermission()
        {
            return new ControlPermission
            {

                IdUser = this.Id,
                NameUser = textBox1.Text,
                Password = textBox2.Text,
                IdRole = radioButton1.Checked ? 1 : 2,
                Catalogs = new List<Catalog>()
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckedCheckBox(this, true);
        }
    }
}
