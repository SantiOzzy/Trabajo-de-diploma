﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLUsuario
    {
        DALUsuario DataUsuario = new DALUsuario();
        Datos Data = new Datos();

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
