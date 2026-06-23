using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_SHARED
{
    public class LoadDataCombo
    {
        ConnectorSql Conex = new ConnectorSql();

        public void Load(ComboBox combo, string query, string display, string value)
        {
            if (string.IsNullOrEmpty(query))
            {
                combo.DataSource = null;
                return;
            }

            try
            {
                if (Conex.auth.State != ConnectionState.Open) Conex.OpenAuth();
                
                using (SqlDataAdapter da = new SqlDataAdapter(query, Conex.auth))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        combo.DataSource = dt;
                        combo.DisplayMember = display;
                        combo.ValueMember = value;
                        combo.SelectedIndex = 0;
                    }
                    else
                    {
                        combo.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }
    }
}
