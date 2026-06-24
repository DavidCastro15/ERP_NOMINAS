using ERP_SHARED.Auth;
using ERP_SHARED.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_SHARED.GlobalFunctions.Logs
{
    public class DataLogsService
    {
        public static void Register(string action, string display, string component, string description)
        {
            int idUser = 1; // Valor de respaldo por si la sesión está vacía un milisegundo
            string nameUser = "Sistema/Anónimo";
            string DataBaseNameActive = "DB/Anónimo";

            try
            {
                if (ERP_SHARED.Auth.Sesion.UserIsLoggin != null)
                {
                    idUser = Convert.ToInt32(ERP_SHARED.Auth.Sesion.UserIsLoggin.Id);
                    nameUser = ERP_SHARED.Auth.Sesion.UserIsLoggin.Username.ToString();
                  
                }

                // 🟢 LEEMOS LA BASE DE DATOS QUE ESTÁ EN MEMORIA GLOBAL
                if (!string.IsNullOrEmpty(ERP_SHARED.Auth.Sesion.DataBaseName))
                {
                    DataBaseNameActive = ERP_SHARED.Auth.Sesion.DataBaseName;
                }
            }
            catch { /* Evita caídas por lectura */ }

            string module = AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "");

            string query = @"INSERT INTO erp_bitacora (id_usuario, nombre_usuario, modulo_programa, pantalla, componente, accion, descripcion, fecha_registro,dbuse) 
                 VALUES (@id_usuario, @nombre_usuario, @modulo, @pantalla, @componente, @action, @description, GETDATE(),@dbuse)";

            try
            {
                Conexion.ConnectorSql conex = new Conexion.ConnectorSql();

                using (SqlConnection cn = conex.auth)
                {
                    if (cn.State == System.Data.ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        // 2. Pasamos los parámetros asegurando que si van vacíos no rompan la base de datos
                        cmd.Parameters.AddWithValue("@id_usuario", idUser);
                        cmd.Parameters.AddWithValue("@nombre_usuario", nameUser ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@modulo", module ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@pantalla", display ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@componente", component ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@action", action ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@description", description ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@dbuse", DataBaseNameActive ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery(); // Ejecuta el comando de forma obligatoria
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                // TRUCO DE CONTROL CRÍTICO: Si el insert falla a nivel de base de datos local, 
                // obligamos a que el sistema te pinte una ventana de alerta real en la pantalla
                System.Windows.Forms.MessageBox.Show($"[SQL BITACORA ERROR] No se pudo guardar en la base de datos:\n\n" +
                                                     $"Mensaje del Servidor: {ex.Message}\n\n" +
                                                     $"Datos intentados: Pantalla={display}, Acción={action}",
                                                     "Fallo de Inserción Real",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
    
    



