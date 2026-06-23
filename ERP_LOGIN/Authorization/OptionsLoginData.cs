using ERP_SHARED.Conexion;
using ERP_SHARED.GlobalFunctions.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_NOMINAS.Authorization
{
    public class OptionsLoginData
    {
        ConnectorSql Conex = new ConnectorSql();
        SqlCommand cmd = new SqlCommand();

        public void LoadTypeCombo(ComboBox combo, TypeCatalog type)
        {
            string query = "";
            string display = "";
            string value = "";

            switch (type)
            {
                case TypeCatalog.PayRoll:
                    query = "SELECT * FROM sistemas_base";
                    display = "sistema";
                    value = "base_de_datos";
                    break;
            }

            LoadComboBox(combo, query, display, value);
        }

        private void LoadComboBox(ComboBox combo, string query, string display, string value)
        {
            if (string.IsNullOrEmpty(query))
            {
                combo.DataSource = null;
                return;
            }

            try
            {
                if (Conex.auth.State != ConnectionState.Open) Conex.OpenAuth();

                SqlDataAdapter da = new SqlDataAdapter(query, Conex.auth);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (da != null && dt.Rows.Count > 0)
                {

                    combo.DataSource = dt;
                    combo.DisplayMember = display; 
                    combo.ValueMember = value;    

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

        }
    }
}
