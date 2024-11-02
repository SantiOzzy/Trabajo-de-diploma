using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLOrdenCompra
    {
        Datos Data = new Datos();
        DALOrdenCompra DatosOrdenCompra = new DALOrdenCompra();
        BLLDV NegociosDV = new BLLDV();

        public void RegistrarOrdenCompra(OrdenCompra orden)
        {
            Data.EjecutarComando("InsertarOrdenCompra", $"'{orden.CUIT}', '{orden.FechaCreacion.ToString("yyyy-MM-ddTHH:mm:ss.fff")}', {orden.PrecioTotal.ToString(System.Globalization.CultureInfo.InvariantCulture)}, '{orden.NumTransaccion}'");

            NegociosDV.RecalcularDVTabla("OrdenCompra");
        }

        public int ObtenerCodOrdenCompra()
        {
            return DatosOrdenCompra.ObtenerCodOrdenCompra();
        }

        public void RegistrarItems(OrdenCompra orden)
        {
            foreach (var item in orden.Items)
            {
                Data.EjecutarComando("RegistrarItemOrden", $"'{item.ISBN}', {item.CodOrdenCompra}, {item.Cotizacion}, {item.StockCompra}");
            }

            NegociosDV.RecalcularDVTabla("ItemOrden");
        }

        public void RecibirProducto(string ISBN, int StockRecepcion, DateTime FechaEntrega, string CodFactura)
        {
            Data.EjecutarComando("RecibirProducto", $"'{ISBN}', {StockRecepcion}, '{FechaEntrega.ToString("yyyy-MM-ddTHH:mm:ss.fff")}', '{CodFactura}'");

            NegociosDV.RecalcularDVTabla("ItemOrden");
            NegociosDV.RecalcularDVTabla("Libro");
            NegociosDV.RecalcularDVTabla("Libro_C");
        }
    }
}
