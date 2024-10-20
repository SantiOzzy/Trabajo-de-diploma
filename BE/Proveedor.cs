using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Proveedor
    {
        public string CUIT;
        public string RazonSocial;
        public string Nombre;
        public string Email;
        public string NumTelefono;
        public string Direccion;
        public string CuentaBancaria;

        public Proveedor(string cUIT, string razonSocial, string nombre, string email, string numTelefono)
        {
            CUIT = cUIT;
            RazonSocial = razonSocial;
            Nombre = nombre;
            Email = email;
            NumTelefono = numTelefono;
        }

        public Proveedor(string cUIT, string direccion, string cuentaBancaria)
        {
            CUIT = cUIT;
            Direccion = direccion;
            CuentaBancaria = cuentaBancaria;
        }

        public Proveedor(string cUIT, string razonSocial, string nombre, string email, string numTelefono, string direccion, string cuentaBancaria) : this(cUIT, razonSocial, nombre, email, numTelefono)
        {
            Direccion = direccion;
            CuentaBancaria = cuentaBancaria;
        }
    }
}
