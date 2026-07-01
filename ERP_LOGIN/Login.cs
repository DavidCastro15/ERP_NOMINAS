using ERP_LOGIN.Authorization.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using ERP_SHARED;
using ERP_SHARED.GlobalFunctions.Enums;

namespace ERP_LOGIN
{
    public partial class Login : Form
    {
        private LoadDataCombo Util = new LoadDataCombo();
        private LoginRepository _repository = new LoginRepository();

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Util.Load(comboBox1,"SELECT sistema,base_de_datos FROM sistemas_base","sistema", "base_de_datos");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validamos credenciales primero
                var res = _repository.Login(textBox1.Text, textBox2.Text);
                if (res == null)
                {
                    MessageBox.Show("Error: Datos incorrectos.");
                    return;
                }

                // 2. Armamos el paquete de sesión completo de forma normal
                string bdSeleccionada = Convert.ToString(comboBox1.SelectedValue);

                var package = new ERP_SHARED.Auth.SessionPackage
                {
                    User = new ERP_SHARED.Auth.Models.User
                    {
                        Id = res.Id,
                        Username = res.Username,
                        IdRole = res.IdRole,
                        NameRole = res.NameRole
                    },
                    Permissions = new PermissionsRepository().GetNameMenuXUser(res.Id),
                    DataBaseEnable = bdSeleccionada,
                };

                // Guardamos en memoria local e insertamos el LOG de inicio de sesión
                ERP_SHARED.Auth.Sesion.UserIsLoggin = package.User;
                ERP_SHARED.Auth.Sesion.DataBaseName = bdSeleccionada;
                ERP_SHARED.GlobalFunctions.Logs.DataLogsService.Register("LOGIN", "FormLogin", "button1", "Inicio de sesión correcto en el sistema.");

                // Generamos el texto seguro en Base64 que usará el ERP (o el actualizador)
                string jsonFull = Newtonsoft.Json.JsonConvert.SerializeObject(package);
                string txtSecure = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonFull));

                // 3. 🔥 --- DETECTOR DE ACTUALIZACIONES CON PASADIZO DIRECTO ---
                // Hacemos la validación de fechas de modificación en el servidor
                string routeLocal = AppDomain.CurrentDomain.BaseDirectory;
                string fileLocal = Path.Combine(routeLocal, "ERP_NOMINAS.exe");
                string routeServer = @"\\192.168.2.250\AsjaApps\ErpNominas\ERP_TEST";
                string fileServer = Path.Combine(routeServer, "ERP_NOMINAS.exe");

                if (Directory.Exists(routeServer) && File.Exists(fileServer) && File.Exists(fileLocal))
                {
                    DateTime dateLocal = File.GetLastWriteTime(fileLocal);
                    DateTime dateServer = File.GetLastWriteTime(fileServer);
                    double totalSecondsDifference = (dateServer - dateLocal).TotalSeconds;

                    if (totalSecondsDifference > 5)
                    {
                        MessageBox.Show("Se detectó una nueva versión en el servidor. El sistema se actualizará de forma invisible y entrará al sistema al instante.",
                                        "Actualización de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 🟢 MANDAMOS LLAMAR AL NUEVO SCRIPT PASÁNDOLE EL PAQUETE DE SESIÓN
                        CreateUpdaterScript(routeLocal, routeServer, txtSecure);

                        Application.Exit(); // Cerramos el login viejo para liberar los archivos
                        return;
                    }
                }
                // ---------------------------------------------------------------

                // 4. FLUJO NORMAL: Si el sistema ya estaba actualizado, abre Nóminas directo como siempre
                string routeNomi = Path.Combine(routeLocal, "ERP_NOMINAS.exe");

                if (File.Exists(routeNomi))
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = routeNomi;
                    info.Arguments = txtSecure;
                    info.UseShellExecute = true;

                    Process.Start(info);

                    this.Hide();
                    System.Threading.Thread.Sleep(1000);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show($"Error Crítico: El archivo 'ERP_NOMINAS.exe' no existe.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error dentro del botón de Login:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreateUpdaterScript(string localFolder, string serverFolder, string txtSecure)
        {
            string routeBat = Path.Combine(localFolder, "Updater.bat");

            // Escribimos las instrucciones nativas de Windows invisibles
            string[] lineScript = new string[]
            {
        "@echo off",
        "ping 127.0.0.1 -n 3 > nul", // Espera 2 segundos a que el Login muera y libere los archivos
        
        // Reemplazo forzado de archivos
        $"xcopy \"{serverFolder}\\*.*\" \"{localFolder}\" /Y /Q /R /K > nul",
        
        // 🟢 LA MAGIA: En lugar de abrir el Login, abrimos DIRECTAMENTE las Nóminas nuevas
        // y le pasamos el paquete de sesión original para que entre logueado de golpe
        $"start ERP_NOMINAS.exe \"{txtSecure}\"",

        "del \"%~f0\"" // Se auto-borra el archivo .bat
            };

            try
            {
                File.WriteAllLines(routeBat, lineScript);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = routeBat;

                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al crear actualizador con bypass: " + ex.Message);
            }
        }
    }
}
