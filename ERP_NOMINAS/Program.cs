using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP_NOMINAS
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args == null || args.Length == 0)
            {
                MessageBox.Show("Acceso denegado: Debe iniciar sesión desde el módulo central.",
                                "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // 1. Recuperamos el argumento seguro en Base64
                byte[] base64Bytes = Convert.FromBase64String(args[0]);
                string jsonSesion = Encoding.UTF8.GetString(base64Bytes);

                // 🟢 ELIMINAMOS LA LÍNEA DE ActivarAuditoriaGlobal() DE AQUÍ COMPLETAMENTE

                // 2. Deserializamos el paquete directo con Newtonsoft
                var package = Newtonsoft.Json.JsonConvert.DeserializeObject<ERP_SHARED.Auth.SessionPackage>(jsonSesion);

                if (package != null)
                {
                    // 3. Reconstruimos los datos de la sesión en el proyecto compartido
                    ERP_SHARED.Auth.Sesion.UserIsLoggin = package.User;
                    ERP_SHARED.Auth.Sesion.AuthorizedCatalogs = package.Permissions;
                    ERP_SHARED.Conexion.ConnectorSql.NameBd = package.DataBaseEnable;
                    ERP_SHARED.Auth.Sesion.DataBaseName = package.DataBaseEnable;
                }
                else
                {
                    MessageBox.Show("Error: El paquete de sesión llegó vacío.");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error crítico al desempaquetar la sesión en Nóminas:\n\n{ex.Message}\n\n{ex.StackTrace}",
                                "Error de Arranque", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Run(new MainControl());
        }
    }
}

