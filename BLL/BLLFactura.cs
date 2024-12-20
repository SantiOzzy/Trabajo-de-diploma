using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using BE;
using Services;

namespace BLL
{
    public class BLLFactura
    {
        Datos Data = new Datos();
        Negocios negocios = new Negocios();
        DALFactura DatosFactura = new DALFactura();
        BLLCliente NegociosCliente = new BLLCliente();
        BLLEvento NegociosEvento = new BLLEvento();
        BLLDV NegociosDV = new BLLDV();
        CryptoManager Encriptacion = new CryptoManager();

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

        public Factura CobrarVenta(string DNI, string MedioPago, string Banco, string MarcaTarjeta, string TipoTarjeta, Factura fact)
        {
            if (!negocios.RevisarDisponibilidad(DNI, "DNI", "Cliente"))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.DNINoExiste"));
            }
            else if (!NegociosCliente.RevisarDesactivado(DNI))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.ClienteDesactivado"));
            }
            else if (MedioPago == "")
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.ElegirMedioDePago"));
            }
            else if (MedioPago != "Efectivo" && (Banco == "" || MarcaTarjeta == "" || TipoTarjeta == ""))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.LlenarCampos"));
            }
            else
            {
                fact.DNI = Convert.ToInt32(DNI);
                fact.Cobro.MetodoPago = MedioPago;

                if (MedioPago != "Efectivo")
                {
                    fact.Cobro.Banco = Banco;
                    fact.Cobro.MarcaTarjeta = MarcaTarjeta;
                    fact.Cobro.TipoTarjeta = TipoTarjeta;
                }
                else
                {
                    fact.Cobro.Banco = null;
                    fact.Cobro.MarcaTarjeta = null;
                    fact.Cobro.TipoTarjeta = null;
                }

                return fact;
            }
        }

        public void GenerarFactura(Factura fact, DataTable Productos)
        {
            if (fact.Cobro.PrecioTotal == 0 || Productos.Rows.Count == 0)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.Etiquetas.SeleccioneProductos"));
            }
            else if (fact.DNI == 0 || fact.Cobro.MetodoPago == null)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.Etiquetas.CobreVenta"));
            }
            else
            {
                fact.Fecha = DateTime.Now;

                RegistrarFactura(fact);

                int CodFact = ObtenerCodFactura();

                foreach (DataRow dr in Productos.Rows)
                {
                    fact.Items.Add(new Item(CodFact, dr[0].ToString(), Convert.ToInt32(dr.Field<string>(4))));
                }
                RegistrarItems(fact);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Ventas", "Generación de factura", 3));
            }
        }

        public DataTable ObtenerTablaReporte()
        {
            DataTable dt = negocios.ObtenerTabla("CodFactura AS [Código de factura], Fecha, PrecioTotal AS [Precio total], MetodoPago AS [Método de pago], Banco, MarcaTarjeta AS [Marca de tarjeta], TipoTarjeta AS [Tipo de tarjeta], Factura.DNI, (Nombre + ' ' + Apellido) AS [Nombre de cliente], Direccion AS Dirección, Email, NumTelefono AS [Número de teléfono]", "Factura INNER JOIN Cliente ON Cliente.DNI = Factura.DNI");

            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodFactura");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Fecha");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.PrecioTotal");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MetodoPago");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Banco");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MarcaTarjeta");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.TipoTarjeta");
            dt.Columns[7].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI");
            dt.Columns[8].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NombreCliente");
            dt.Columns[9].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion");
            dt.Columns[10].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email");
            dt.Columns[11].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono");

            foreach (DataRow dr in dt.Rows)
            {
                dr[9] = Encriptacion.DesencriptarAES256(dr[9].ToString());
            }

            return dt;
        }
    }
}
