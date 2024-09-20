using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using Services;

namespace BLL
{
    public class BLLEvento
    {
        Datos Data = new Datos();
        public void RegistrarEvento(Evento e)
        {
            Data.EjecutarComando("InsertarEvento", $"{e.Login}, '{e.Fecha}', '{e.Hora}', '{e.Modulo}', '{e.Evento_}', '{e.Criticidad}'");
        }
    }
}
