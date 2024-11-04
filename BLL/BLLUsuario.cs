using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BE;
using DAL;
using Services;

namespace BLL
{
    public class BLLUsuario
    {
        DALUsuario DataUsuario = new DALUsuario();
        CryptoManager Encriptacion = new CryptoManager();
        Datos Data = new Datos();
        BLLEvento NegociosEvento = new BLLEvento();
        BLLDV NegociosDV = new BLLDV();

        public void IniciarSesion(string username, string password)
        {
            if (username == "" || password == "")
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.LlenarCampos"));
            }
            else if (RevisarDesactivado(username))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.UsuarioDesactivado"));
            }
            else if (RevisarBloqueado(username))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.UsuarioBloqueado"));
            }
            else
            {
                Usuario user = RevisarLogIn(username, Encriptacion.GetSHA256(password));

                if (user == null)
                {
                    if (IntentoFallido(username))
                    {
                        NegociosEvento.RegistrarEvento(new Evento(username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Sesiones", "Bloqueo de usuario por contraseña incorrecta", 1));

                        throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.TresFallos"));
                    }
                    throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.DatosIncorrectos"));
                }
                else
                {
                    SessionManager.ObtenerInstancia().IniciarSesion(user);

                    ReiniciarIntentosFallidos(username);

                    if (Data.LlenarTabla("DVH", "DV").Rows[0][0].ToString() == "")
                    {
                        NegociosDV.RecalcularBD();
                    }
                    else
                    {
                        NegociosDV.ComprobarDV();
                    }

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Sesiones", "Inicio de sesión", 1));

                }
            }
        }

        public void CerrarSesion()
        {
            NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Sesiones", "Cierre de sesión", 1));

            SessionManager.ObtenerInstancia().CerrarSesion();
        }

        public void CambiarContraseñaPropia(string username, string passwordViejo, string passwordNuevo, string passwordRepetido)
        {
            if (username == "" || passwordViejo == "" || passwordNuevo == "" || passwordRepetido == "")
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCambiarContraseña.Etiquetas.LlenarCampos"));
            }
            else if (passwordNuevo != passwordRepetido)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCambiarContraseña.Etiquetas.ContraseñaNoCoincide"));
            }
            else if (SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username != username || SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Contraseña != Encriptacion.GetSHA256(passwordViejo))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCambiarContraseña.Etiquetas.DatosIncorrectos"));
            }
            else if (!Regex.IsMatch(passwordNuevo, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCambiarContraseña.Etiquetas.ContraseñaInvalida"));
            }
            else
            {
                Usuario user = SessionManager.ObtenerInstancia().ObtenerDatosUsuario();

                user.Contraseña = Encriptacion.GetSHA256(passwordNuevo);

                ModificarContraseña(user.DNI, Encriptacion.GetSHA256(passwordNuevo));

                SessionManager.ObtenerInstancia().IniciarSesion(user);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Sesiones", "Cambio de contraseña", 1));

                SessionManager.ObtenerInstancia().CerrarSesion();
            }
        }

        public void RegistrarUsuario(Usuario user)
        {
            Data.EjecutarComando("InsertarUsuario", $"{user.DNI}, '{user.Nombre}', '{user.Apellido}', '{user.FechaNac.ToString("yyyy-MM-dd")}', '{user.Email}', '{user.NumTelefono}', '{user.Username}', '{user.Contraseña}', '{user.Rol}'");
        }

        public void ModificarUsuario(Usuario user)
        {
            Data.EjecutarComando("ModificarUsuario", $"{user.DNI}, '{user.Nombre}', '{user.Apellido}', '{user.FechaNac.ToString("yyyy-MM-dd")}', '{user.Email}', '{user.NumTelefono}', '{user.Username}', '{user.Rol}'");
        }

        public void ModificarContraseña(int DNI, string Contraseña)
        {
            Data.EjecutarComando("ModificarPassword", $"{DNI}, '{Contraseña}'");
        }

        public void DesactivarUsuario(int DNI)
        {
            Data.EjecutarComando("DesactivacionUsuario", $"{DNI}, 1");
        }

        public void ActivarUsuario(int DNI)
        {
            Data.EjecutarComando("DesactivacionUsuario", $"{DNI}, 0");
        }

        public Usuario RevisarLogIn(string Username, string Contraseña)
        {
            return DataUsuario.RevisarLogIn(Username, Contraseña);
        }

        public Usuario ObtenerUsuario(string Username)
        {
            return DataUsuario.ObtenerUsuario(Username);
        }

        public bool IntentoFallido(string Username)
        {
            if(Data.RevisarDisponibilidad(Username, "Username", "Usuario"))
            {
                Data.EjecutarComando("IntentoFallido", Username);
            }
            if (DataUsuario.RevisarSanciones(Username, 3, "Username, IntentosFallidos"))
            {
                Data.EjecutarComando("BloqueoUsuarioUsername", $"{Username}, 1");
                return true;
            }
            return false;
        }

        public void ReiniciarIntentosFallidos(string Username)
        {
            Data.EjecutarComando("ReiniciarIntentosFallidos", Username);
        }

        public bool RevisarDesactivado(string Username)
        {
            return DataUsuario.RevisarSanciones(Username, "Username, Desactivado");
        }

        public bool RevisarDesactivado(int DNI)
        {
            return DataUsuario.RevisarSanciones(DNI, "DNI, Desactivado");
        }

        public bool RevisarBloqueado(string Username)
        {
            return DataUsuario.RevisarSanciones(Username, "Username, Bloqueado");
        }

        public bool RevisarBloqueado(int DNI)
        {
            return DataUsuario.RevisarSanciones(DNI, "DNI, Bloqueado");
        }

        public void DesbloquearUsuario(int DNI)
        {
            Data.EjecutarComando("BloqueoUsuarioDNI", $"{DNI}, 0");
        }

        public void BloquearUsuario(int DNI)
        {
            Data.EjecutarComando("BloqueoUsuarioDNI", $"{DNI}, 1");
        }
    }
}
