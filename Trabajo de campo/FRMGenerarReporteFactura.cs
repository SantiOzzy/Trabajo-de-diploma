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
    public partial class FRMGenerarReporteFactura : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLEvento NegociosEvento = new BLLEvento();
        CryptoManager Encriptacion = new CryptoManager();

        public FRMGenerarReporteFactura()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            Actualizar();
        }

        private void FRMGenerarReporteFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            Actualizar();
        }

        private void FRMGenerarReporteFactura_VisibleChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void FRMGenerarReporteFactura_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        public void Actualizar()
        {
            DataTable dt = negocios.ObtenerTabla("CodFactura AS [Código de factura], Fecha, PrecioTotal AS [Precio total], MetodoPago AS [Método de pago], Banco, MarcaTarjeta AS [Marca de tarjeta], TipoTarjeta AS [Tipo de tarjeta], Factura.DNI, (Nombre + ' ' + Apellido) AS [Nombre de cliente], Direccion AS Dirección, Email, NumTelefono AS [Número de teléfono]", "Factura INNER JOIN Cliente ON Cliente.DNI = Factura.DNI");
            dt = TraducirTabla(dt);

            foreach (DataRow dr in dt.Rows)
            {
                dr[9] = Encriptacion.DesencriptarAES256(dr[9].ToString());
            }

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception) { }
        }

        private void BTNGenerarReporte_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            string PaginaHTML_Texto = Properties.Resources.Plantilla.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));
            
            //DATOS DE PRODUCTOS

            string filas = string.Empty;
            decimal total = 0;

            DataTable dt = negocios.ObtenerTabla("Libro.ISBN, Nombre, Autor, Precio, Cantidad", "Libro INNER JOIN Item ON Libro.ISBN = Item.ISBN", $"CodFactura = {dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value}");

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
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "</td>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString() + "</td>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString() + "</td>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value.ToString() + "</td>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString() + "</td>";
            filas += "</tr>";

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

            //DATOS DE CLIENTE

            filas = string.Empty;

            filas += "<tr>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value.ToString() + "</td>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[8].Value.ToString() + "</td>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[9].Value.ToString() + "</td>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[10].Value.ToString() + "</td>";
            filas += "<td>" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[11].Value.ToString() + "</td>";
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
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODFACTURA", dataGridView1.Columns[0].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@METODOPAGO", dataGridView1.Columns[3].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@BANCO", dataGridView1.Columns[4].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MARCATARJETA", dataGridView1.Columns[5].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TIPOTARJETA", dataGridView1.Columns[6].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", dataGridView1.Columns[7].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRECLIENTE", dataGridView1.Columns[8].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DIRECCION", dataGridView1.Columns[9].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", dataGridView1.Columns[10].HeaderText);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", dataGridView1.Columns[11].HeaderText);

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

        DataTable TraducirTabla(DataTable dt)
        {
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

            return dt;
        }
    }
}
