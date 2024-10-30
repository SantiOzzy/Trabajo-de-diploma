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
        DALProveedor DataProveedor = new DALProveedor();
        public void PreRegistrarProveedor(Proveedor prov)
        {
            Data.EjecutarComando("PreRegistrarProveedor", $"'{prov.CUIT}', '{prov.RazonSocial}', '{prov.Nombre}', '{prov.Email}', '{prov.NumTelefono}'");
        }

        public void RegistrarProveedor(Proveedor prov)
        {
            Data.EjecutarComando("RegistrarProveedor", $"'{prov.CUIT}', '{prov.Direccion}', '{prov.CuentaBancaria}'");
        }

        public void RegistrarProveedorCompleto(Proveedor prov)
        {
            if(prov.Direccion == "")
            {
                prov.Direccion = "NULL";
            }
            if(prov.CuentaBancaria == "")
            {
                prov.CuentaBancaria = "NULL";
            }

            Data.EjecutarComando("RegistrarProveedorCompleto", $"'{prov.CUIT}', '{prov.RazonSocial}', '{prov.Nombre}', '{prov.Email}', '{prov.NumTelefono}', '{prov.Direccion}', '{prov.CuentaBancaria}'");
        }

        public Proveedor ObtenerProveedor(string CUIT)
        {
            return DataProveedor.ObtenerProveedor(CUIT);
        }

        public void ModificarProveedor(Proveedor prov)
        {
            if (prov.Direccion == "")
            {
                prov.Direccion = "NULL";
            }
            else
            {
                prov.Direccion = $"'{prov.Direccion}'";
            }
            if (prov.CuentaBancaria == "")
            {
                prov.CuentaBancaria = "NULL";
            }
            else
            {
                prov.CuentaBancaria = $"'{prov.CuentaBancaria}'";
            }

            Data.EjecutarComando("ModificarProveedor", $"'{prov.CUIT}', '{prov.RazonSocial}', '{prov.Nombre}', '{prov.Email}', '{prov.NumTelefono}', {prov.Direccion}, {prov.CuentaBancaria}");
        }

        public void DesactivarProveedor(string CUIT)
        {
            DataProveedor.DesactivacionProveedor(CUIT, 0);
        }

        public void ActivarProveedor(string CUIT)
        {
            DataProveedor.DesactivacionProveedor(CUIT, 1);
        }

        public bool RevisarDesactivado(string CUIT)
        {
            return DataProveedor.RevisarDesactivado(CUIT, "CUIT, Activo");
        }
    }
}
