using ERP_NOMINAS.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.GlobalFunctions
{
    public class Utilities
    {
        ConnectorSql Conex = new ConnectorSql();
        readonly SqlCommand cmd = new SqlCommand();

        public enum TypeCatalog
        {
            Employee,
            Use,
            Category,
            Turn,
            Concept,
            Department,
            Management,
            Group,
            Equipment,
            TrainedCategories,
            Material
        }
       
        public void ConfigGrid<T>(DataGridView dgv)
        {       
            PropertyInfo[] propiedades = typeof(T).GetProperties();

            for (int i = 0; i < propiedades.Length; i++)
            {              
                if (i < dgv.Columns.Count)
                {
                    dgv.Columns[i].DataPropertyName = propiedades[i].Name;
               
                }
            }
        }

        private void LoadComboBox(ComboBox combo, string query, string display, string value)
        {
            // SI LA QUERY ESTÁ VACÍA, SALIMOS PARA EVITAR EL ERROR
            if (string.IsNullOrEmpty(query))
            {
                combo.DataSource = null;
                return;
            }

            try
            {
                if (Conex.nomi.State != ConnectionState.Open) Conex.OpenNomina();

                SqlDataAdapter da = new SqlDataAdapter(query, Conex.nomi);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (da != null && dt.Rows.Count > 0)
                {

                    // Configuramos el ComboBox
                    combo.DataSource = dt;
                    combo.DisplayMember = display; // Nombre de la columna a mostrar
                    combo.ValueMember = value;     // Nombre de la columna con el ID real

                    // Opcional: Deseleccionar el primer elemento al cargar
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.DataSource = null;
                    combo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }



            //Conex.OpenNomina();
            //try
            //{
            //    SqlDataAdapter da = new SqlDataAdapter(query, Conex.nomi);
               

             
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al cargar datos: " + ex.Message);
            //}

        }

        public void LoadTypeCombo(ComboBox combo, TypeCatalog type)
        {
            string query = "";
            string display = "";
            string value = "";

            switch (type)
            {
                case TypeCatalog.Employee:
                    query = "SELECT numero_empleado, CONCAT(numero_empleado,'--',nombre,' ',apellido_paterno,' ', apellido_materno) as nombre_completo " +
                            "FROM empleados WHERE nombre <> '' AND numero_empleado <> 42 AND estatus = 'Activo' ORDER BY numero_empleado ASC";
                    display = "nombre_completo";
                    value = "numero_empleado";
                    break;

                case TypeCatalog.Use:
                    query = "SELECT u.uso, CONCAT(u.uso,'--',u.descripcion,'--',d.nombre) as descripcion " +
                            "FROM usos u INNER JOIN departamentos d ON u.departamento = d.id_deparamento";
                    display = "descripcion";
                    value = "uso";
                    break;

                case TypeCatalog.Category:
                    query = "SELECT id_categoria, CONCAT(id_categoria,'--',nombre_categoria) AS n_categoria " +
                            "FROM categorias ORDER BY id_categoria";
                    display = "n_categoria";
                    value = "id_categoria";
                    break;

                case TypeCatalog.Turn:
                    query = "SELECT turno FROM turnos";
                    display = "turno";
                    value = "turno";
                    break;
                case TypeCatalog.Concept:
                    query = "SELECT id_concepto, CONCAT(id_concepto,'--',nombre_concepto) AS n_concepto " +
                        " FROM conceptos_pd";
                    display = "n_concepto";
                    value = "id_concepto";
                    break;
                case TypeCatalog.Equipment:
                    query = " SELECT equipo, CONCAT(equipo,'--',descripcion) AS equipos " +
                            $" FROM equipos ORDER BY equipo ASC ";
                    display = "equipos";
                    value = "equipo";
                    break;

                case TypeCatalog.Management:
                    query = " SELECT gerencia, CONCAT(nombre,'--',responsable) AS nombres " +
                            $" FROM gerencias ORDER BY nombre ASC ";
                    display = "nombres";
                    value = "gerencia";
                    break;

                case TypeCatalog.Group:
                    query = " SELECT grupo, CONCAT(grupo,'--',descripcion) AS grupos " +
                            $" FROM grupos ORDER BY grupo ASC ";
                    display = "grupos";
                    value = "grupo";
                    break;

                case TypeCatalog.Department:
                    query = " SELECT id_deparamento, CONCAT(id_deparamento,'--',nombre) AS departamentos " +
                            $" FROM departamentos ORDER BY nombre ASC ";
                    display = "departamentos";
                    value = "id_deparamento";
                    break;

                case TypeCatalog.Material:
                    query = "SELECT id,material FROM materiales ";
                    display = "material";
                    value = "material";
                    break;
            }

            LoadComboBox(combo, query, display, value);
        }

        public void LoadTypeComboFilter(ComboBox combo, TypeCatalog type,string param,int nEmployee = 0)
        {
            string query = "";
            string display = "";
            string value = "";
            string filtro = (param ?? "").Trim();
            switch (type)
            {
                //case TypeCatalog.Employee:
                //    query = " SELECT numero_empleado, CONCAT(numero_empleado,'--',nombre,' ',apellido_paterno,' ', apellido_materno) as nombre_completo " +
                //            $" FROM empleados WHERE CONCAT(nombre, ' ', apellido_paterno, ' ', apellido_materno) LIKE '%{filtro}%' AND estatus = 'Activo' ORDER BY nombre ASC ";
                //    display = "nombre_completo";
                //    value = "numero_empleado";
                //    break;

                case TypeCatalog.Employee:
                    query = " SELECT numero_empleado, CONCAT(numero_empleado,'--',nombre,' ',apellido_paterno,' ', apellido_materno) as nombre_completo " +
                            $" FROM empleados WHERE numero_empleado LIKE '%{filtro}%' AND estatus = 'Activo' ORDER BY numero_empleado ASC ";
                    display = "nombre_completo";
                    value = "numero_empleado";
                    break;

                case TypeCatalog.Use:
                    query = " SELECT u.uso, CONCAT(u.uso,'--',u.descripcion,'--',d.nombre) as descripcion " +
                            $" FROM usos u INNER JOIN departamentos d ON u.departamento = d.id_deparamento WHERE u.uso LIKE '%{param}%' ORDER BY u.uso ASC ";
                    display = "descripcion";
                    value = "uso";
                    break;

                case TypeCatalog.Category:
                    query = " SELECT id_categoria, CONCAT(id_categoria,'--',nombre_categoria) AS n_categoria " +
                            $" FROM categorias WHERE id_categoria LIKE '%{param}%' ORDER BY id_categoria ASC ";
                    display = "n_categoria";
                    value = "id_categoria";
                    break;

                case TypeCatalog.Concept:
                    query = " SELECT id_concepto, CONCAT(id_concepto,'--',nombre_concepto) AS n_concepto " +
                            $" FROM conceptos_pd WHERE nombre_concepto LIKE '%{param}%' ORDER BY id_concepto ASC ";
                    display = "n_concepto";
                    value = "id_concepto";
                    break;

                case TypeCatalog.Equipment:
                    query = " SELECT equipo, CONCAT(equipo,'--',descripcion) AS equipos " +
                            $" FROM equipos WHERE equipo LIKE '%{param}%' ORDER BY equipo ASC ";
                    display = "equipos";
                    value = "equipo";
                    break;

                case TypeCatalog.Management:
                    query = " SELECT gerencia, CONCAT(nombre,'--',responsable) AS nombres " +
                            $" FROM gerencias WHERE nombre LIKE '%{param}%' ORDER BY nombre ASC ";
                    display = "nombres";
                    value = "gerencia";
                    break;

                case TypeCatalog.Group:
                    query = " SELECT grupo, CONCAT(grupo,'--',descripcion) AS grupos " +
                            $" FROM grupos WHERE grupo LIKE '%{param}%' ORDER BY grupo ASC ";
                    display = "grupos";
                    value = "grupo";
                    break;

                case TypeCatalog.Department:
                    query = " SELECT id_deparamento, CONCAT(id_deparamento,'--',nombre) AS departamentos " +
                            $" FROM departamentos WHERE nombre LIKE '%{param}%' ORDER BY nombre ASC ";
                    display = "departamentos";
                    value = "id_deparamento";
                    break;

                case TypeCatalog.TrainedCategories:
                    query = "SELECT C.nombre_categoria, CC.id_categoria,concat(CC.id_categoria,'--',C.nombre_categoria) as n_categoria " +
                        "FROM categorias C " +
                        "INNER JOIN categorias_capacitadas CC " +
                        "ON " +
                        $"C.id_categoria = CC.id_categoria WHERE CC.id_empleado = {nEmployee} ORDER BY CC.id_categoria ASC ";
                    display = "n_categoria";
                    value = "id_categoria";
                    break;

                case TypeCatalog.Material:
                    query = "SELECT id,material FROM materiales " +
                            $"WHERE material LIKE '%{param}%' ORDER BY material ASC";
                    display = "material";
                    value = "material";
                    break;
            }

            LoadComboBox(combo, query, display, value);
        }

        public void OnlyLetter(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancela el evento, no escribe nada
            }
        }

        public void OnlyNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es número ni control, se marca como "manejado" para que no se escriba
                e.Handled = true;
            }
        }

        public DataTable GetDataCSV(string root)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("c1", typeof(int));
            dt.Columns.Add("c2", typeof(decimal));
            dt.Columns.Add("c3", typeof(decimal));

            string[] lines = File.ReadAllLines(root);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] datos = line.Split(',');
                if (datos.Length >= 3)
                {
                    DataRow row = dt.NewRow();
                    row["c1"] = int.Parse(datos[0].Trim());
                    row["c2"] = decimal.Parse(datos[1].Trim());
                    row["c3"] = decimal.Parse(datos[2].Trim());
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public int GetPayWeek(int n)
        {
            string number = n.ToString();
            return int.Parse(number.Substring(4, 2));
        }

        public int GetPayWeekType(int n)
        {
            string number = n.ToString();
            return int.Parse(number.Substring(6, 1)); // "1");
        }

        public int PayWeekNow(int num1, int num2)
        {
            int year = DateTime.Now.Year;
            int week = (int)num1;
            int extra = (int)num2;

            return (year * 1000) + (week * 10) + extra;
        }

    }
}
