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
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

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

                //NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Ventas", "Generación de reporte de factura de venta", 5));
            }
        }

        public static void ReporteCompra()
        {
            //SaveFileDialog savefile = new SaveFileDialog();
            //savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            //string PaginaHTML_Texto = Properties.Resources.ReporteOrdenCompra.ToString();
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            ////DATOS DE PRODUCTOS

            //string filas = string.Empty;
            //decimal total = 0;

            //DataTable dt = negocios.ObtenerTabla("Libro.ISBN, Autor, Nombre, Cotizacion, StockCompra", "Libro INNER JOIN ItemOrden ON Libro.ISBN = ItemOrden.ISBN", $"CodOrdenCompra = {NegociosOrdenCompra.ObtenerCodOrdenCompra()}");

            //foreach (DataRow r in dt.Rows)
            //{
            //    filas += "<tr>";
            //    filas += "<td>" + r[0].ToString() + "</td>";
            //    filas += "<td>" + r[1].ToString() + "</td>";
            //    filas += "<td>" + r[2].ToString() + "</td>";
            //    filas += "<td>" + r[3].ToString() + "</td>";
            //    filas += "<td>" + r[4].ToString() + "</td>";
            //    filas += "</tr>";
            //    total += (decimal.Parse(r[3].ToString()) * decimal.Parse(r[4].ToString()));
            //}
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", total.ToString());

            ////DATOS DE VENTA

            //filas = string.Empty;

            //filas += "<tr>";
            //filas += "<td>" + NegociosOrdenCompra.ObtenerCodOrdenCompra() + "</td>";
            //filas += "<td>" + orden.FechaCreacion + "</td>";
            //filas += "<td>" + orden.PrecioTotal + "</td>";
            //filas += "<td>" + orden.NumTransaccion + "</td>";
            //filas += "</tr>";

            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

            ////DATOS DE CLIENTE

            //dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria", "Proveedor", $"CUIT = {orden.CUIT}");

            //filas = string.Empty;

            //filas += "<tr>";
            //filas += "<td>" + dt.Rows[0][0].ToString() + "</td>";
            //filas += "<td>" + dt.Rows[0][1].ToString() + "</td>";
            //filas += "<td>" + dt.Rows[0][2].ToString() + "</td>";
            //filas += "<td>" + dt.Rows[0][3].ToString() + "</td>";
            //filas += "<td>" + dt.Rows[0][4].ToString() + "</td>";
            //filas += "<td>" + dt.Rows[0][5].ToString() + "</td>";
            //filas += "<td>" + dt.Rows[0][6].ToString() + "</td>";
            //filas += "</tr>";

            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS3", filas);

            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTECOMPRA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteVenta"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIOTOTAL", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.Total"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@COTIZACION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cotizacion"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANTIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.StockCompra"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODORDEN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodOrdenCompra"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CREACION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaCreacion"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTRANSACCION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTransaccion"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CUIT", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CUIT"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@RAZONSOCIAL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.RazonSocial"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DIRECCION", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion"));
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CUENTABANCARIA", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CuentaBancaria"));

            //if (savefile.ShowDialog() == DialogResult.OK)
            //{
            //    using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
            //    {
            //        Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

            //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            //        pdfDoc.Open();
            //        pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteCompra")));

            //        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
            //        img.ScaleToFit(150, 150);
            //        img.Alignment = iTextSharp.text.Image.UNDERLYING;

            //        img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
            //        pdfDoc.Add(img);


            //        using (StringReader sr = new StringReader(PaginaHTML_Texto))
            //        {
            //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //        }

            //        pdfDoc.Close();
            //        stream.Close();
            //    }

            //    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Compras", "Generación de reporte de factura de compra", 5));
            //    FRMUI parent = this.MdiParent as FRMUI;
            //    parent.FormBitacoraEventos.Actualizar();

            //    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMOrdenCompra.Etiquetas.ReporteGenerado"));
            //}
        }

        public static void ReporteEvento()
        {
            //if (dataGridView1.Rows.Count == 0)
            //{
            //    MessageBox.Show("No hay datos en la grilla para imprimir");
            //}
            //else
            //{
            //    try
            //    {
            //        SaveFileDialog savefile = new SaveFileDialog();
            //        savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            //        string PaginaHTML_Texto = Properties.Resources.ReporteEvento.ToString();
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            //        //DATOS DE EVENTO

            //        string filas = string.Empty;

            //        //

            //        DataTable dt;
            //        if (dateTimePicker1.Value == dateTimePicker1.MinDate && dateTimePicker2.Value == dateTimePicker2.MinDate && CBCriticidad.Text == "" && CBEvento.Text == "" && CBLogin.Text == "" && CBModulo.Text == "")
            //        {
            //            dt = negocios.ObtenerTabla("*", "Evento", $"CAST(Fecha AS date) >= '{DateTime.Now.AddDays(-3)}' ORDER BY CodEvento DESC");
            //        }
            //        else
            //        {
            //            dt = negocios.ObtenerTabla("*", "Evento", $"Login LIKE '{CBLogin.Text}%' AND CAST(Fecha AS date) >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND CAST(Fecha AS date) <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' AND Modulo LIKE '{CBModulo.Text}%' AND Evento LIKE '{CBEvento.Text}%' AND Criticidad LIKE '{CBCriticidad.Text}%' ORDER BY CodEvento DESC");
            //        }

            //        foreach (DataRow r in dt.Rows)
            //        {
            //            filas += "<tr>";
            //            filas += "<td>" + r[0].ToString() + "</td>";
            //            filas += "<td>" + r[1].ToString() + "</td>";
            //            filas += "<td>" + r[2].ToString() + "</td>";
            //            filas += "<td>" + r[3].ToString() + "</td>";
            //            filas += "<td>" + r[4].ToString() + "</td>";
            //            filas += "<td>" + r[5].ToString() + "</td>";
            //            filas += "<td>" + r[6].ToString() + "</td>";
            //            filas += "</tr>";
            //        }
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);

            //        //DATOS DE USUARIO

            //        dt = negocios.ObtenerTabla("DNI, Username, (Nombre + ' ' + Apellido), Rol, FechaNac, Email, NumTelefono", "Usuario", $"Username = '{dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value}'");

            //        filas = string.Empty;

            //        filas += "<tr>";
            //        filas += "<td>" + dt.Rows[0][0].ToString() + "</td>";
            //        filas += "<td>" + dt.Rows[0][1].ToString() + "</td>";
            //        filas += "<td>" + dt.Rows[0][2].ToString() + "</td>";
            //        filas += "<td>" + dt.Rows[0][3].ToString() + "</td>";
            //        filas += "<td>" + dt.Rows[0][4].ToString() + "</td>";
            //        filas += "<td>" + dt.Rows[0][5].ToString() + "</td>";
            //        filas += "<td>" + dt.Rows[0][6].ToString() + "</td>";
            //        filas += "</tr>";

            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTEEVENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteEvento"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANTIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cantidad"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODEVENTO", dataGridView1.Columns[0].HeaderText);
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LOGIN", dataGridView1.Columns[1].HeaderText);
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHEVENTO", dataGridView1.Columns[2].HeaderText);
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@HORA", dataGridView1.Columns[3].HeaderText);
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MODULO", dataGridView1.Columns[4].HeaderText);
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EVENTO", dataGridView1.Columns[5].HeaderText);
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CRITICIDAD", dataGridView1.Columns[6].HeaderText);
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@USERNAME", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Username"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ROL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Rol"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NACIMIENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaNac"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
            //        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));

            //        if (savefile.ShowDialog() == DialogResult.OK)
            //        {
            //            using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
            //            {
            //                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

            //                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            //                pdfDoc.Open();
            //                pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteEvento")));

            //                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
            //                img.ScaleToFit(150, 150);
            //                img.Alignment = iTextSharp.text.Image.UNDERLYING;

            //                img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
            //                pdfDoc.Add(img);


            //                using (StringReader sr = new StringReader(PaginaHTML_Texto))
            //                {
            //                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //                }

            //                pdfDoc.Close();
            //                stream.Close();
            //            }

            //            NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Generación de reporte de evento", 5));
            //            Actualizar();

            //            MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));


            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error al imprimir: " + ex);
            //    }
        }
    }
}