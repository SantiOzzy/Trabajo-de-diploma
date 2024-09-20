using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Cliente
    {
        public int DNI;
        public string Nombre;
        public string Apellido;
        public string Direccion;
        public string Email;
        public string NumTelefono;

        public Cliente(int dNI, string nombre, string apellido, string direccion, string email, string numTelefono)
        {
            DNI = dNI;
            Nombre = nombre;
            Apellido = apellido;
            Direccion = direccion;
            Email = email;
            NumTelefono = numTelefono;
        }
    }
}
