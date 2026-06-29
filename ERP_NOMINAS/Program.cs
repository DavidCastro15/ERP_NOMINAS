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
            try
            {
                var forzarOpenXml = typeof(DocumentFormat.OpenXml.OpenXmlElement);
                var forzarNumberFormat = typeof(ExcelNumberFormat.NumberFormat);
            }
            catch { /* No hace nada, solo es para el compilador */ }
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

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //// 🟢 MODO BYPASS: Si no se detectan argumentos (doble clic directo)
            //if (args == null || args.Length == 0)
            //{
            //    // Creamos una sesión de pruebas falsa directamente en la memoria para el entorno local
            //    ERP_SHARED.Auth.Sesion.UserIsLoggin = new ERP_SHARED.Auth.Models.User
            //    {
            //        Id = 999,
            //        Username = "PROBADOR_LOCAL",
            //        IdRole = 1,
            //        NameRole = "Administrador" // Así hereda control total de los menús
            //    };



            //    // Fijas tu base de datos de pruebas predeterminada
            //    ERP_SHARED.Conexion.ConnectorSql.NameBd = "azsja_nomina";
            //    ERP_SHARED.Auth.Sesion.DataBaseName = "azsja_nomina";
            //}
            //else
            //{
            //    // Flujo de producción normal (Lo que ya te funciona al 100% con el Login)
            //    try
            //    {
            //        byte[] base64Bytes = Convert.FromBase64String(args[0]);
            //        string jsonSesion = Encoding.UTF8.GetString(base64Bytes);

            //        var package = Newtonsoft.Json.JsonConvert.DeserializeObject<ERP_SHARED.Auth.SessionPackage>(jsonSesion);

            //        if (package != null)
            //        {
            //            ERP_SHARED.Auth.Sesion.UserIsLoggin = package.User;
            //            ERP_SHARED.Auth.Sesion.AuthorizedCatalogs = package.Permissions;
            //            ERP_SHARED.Conexion.ConnectorSql.NameBd = package.DataBaseEnable;
            //            ERP_SHARED.Auth.Sesion.DataBaseName = package.DataBaseEnable;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Error de Arranque: {ex.Message}");
            //        return;
            //    }
            //}

            //// Abre el sistema directo con los datos precargados
            //Application.Run(new MainControl());
        }
    }
}

