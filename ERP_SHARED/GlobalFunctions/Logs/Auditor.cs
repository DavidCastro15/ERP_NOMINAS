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
            private static readonly HashSet<Form> _registeredForm = new HashSet<Form>();

            // ESTE MÉTODO SERÁ LLAMADO DIRECTAMENTE POR EL BASEFORM
            public static void RegisterChildScreen(Form form)
            {
                if (form == null) return;

                try
                {
                    // Si este formulario ya se registró en esta apertura, no hacemos nada
                    if (_registeredForm.Contains(form)) return;
                _registeredForm.Add(form);

                    string nameDisplayReal = form.GetType().Name;
                    if (nameDisplayReal == "FormLogin") return; // Ignoramos el login aquí

                    string titleDisplay = string.IsNullOrEmpty(form.Text) ? nameDisplayReal : form.Text;

                    // 1. Registro directo e inmediato en la base de datos de auditoría
                    ERP_SHARED.GlobalFunctions.Logs.DataLogsService.Register(
                        "OPEN",
                        nameDisplayReal,
                        nameDisplayReal,
                        $"El usuario abrió la pantalla: {titleDisplay}"

                    );

                    // 2. Mapeo profundo e inmediato de los botones
                    if (form.Controls != null && form.Controls.Count > 0)
                    {
                    MapControlRecursive(form.Controls, nameDisplayReal);
                    }

                // Si se cierra la pantalla, la quitamos de la lista para permitir futuros registros
                form.FormClosed += (s, e) =>
                    {
                        _registeredForm.Remove(form);
                    };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en Registro Directo: " + ex.Message);
                }
            }

            public static void MapControlRecursive(Control.ControlCollection controls, string nameDisplay)
            {
                if (controls == null) return;

                foreach (Control control in controls)
                {
                    if (control is Button boton)
                    {
                        boton.Click -= (s, e) => { }; // Limpieza
                        boton.Click += (sender, e) =>
                        {
                            ERP_SHARED.GlobalFunctions.Logs.DataLogsService.Register(
                                "CLICK",
                                nameDisplay,
                                boton.Name,
                                $"Clic en botón: {boton.Text}"
                            );
                        };
                    }

                    if (control.HasChildren || control.Controls.Count > 0)
                    {
                    MapControlRecursive(control.Controls, nameDisplay);
                    }
                }
            }
        }
}



