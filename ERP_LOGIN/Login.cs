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
                var res = _repository.Login(textBox1.Text, textBox2.Text);
                if (res == null)
                {
                    MessageBox.Show("Error: Datos incorrectos.");
                    return;
                }

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
                    DataBaseEnable = Convert.ToString(comboBox1.SelectedValue),
                    
                };

                // Serializamos el paquete completo a texto y luego a Base64
                ERP_SHARED.Auth.Sesion.UserIsLoggin = package.User;
                ERP_SHARED.Auth.Sesion.DataBaseName = Convert.ToString(comboBox1.SelectedValue);
                ERP_SHARED.GlobalFunctions.Logs.DataLogsService.Register("LOGIN", "FormLogin", "button1", "Inicio de sesión correcto en el sistema.");
                string jsonFull = Newtonsoft.Json.JsonConvert.SerializeObject(package);
                string txtSecure = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonFull));
                

                string routeNomi = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ERP_NOMINAS.exe");

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
                    MessageBox.Show($"Error Crítico: El archivo 'ERP_NOMINAS.exe' no existe en esa ruta.\n\nAsegúrate de compilar el proyecto de Nóminas para que se genere el archivo en la carpeta Debug/Release.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error dentro del botón de Login:\n\n{ex.Message}\n\n{ex.StackTrace}", "Error de código", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
    }
}
