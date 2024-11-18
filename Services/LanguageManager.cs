using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Services
{
    public class LanguageManager : ISubject
    {
        private List<IObserver> ListaFormularios = new List<IObserver>();
        private Dictionary<string, string> Diccionario;

        //Patrón singleton

        private static LanguageManager instancia;

        private LanguageManager() { }

        public static LanguageManager ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new LanguageManager();
            }
            return instancia;
        }
        
        //Patrón observer

        public void Agregar(IObserver observer)
        {
            ListaFormularios.Add(observer);
        }

        public void Quitar(IObserver observer)
        {
            ListaFormularios.Remove(observer);
        }

        public void Notificar()
        {
            System.Globalization.CultureInfo culture;

            switch (SessionManager.ObtenerInstancia().idiomaActual)
            {
                case "Español":
                    culture = new System.Globalization.CultureInfo("es-ES");
                    break;
                case "English":
                    culture = new System.Globalization.CultureInfo("en-US");
                    break;
                default:
                    culture = new System.Globalization.CultureInfo("es-ES");
                    break;
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            foreach (IObserver observer in ListaFormularios)
            {
                observer.ActualizarIdioma();
            }
        }

        public void CargarIdioma()
        {
            try
            {
                var NombreArchivo = Path.Combine("..", "..", "..", $"Idiomas\\{SessionManager.ObtenerInstancia().idiomaActual}.json");
                var jsonString = File.ReadAllText(NombreArchivo);
                Diccionario = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            }
            catch(Exception)
            {
                var NombreArchivo = Path.Combine($"Idiomas\\{SessionManager.ObtenerInstancia().idiomaActual}.json");
                var jsonString = File.ReadAllText(NombreArchivo);
                Diccionario = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            }
        }

        public string ObtenerTexto(string key)
        {
            return Diccionario.ContainsKey(key) ? Diccionario[key] : key;
        }

        public void CambiarIdiomaControles(Control frm)
        {
            try
            {
                frm.Text = LanguageManager.ObtenerInstancia().ObtenerTexto(frm.Name + ".Text");

                foreach (Control c in frm.Controls)
                {
                    if (c is Button || c is Label)
                    {
                        c.Text = ObtenerInstancia().ObtenerTexto(frm.Name + "." + c.Name);
                    }

                    if(c is MenuStrip m)
                    {
                        foreach(ToolStripMenuItem item in m.Items)
                        {
                            if (item is ToolStripMenuItem toolStripMenuItem)
                            {
                                item.Text = ObtenerInstancia().ObtenerTexto(frm.Name + "." + item.Name);
                                CambiarIdiomaMenuStrip(toolStripMenuItem.DropDownItems, frm);
                            }
                        }
                    }

                    if(c is Form)
                    {
                        break;
                    }

                    if (c.Controls.Count > 0)
                    {
                        CambiarIdiomaControles(c);
                    }
                }
            }
            catch (Exception) { };
        }

        private void CambiarIdiomaMenuStrip(ToolStripItemCollection items, Control frm)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem item1)
                {
                    item.Text = ObtenerInstancia().ObtenerTexto(frm.Name + "." + item.Name);

                    CambiarIdiomaMenuStrip(item1.DropDownItems, frm);
                }
            }
        }
    }
}
