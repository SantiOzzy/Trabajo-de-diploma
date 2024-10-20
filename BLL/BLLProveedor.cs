using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
    public class BLLProveedor
    {
        Datos Data = new Datos();
        public void PreRegistrarProveedor(Proveedor prov)
        {
            Data.EjecutarComando("PreRegistrarProveedor", $"{prov.CUIT}, '{prov.RazonSocial}', '{prov.Nombre}', '{prov.Email}', '{prov.NumTelefono}'");
        }

        public void RegistrarProveedor(Proveedor prov)
        {
            Data.EjecutarComando("RegistrarProveedor", $"{prov.CUIT}, '{prov.Direccion}', '{prov.CuentaBancaria}'");
        }

        public Proveedor ObtenerProveedor(string CUIT)
        {
            return DataProveedor.ObtenerProveedor(CUIT);
        }
    }
}
