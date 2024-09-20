using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Services
{
    public sealed class SessionManager
    {
        private SessionManager() { }

        private static SessionManager Instancia = null;
        Usuario user;
        public string idiomaActual;

        public string IdiomaActual
        {
            get { return idiomaActual; }
            set
            {
                idiomaActual = value;
                LanguageManager.ObtenerInstancia().CargarIdioma();
                LanguageManager.ObtenerInstancia().Notificar();
            }
        }

        public static SessionManager ObtenerInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new SessionManager();
            }
            return Instancia;
        }

        public void IniciarSesion(Usuario userNuevo)
        {
            user = userNuevo;
        }

        public void CerrarSesion()
        {
            user = null;
        }

        public Usuario ObtenerDatosUsuario()
        {
            return user;
        }
    }
}
