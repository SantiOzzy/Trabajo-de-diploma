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
    public class BLLFactura
    {
        Datos Data = new Datos();
        DALFactura DatosFactura = new DALFactura();
        public void RegistrarFactura(Factura fact)
        {
            if (fact.Cobro.MetodoPago == "Efectivo")
            {
                Data.EjecutarComando("InsertarFacturaEfectivo", $"'{fact.Fecha.ToString("yyyy-MM-ddTHH:mm:ss.fff")}', {fact.Cobro.PrecioTotal}, '{fact.Cobro.MetodoPago}', {fact.DNI}");
            }
            else
            {
                Data.EjecutarComando("InsertarFacturaTarjeta", $"'{fact.Fecha.ToString("yyyy-MM-ddTHH:mm:ss.fff")}', {fact.Cobro.PrecioTotal}, '{fact.Cobro.MetodoPago}', '{fact.Cobro.Banco}', '{fact.Cobro.MarcaTarjeta}', '{fact.Cobro.TipoTarjeta}', {fact.DNI}");
            }
        }

        public int ObtenerCodFactura()
        {
            return DatosFactura.ObtenerCodFactura();
        }

        public void RegistrarItem(int CodFactura, string ISBN, int Cantidad)
        {
            Data.EjecutarComando("RegistrarItem", $"{CodFactura}, '{ISBN}', {Cantidad}");
        }
    }
}
