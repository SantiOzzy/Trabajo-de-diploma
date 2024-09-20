using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Evento
    {
        public string Login;
        public string Fecha;
        public string Hora;
        public string Modulo;
        public string Evento_;
        public int Criticidad;

        public Evento(string login, string fecha, string hora, string modulo, string evento, int criticidad)
        {
            Login = login;
            Fecha = fecha;
            Hora = hora;
            Modulo = modulo;
            Evento_ = evento;
            Criticidad = criticidad;
        }
    }
}
