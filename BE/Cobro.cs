using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Cobro
    {
        public int PrecioTotal;
        public string MetodoPago;
        public string Banco;
        public string MarcaTarjeta;
        public string TipoTarjeta;

        public Cobro(int precioTotal, string metodoPago, string banco, string marcaTarjeta, string tipoTarjeta)
        {
            PrecioTotal = precioTotal;
            MetodoPago = metodoPago;
            Banco = banco;
            MarcaTarjeta = marcaTarjeta;
            TipoTarjeta = tipoTarjeta;
        }

        public Cobro(string metodoPago, string banco, string marcaTarjeta, string tipoTarjeta)
        {
            MetodoPago = metodoPago;
            Banco = banco;
            MarcaTarjeta = marcaTarjeta;
            TipoTarjeta = tipoTarjeta;
        }

        public Cobro()
        {

        }
    }
}
