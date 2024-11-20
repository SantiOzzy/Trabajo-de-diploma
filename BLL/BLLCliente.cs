using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data;
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

        public void BotonAplicar(string modo, int dni, string nombre, string apellido, string direccion, string email, string numeroTelefono)
        {
            if (modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"))
            {
                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Consulta de clientes", 6));
                throw new ArithmeticException(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClientesConsultados"));
            }

            if (modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                if (ValidarCampos(modo, dni, nombre, apellido, direccion, email, numeroTelefono) == true)
                {
                    Cliente customer = new Cliente(dni, nombre, apellido, direccion, email, numeroTelefono);

                    RegistrarCliente(customer);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Registro de cliente", 4));

                    throw new ApplicationException(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClienteRegistrado"));
                }
            }

            if (modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                if (ValidarCampos(modo, dni, nombre, apellido, direccion, email, numeroTelefono) == true)
                {
                    Cliente customer = new Cliente(dni, nombre, apellido, direccion, email, numeroTelefono);

                    ModificarCliente(customer);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Modificación de cliente", 4));

                    throw new ApplicationException(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClienteModificado"));
                }
            }

            if (modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar"))
            {
                if (negocios.RevisarDisponibilidad(dni.ToString(), "DNI", "Cliente") == false)
                {
                    throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DNINoExiste"));
                }
                else if (RevisarDesactivado(dni.ToString()) == false)
                {
                    ActivarCliente(dni.ToString());

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Restauración de cliente", 4));

                    throw new ApplicationException(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClienteRestaurado"));
                }
                else
                {
                    DesactivarCliente(dni.ToString());

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Borrado lógico de cliente", 4));

                    throw new ApplicationException(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClienteEliminado"));
                }
            }
        }

        public string ObtenerCondicionesQuery(string dni, string nombre, string apellido, string direccion, string email, string numTelefono, string activo, bool checkDni, bool checkNombre, bool checkApellido, bool checkDireccion, bool checkEmail, bool checkNumTel, bool checkActivo)
        {
            //Esta función devuelve las condiciones para el WHERE de la consulta de clientes

            string c = "";

            if (checkDni == true)
            {
                c += "DNI = " + dni + " AND ";
            }

            if (checkNombre == true)
            {
                c += "Nombre = '" + nombre + "' AND ";
            }

            if (checkApellido == true)
            {
                c += "Apellido = '" + apellido + "' AND ";
            }

            if (checkDireccion == true)
            {
                c += "Direccion = '" + direccion + "' AND ";
            }

            if (checkEmail == true)
            {
                c += "Email = '" + email + "' AND ";
            }

            if (checkNumTel == true)
            {
                c += "NumTelefono = '" + numTelefono + "' AND ";
            }

            if (checkActivo == true)
            {
                c += "Activo = '" + activo + "' AND ";
            }

            if (c.Length > 5)
            {
                c = c.Substring(0, c.Length - 5);
            }

            return c;
        }

        private bool ValidarCampos(string modo, int dni, string nombre, string apellido, string direccion, string email, string numeroTelefono)
        {
            //Valida todos los campos para verificar que los datos ingresados son válidos (Se usa al añadir y modificar clientes)

            MailAddress Correo;
            bool MailValido = false;
            bool NumeroTelefonoValido = Regex.IsMatch(numeroTelefono, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");

            if (email != "")
            {
                try
                {
                    Correo = new MailAddress(email);
                    MailValido = true;
                }
                catch (FormatException) { }
            }

            if (nombre == "" || apellido == "" || direccion == "" || email == "" || numeroTelefono == "")
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.LlenarCampos"));
            }
            else if (negocios.RevisarDisponibilidad(dni.ToString(), "DNI", "Cliente") && modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DNIEnUso"));
            }
            else if (negocios.RevisarDisponibilidad(dni.ToString(), "DNI", "Cliente") == false && modo != LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DNINoExiste"));
            }
            else if (MailValido == false)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.CorreoInvalido"));
            }
            else if (negocios.RevisarDisponibilidad(email, "Email", "Cliente") && modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.CorreoEnUso"));
            }
            else if (negocios.RevisarDisponibilidadConExcepcion(email, dni.ToString(), "DNI, Email", "Cliente") && modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.CorreoEnUso"));
            }
            else if (NumeroTelefonoValido == false)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.NumTelInvalido"));
            }
            else
            {
                return true;
            }
        }

        public DataTable TraducirTablaClientes(DataTable dt)
        {
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Apellido");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Activo");

            return dt;
        }
    }
}
