using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using DAL;
using BE;

namespace BLL
{
    public class BLLCliente
    {
        Datos Data = new Datos();
        DALCliente DataCliente = new DALCliente();
        BLLDV NegociosDV = new BLLDV();
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
    }
}
