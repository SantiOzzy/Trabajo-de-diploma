using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;

namespace Services
{
    public static class ReportesPDF
    {

        public static void ReporteVenta(DataTable productosComprados, string CodFact, Cobro c, Cliente cl)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Factura de venta {0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            string PaginaHTML_Texto = Properties.Resources.Plantilla.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            //DATOS DE PRODUCTOS

            string filas = string.Empty;
            decimal total = 0;

            foreach (DataRow r in productosComprados.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + r[0].ToString() + "</td>";
                filas += "<td>" + r[1].ToString() + "</td>";
                filas += "<td>" + r[2].ToString() + "</td>";
                filas += "<td>" + r[3].ToString() + "</td>";
                filas += "<td>" + r[4].ToString() + "</td>";
                filas += "</tr>";
                total += (decimal.Parse(r[3].ToString()) * decimal.Parse(r[4].ToString()));
            }
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", total.ToString());

            ////DATOS DE VENTA

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + CodFact + "</td>";
            filas += "<td>" + c.MetodoPago + "</td>";
            filas += "<td>" + c.Banco + "</td>";
            filas += "<td>" + c.MarcaTarjeta + "</td>";
            filas += "<td>" + c.TipoTarjeta + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

            ////DATOS DE CLIENTE

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + cl.DNI + "</td>";
            filas += "<td>" + cl.Nombre + "</td>";
            filas += "<td>" + cl.Direccion + "</td>";
            filas += "<td>" + cl.Email + "</td>";
            filas += "<td>" + cl.NumTelefono + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS3", filas);

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTEVENTA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteVenta"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIOTOTAL", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.Total"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANTIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cantidad"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODFACTURA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodFactura"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@METODOPAGO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MetodoPago"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@BANCO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Banco"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MARCATARJETA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MarcaTarjeta"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TIPOTARJETA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.TipoTarjeta"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRECLIENTE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NombreCliente"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DIRECCION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteFactura")));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(150, 150);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;

                    img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
                    pdfDoc.Add(img);

                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));
            }
        }

        public static void ReporteCompra(DataTable productosComprados, string CodOrden, OrdenCompra orden, Proveedor prov)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Orden de compra {0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            string PaginaHTML_Texto = Properties.Resources.ReporteOrdenCompra.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            //DATOS DE PRODUCTOS

            string filas = string.Empty;
            double total = 0;

            foreach (DataRow r in productosComprados.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + r[0].ToString() + "</td>";
                filas += "<td>" + r[1].ToString() + "</td>";
                filas += "<td>" + r[2].ToString() + "</td>";
                filas += "<td>" + r[3].ToString() + "</td>";
                filas += "<td>" + r[4].ToString() + "</td>";
                filas += "</tr>";
                total += (double.Parse(r[3].ToString()) * double.Parse(r[4].ToString()));
            }
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", (total * 1.21).ToString());

            //DATOS DE COMPRA

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + CodOrden + "</td>";
            filas += "<td>" + orden.FechaCreacion + "</td>";
            filas += "<td>" + orden.PrecioTotal + "</td>";
            filas += "<td>" + orden.NumTransaccion + "</td>";
            filas += "<td>" + orden.CodFactura + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

            //DATOS DE PROVEEDOR

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + prov.CUIT + "</td>";
            filas += "<td>" + prov.RazonSocial + "</td>";
            filas += "<td>" + prov.Nombre + "</td>";
            filas += "<td>" + prov.Email + "</td>";
            filas += "<td>" + prov.NumTelefono + "</td>";
            filas += "<td>" + prov.Direccion + "</td>";
            filas += "<td>" + prov.CuentaBancaria + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS3", filas);

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTECOMPRA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteCompra"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIOTOTAL", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.Total"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@COTIZACION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cotizacion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANTIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.StockCompra"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODORDEN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodOrdenCompra"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CREACION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaCreacion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTRANSACCION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTransaccion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODFACTURA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodFactura"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CUIT", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CUIT"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@RAZONSOCIAL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.RazonSocial"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DIRECCION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CUENTABANCARIA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CuentaBancaria"));

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteCompra")));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(150, 150);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;

                    img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
                    pdfDoc.Add(img);


                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));
            }
        }

        public static void ReporteEvento(int filasDGV, DataTable dtEvento, DataTable dtUsuario)
        {
            if (filasDGV == 0)
            {
                throw new Exception("No hay datos en la grilla para imprimir");
            }
            else
            {
                try
                {
                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = string.Format("Eventos {0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                    string PaginaHTML_Texto = Properties.Resources.ReporteEvento.ToString();
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

                    //DATOS DE EVENTO

                    string filas = string.Empty;

                    foreach (DataRow r in dtEvento.Rows)
                    {
                        filas += "<tr>";
                        filas += "<td>" + r[0].ToString() + "</td>";
                        filas += "<td>" + r[1].ToString() + "</td>";
                        filas += "<td>" + r[2].ToString() + "</td>";
                        filas += "<td>" + r[3].ToString() + "</td>";
                        filas += "<td>" + r[4].ToString() + "</td>";
                        filas += "<td>" + r[5].ToString() + "</td>";
                        filas += "<td>" + r[6].ToString() + "</td>";
                        filas += "</tr>";
                    }
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);

                    //DATOS DE USUARIO

                    filas = string.Empty;

                    filas += "<tr>";
                    filas += "<td>" + dtUsuario.Rows[0][0].ToString() + "</td>";
                    filas += "<td>" + dtUsuario.Rows[0][1].ToString() + "</td>";
                    filas += "<td>" + dtUsuario.Rows[0][2].ToString() + "</td>";
                    filas += "<td>" + dtUsuario.Rows[0][3].ToString() + "</td>";
                    filas += "<td>" + dtUsuario.Rows[0][4].ToString() + "</td>";
                    filas += "<td>" + dtUsuario.Rows[0][5].ToString() + "</td>";
                    filas += "<td>" + dtUsuario.Rows[0][6].ToString() + "</td>";
                    filas += "</tr>";

                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTEEVENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteEvento"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANTIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cantidad"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODEVENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodEvento"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LOGIN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Login"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHEVENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Fecha"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@HORA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Hora"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MODULO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Modulo"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EVENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Evento"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CRITICIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Criticidad"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@USERNAME", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Username"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ROL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Rol"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NACIMIENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaNac"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));

                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                        {
                            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();
                            pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteEvento")));

                            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
                            img.ScaleToFit(150, 150);
                            img.Alignment = iTextSharp.text.Image.UNDERLYING;

                            img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
                            pdfDoc.Add(img);


                            using (StringReader sr = new StringReader(PaginaHTML_Texto))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                            }

                            pdfDoc.Close();
                            stream.Close();
                        }
                        throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void ReporteRecepcion(DataTable productosRecibidos, string CodOrden, OrdenCompra orden, Proveedor prov)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Recepcion de productos {0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            string PaginaHTML_Texto = Properties.Resources.ReporteRecepcion.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            //DATOS DE PRODUCTOS

            string filas = string.Empty;
            double total = 0;

            foreach (DataRow r in productosRecibidos.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + r[0].ToString() + "</td>";
                filas += "<td>" + r[1].ToString() + "</td>";
                filas += "<td>" + r[2].ToString() + "</td>";
                filas += "<td>" + r[3].ToString() + "</td>";
                filas += "<td>" + r[4].ToString() + "</td>";
                filas += "<td>" + r[5].ToString() + "</td>";
                filas += "<td>" + r[6].ToString() + "</td>";
                filas += "</tr>";
                total += (double.Parse(r[3].ToString()) * double.Parse(r[4].ToString()));
            }
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", (total * 1.21).ToString());

            //DATOS DE VENTA

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + CodOrden + "</td>";
            filas += "<td>" + orden.FechaCreacion + "</td>";
            filas += "<td>" + orden.PrecioTotal + "</td>";
            filas += "<td>" + orden.NumTransaccion + "</td>";
            filas += "<td>" + orden.CodFactura + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

            //DATOS DE PROVEEDOR

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + prov.CUIT + "</td>";
            filas += "<td>" + prov.RazonSocial + "</td>";
            filas += "<td>" + prov.Nombre + "</td>";
            filas += "<td>" + prov.Email + "</td>";
            filas += "<td>" + prov.NumTelefono + "</td>";
            filas += "<td>" + prov.Direccion + "</td>";
            filas += "<td>" + prov.CuentaBancaria + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS3", filas);

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTERECEPCION", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteRecepcion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIOTOTAL", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.Total"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@COTIZACION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cotizacion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANTIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.StockCompra"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ENTREGA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaEntrega"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@STOCKRECIBIDO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.StockRecepcion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODORDEN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodOrdenCompra"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CREACION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaCreacion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTRANSACCION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTransaccion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODFACTURA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodFactura"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CUIT", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CUIT"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@RAZONSOCIAL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.RazonSocial"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DIRECCION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CUENTABANCARIA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CuentaBancaria"));

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteRecepcion")));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(150, 150);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;

                    img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
                    pdfDoc.Add(img);


                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));
            }
        }

        public static void ReporteSolicitud(DataTable productosSolicitados, string CodSolicitud, SolicitudCotizacion solicitud, Proveedor prov)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Solicitud de cotizacion {0} {1}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"), prov.Nombre);

            string PaginaHTML_Texto = Properties.Resources.ReporteSolicitudCotizacion.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            //DATOS DE PRODUCTOS

            string filas = string.Empty;

            foreach (DataRow r in productosSolicitados.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + r[0].ToString() + "</td>";
                filas += "<td>" + r[1].ToString() + "</td>";
                filas += "<td>" + r[2].ToString() + "</td>";
                filas += "</tr>";
            }
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);

            //DATOS DE COMPRA

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + CodSolicitud + "</td>";
            filas += "<td>" + solicitud.FechaEmision + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

            //DATOS DE PROVEEDOR

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + prov.CUIT + "</td>";
            filas += "<td>" + prov.RazonSocial + "</td>";
            filas += "<td>" + prov.Nombre + "</td>";
            filas += "<td>" + prov.Email + "</td>";
            filas += "<td>" + prov.NumTelefono + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS3", filas);

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTESOLICITUD", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteSolicitud"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODSOLICITUD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodOrdenCompra"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CREACION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaCreacion"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CUIT", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CUIT"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@RAZONSOCIAL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.RazonSocial"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteSolicitud")));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(150, 150);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;

                    img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
                    pdfDoc.Add(img);


                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));
            }
        }

        public static void ReporteInteligente(DataTable dt, string NombreReporte)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{1} {0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"), NombreReporte);

            string PaginaHTML_Texto = Properties.Resources.ReporteInteligente.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            string tabla = "";
            //COLUMNAS
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tabla += "<th>" + dt.Columns[i].ColumnName + "</th>";
            }
            //FILAS
            string filas = string.Empty;
            foreach (DataRow r in dt.Rows)
            {
                filas += "<tr>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    filas += "<td>" + r[i].ToString() + "</td>";
                }
                filas += "</tr>";
            }

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@COLUMNAS", tabla);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas);

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteInteligente")));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(150, 150);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;

                    img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
                    pdfDoc.Add(img);


                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));
            }
        }
    }
}