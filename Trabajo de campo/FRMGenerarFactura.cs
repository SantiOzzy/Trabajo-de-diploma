using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services;
using BLL;
using BE;

namespace Trabajo_de_campo
{
    public partial class FRMGenerarFactura : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLFactura NegociosFactura = new BLLFactura();
        BLLEvento NegociosEvento = new BLLEvento();
        CryptoManager Encriptacion = new CryptoManager();
        public DataTable Productos = new DataTable();
        FRMUI parent;

        public FRMGenerarFactura()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMGenerarFactura_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;
        }

        private void FRMGenerarFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            VaciarCampos();
        }

        private void BTNSeleccionarProductos_Click(object sender, EventArgs e)
        {
            parent.FormSeleccionarLibros.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {

            }
        }

        private void BTNCobrarVenta_Click(object sender, EventArgs e)
        {
            if (parent.cobro.PrecioTotal == 0 || Productos.Rows.Count == 0)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.Etiquetas.SeleccioneProductos"));
            }
            else
            {
                parent.FormCobrarVenta.Show();
            }
        }

        private void BTNGenerarFactura_Click(object sender, EventArgs e)
        {
            if (parent.cobro.PrecioTotal == 0 || Productos.Rows.Count == 0)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.Etiquetas.SeleccioneProductos"));
            }
            else if (parent.fact.DNI == 0 || parent.cobro.MetodoPago == null)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.Etiquetas.CobreVenta"));
            }
            else
            {
                parent.fact.Fecha = DateTime.Now;

                parent.fact.Cobro = parent.cobro;

                NegociosFactura.RegistrarFactura(parent.fact);

                int CodFact = NegociosFactura.ObtenerCodFactura();

                foreach (DataRow dr in Productos.Rows)
                {
                    parent.fact.Items.Add(new Item(CodFact, dr[0].ToString(), Convert.ToInt32(dr.Field<string>(4))));
                }
                NegociosFactura.RegistrarItems(parent.fact);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Ventas", "Generación de factura", 3));
                parent.FormBitacoraEventos.Actualizar();
                parent.FormBitacoraCambios.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.Etiquetas.FacturaGenerada"));

                GenerarReportePDF();

                this.Hide();

                VaciarCampos();
            }
        }

        void VaciarCampos()
        {
            parent.FormSeleccionarLibros.Hide();
            parent.FormRegistrarCliente.Hide();
            parent.FormCobrarVenta.Hide();

            parent.FormSeleccionarLibros.label5.Text = "0";

            parent.FormGenerarFactura.label1.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label1") + "-";
            parent.FormGenerarFactura.label2.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label2") + "-";
            parent.FormGenerarFactura.label3.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label3") + "-";
            parent.FormGenerarFactura.label4.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label4") + "-";
            parent.FormGenerarFactura.label5.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label5") + "-";
            parent.FormGenerarFactura.label8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label8") + "-";

            try
            {
                dataGridView1.DataSource = null;
                parent.FormSeleccionarLibros.dataGridView2.Rows.Clear();
            }
            catch (Exception) { }

            Productos.Clear();
            parent.fact = new Factura();
        }

        void GenerarReportePDF()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            string PaginaHTML_Texto = Properties.Resources.Plantilla.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            //DATOS DE PRODUCTOS

            string filas = string.Empty;
            decimal total = 0;

            DataTable dt = negocios.ObtenerTabla("Libro.ISBN, Nombre, Autor, Precio, Cantidad", "Libro INNER JOIN Item ON Libro.ISBN = Item.ISBN", $"CodFactura = {NegociosFactura.ObtenerCodFactura()}");

            foreach (DataRow r in dt.Rows)
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

            //DATOS DE VENTA

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + NegociosFactura.ObtenerCodFactura() + "</td>";
            filas += "<td>" + parent.fact.Cobro.MetodoPago + "</td>";
            filas += "<td>" + parent.fact.Cobro.Banco + "</td>";
            filas += "<td>" + parent.fact.Cobro.MarcaTarjeta + "</td>";
            filas += "<td>" + parent.fact.Cobro.TipoTarjeta + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

            //DATOS DE CLIENTE

            dt = negocios.ObtenerTabla("DNI, Nombre, Direccion, Email, NumTelefono", "Cliente", $"DNI = {parent.fact.DNI}");

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + dt.Rows[0][0].ToString() + "</td>";
            filas += "<td>" + dt.Rows[0][1].ToString() + "</td>";
            filas += "<td>" + Encriptacion.DesencriptarAES256(dt.Rows[0][2].ToString()) + "</td>";
            filas += "<td>" + dt.Rows[0][3].ToString() + "</td>";
            filas += "<td>" + dt.Rows[0][4].ToString() + "</td>";
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

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Ventas", "Generación de reporte de factura de venta", 5));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));
            }
        }
    }
}
