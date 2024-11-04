using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Services;
using DAL;
using BE;

namespace BLL
{
    public class BLLCliente
    {
        Datos Data = new Datos();
        Negocios negocios = new Negocios();
        DALCliente DataCliente = new DALCliente();
        BLLDV NegociosDV = new BLLDV();
        BLLEvento NegociosEvento = new BLLEvento();
        CryptoManager Encriptacion = new CryptoManager();

        public void RegistrarCliente(Cliente customer)
        {
            Data.EjecutarComando("InsertarCliente", $"{customer.DNI}, '{customer.Nombre}', '{customer.Apellido}', '{Encriptacion.GetAES256(customer.Direccion)}', '{customer.Email}', '{customer.NumTelefono}'");

            NegociosDV.RecalcularDVTabla("Cliente");
        }

        public void ModificarCliente(Cliente customer)
        {
            Data.EjecutarComando("ModificarCliente", $"{customer.DNI}, '{customer.Nombre}', '{customer.Apellido}', '{Encriptacion.GetAES256(customer.Direccion)}', '{customer.Email}', '{customer.NumTelefono}'");

            NegociosDV.RecalcularDVTabla("Cliente");
        }

        public void DesactivarCliente(string DNI)
        {
            DataCliente.DesactivacionCliente(DNI, 0);

            NegociosDV.RecalcularDVTabla("Cliente");
        }

        public void ActivarCliente(string DNI)
        {
            DataCliente.DesactivacionCliente(DNI, 1);

            NegociosDV.RecalcularDVTabla("Cliente");
        }

        public bool RevisarDesactivado(string DNI)
        {
            return DataCliente.RevisarDesactivado(DNI, "DNI, Activo");
        }

        public void RegistrarClienteFormulario(string DNI, string Nombre, string Apellido, string Direccion, string Email, string NumTel)
        {
            MailAddress Correo;
            bool MailValido = false;
            bool NumeroTelefonoValido = Regex.IsMatch(NumTel, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");

            if (Email != "")
            {
                try
                {
                    Correo = new MailAddress(Email);
                    MailValido = true;
                }
                catch (FormatException) { }
            }

            if (Nombre == "" || Apellido == "" || Direccion == "" || Email == "" || NumTel == "")
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.LlenarCampos"));
            }
            else if (negocios.RevisarDisponibilidad(DNI, "DNI", "Cliente"))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.DNIOcupado"));
            }
            else if (MailValido == false)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.CorreoInvalido"));
            }
            else if (negocios.RevisarDisponibilidad(Email, "Email", "Cliente"))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.CorreoOcupado"));
            }
            else if (NumeroTelefonoValido == false)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.NumTelInvalido"));
            }
            else
            {
                Cliente cliente = new Cliente(Convert.ToInt32(DNI), Nombre, Apellido, Direccion, Email, NumTel);

                RegistrarCliente(cliente);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Cliente", "Registro de cliente", 4));
            }
        }
    }
}
