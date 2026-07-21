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

            // Forzar carga de ensamblados
            ForceAssemblyLoading();

            // Se ejecuta SOLO en modo Desarrollo/Pruebas en Visual Studio
            InitializeTestEnvironment();
            //InitializeProductionEnvironment(args);

            Application.Run(new MainControl());
        }

        /// <summary>
        /// Configura un usuario administrador ficticio y base de datos local para desarrollo.
        /// </summary>
        private static void InitializeTestEnvironment()
        {
            ERP_SHARED.Auth.Sesion.UserIsLoggin = new ERP_SHARED.Auth.Models.User
            {
                Id = 999,
                Username = "PROBADOR_LOCAL",
                IdRole = 1,
                NameRole = "Administrador"
            };

            ERP_SHARED.Conexion.ConnectorSql.NameBd = "azsja_nomina";
            ERP_SHARED.Auth.Sesion.DataBaseName = "azsja_nomina";
        }

        /// <summary>
        /// Procesa los argumentos seguros de producción para validar el inicio de sesión central.
        /// </summary>
        private static bool InitializeProductionEnvironment(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                MessageBox.Show("Acceso denegado: Debe iniciar sesión desde el módulo central.",
                                "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                byte[] base64Bytes = Convert.FromBase64String(args[0]);
                string jsonSesion = Encoding.UTF8.GetString(base64Bytes);

                var package = Newtonsoft.Json.JsonConvert.DeserializeObject<ERP_SHARED.Auth.SessionPackage>(jsonSesion);

                if (package != null)
                {
                    ERP_SHARED.Auth.Sesion.UserIsLoggin = package.User;
                    ERP_SHARED.Auth.Sesion.AuthorizedCatalogs = package.Permissions;
                    ERP_SHARED.Conexion.ConnectorSql.NameBd = package.DataBaseEnable;
                    ERP_SHARED.Auth.Sesion.DataBaseName = package.DataBaseEnable;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error: El paquete de sesión llegó vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error crítico al desempaquetar la sesión en Nóminas:\n\n{ex.Message}\n\n{ex.StackTrace}",
                                "Error de Arranque", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Mantiene las referencias de OpenXml y ExcelNumberFormat para el compilador.
        /// </summary>
        private static void ForceAssemblyLoading()
        {
            try
            {
                var forzarOpenXml = typeof(DocumentFormat.OpenXml.OpenXmlElement);
                var forzarNumberFormat = typeof(ExcelNumberFormat.NumberFormat);
            }
            catch { /* No hace nada, solo es para el compilador */ }



    //Application.EnableVisualStyles();
    //Application.SetCompatibleTextRenderingDefault(false);
    //try
    //{
    //    var forzarOpenXml = typeof(DocumentFormat.OpenXml.OpenXmlElement);
    //    var forzarNumberFormat = typeof(ExcelNumberFormat.NumberFormat);
    //}
    //catch { /* No hace nada, solo es para el compilador */ }
    //if (args == null || args.Length == 0)
    //{
    //    MessageBox.Show("Acceso denegado: Debe iniciar sesión desde el módulo central.",
    //                    "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    //    return;
    //}
    //try
    //{
    //    // 1. Recuperamos el argumento seguro en Base64
    //    byte[] base64Bytes = Convert.FromBase64String(args[0]);
    //    string jsonSesion = Encoding.UTF8.GetString(base64Bytes);

    //    // 🟢 ELIMINAMOS LA LÍNEA DE ActivarAuditoriaGlobal() DE AQUÍ COMPLETAMENTE

    //    // 2. Deserializamos el paquete directo con Newtonsoft
    //    var package = Newtonsoft.Json.JsonConvert.DeserializeObject<ERP_SHARED.Auth.SessionPackage>(jsonSesion);

    //    if (package != null)
    //    {
    //        // 3. Reconstruimos los datos de la sesión en el proyecto compartido
    //        ERP_SHARED.Auth.Sesion.UserIsLoggin = package.User;
    //        ERP_SHARED.Auth.Sesion.AuthorizedCatalogs = package.Permissions;
    //        ERP_SHARED.Conexion.ConnectorSql.NameBd = package.DataBaseEnable;
    //        ERP_SHARED.Auth.Sesion.DataBaseName = package.DataBaseEnable;
    //    }
    //    else
    //    {
    //        MessageBox.Show("Error: El paquete de sesión llegó vacío.");
    //        return;
    //    }

    //}
    //catch (Exception ex)
    //{
    //    MessageBox.Show($"Ocurrió un error crítico al desempaquetar la sesión en Nóminas:\n\n{ex.Message}\n\n{ex.StackTrace}",
    //                    "Error de Arranque", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //}

    //Application.Run(new MainControl()); ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Application.EnableVisualStyles();
    //Application.SetCompatibleTextRenderingDefault(false);

    //// 🟢 MODO BYPASS: Si no se detectan argumentos 
    //if (args == null || args.Length == 0)
    //{
    //    ERP_SHARED.Auth.Sesion.UserIsLoggin = new ERP_SHARED.Auth.Models.User
    //    {
    //        Id = 999,
    //        Username = "PROBADOR_LOCAL",
    //        IdRole = 1,
    //        NameRole = "Administrador" 
    //    };

    //    ERP_SHARED.Conexion.ConnectorSql.NameBd = "azsja_nomina";
    //    ERP_SHARED.Auth.Sesion.DataBaseName = "azsja_nomina";
    //}
    //else
    //{
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

    //Application.Run(new MainControl());
}
    }
}

