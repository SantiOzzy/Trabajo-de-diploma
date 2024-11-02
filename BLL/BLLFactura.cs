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
        BLLDV NegociosDV = new BLLDV();
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

            NegociosDV.RecalcularDVTabla("Factura");
        }

        public int ObtenerCodFactura()
        {
            return DatosFactura.ObtenerCodFactura();
        }

        public void RegistrarItems(Factura fact)
        {
            foreach (var item in fact.Items)
            {
                Data.EjecutarComando("RegistrarItem", $"{item.CodFactura}, '{item.ISBN}', {item.Cantidad}");
            }

            NegociosDV.RecalcularDVTabla("Item");
            NegociosDV.RecalcularDVTabla("Libro");
            NegociosDV.RecalcularDVTabla("Libro_C");
        }
    }
}
