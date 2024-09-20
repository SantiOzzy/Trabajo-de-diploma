using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        public int DNI;
        public string Nombre;
        public string Apellido;
        public DateTime FechaNac;
        public string Email;
        public string NumTelefono;
        public string Username;
        public string Contraseña;
        public string Rol;
        public bool Bloqueado;
        public bool Desactivado;

        public Usuario(int dNI, string nombre, string apellido, DateTime fechaNac, string email, string numTelefono, string username, string contraseña, string rol)
        {
            DNI = dNI;
            Nombre = nombre;
            Apellido = apellido;
            FechaNac = fechaNac;
            Email = email;
            NumTelefono = numTelefono;
            Username = username;
            Contraseña = contraseña;
            Rol = rol;
        }
    }
}
