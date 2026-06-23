using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_SHARED.GlobalFunctions.Logs
{
    public class Auditor
    {
 
            // Lista en memoria para evitar registros duplicados en la misma ventana abierta
            private static readonly HashSet<Form> _formulariosRegistrados = new HashSet<Form>();

            // 🟢 ESTE MÉTODO SERÁ LLAMADO DIRECTAMENTE POR EL BASEFORM
            public static void RegistrarPantallaHija(Form formulario)
            {
                if (formulario == null) return;

                try
                {
                    // Si este formulario ya se registró en esta apertura, no hacemos nada
                    if (_formulariosRegistrados.Contains(formulario)) return;
                    _formulariosRegistrados.Add(formulario);

                    string nombrePantallaReal = formulario.GetType().Name;
                    if (nombrePantallaReal == "FormLogin") return; // Ignoramos el login aquí

                    string tituloPantalla = string.IsNullOrEmpty(formulario.Text) ? nombrePantallaReal : formulario.Text;

                    // 1. Registro directo e inmediato en la base de datos de auditoría
                    ERP_SHARED.GlobalFunctions.Logs.DataLogsService.Register(
                        "OPEN",
                        nombrePantallaReal,
                        nombrePantallaReal,
                        $"El usuario abrió la pantalla: {tituloPantalla}"
                    );

                    // 2. Mapeo profundo e inmediato de los botones
                    if (formulario.Controls != null && formulario.Controls.Count > 0)
                    {
                        MapearControlesRecursivo(formulario.Controls, nombrePantallaReal);
                    }

                    // Si se cierra la pantalla, la quitamos de la lista para permitir futuros registros
                    formulario.FormClosed += (s, e) =>
                    {
                        _formulariosRegistrados.Remove(formulario);
                    };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en Registro Directo: " + ex.Message);
                }
            }

            public static void MapearControlesRecursivo(Control.ControlCollection controles, string nombrePantalla)
            {
                if (controles == null) return;

                foreach (Control control in controles)
                {
                    if (control is Button boton)
                    {
                        boton.Click -= (s, e) => { }; // Limpieza
                        boton.Click += (sender, e) =>
                        {
                            ERP_SHARED.GlobalFunctions.Logs.DataLogsService.Register(
                                "CLICK",
                                nombrePantalla,
                                boton.Name,
                                $"Clic en botón: {boton.Text}"
                            );
                        };
                    }

                    if (control.HasChildren || control.Controls.Count > 0)
                    {
                        MapearControlesRecursivo(control.Controls, nombrePantalla);
                    }
                }
            }
        }
}



