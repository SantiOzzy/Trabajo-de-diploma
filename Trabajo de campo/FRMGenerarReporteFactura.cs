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
            DataTable ProductosVenta = negocios.ObtenerTabla("Libro.ISBN, Nombre, Autor, Precio, Cantidad", "Libro INNER JOIN Item ON Libro.ISBN = Item.ISBN", $"CodFactura = {dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value}");
            string codFact = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            Cobro c = new Cobro(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString());
            Cliente cl = new Cliente(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[8].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[8].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[9].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[10].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[11].Value.ToString());

            ReportesPDF.ReporteVenta(ProductosVenta, codFact, c, cl);

            NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Ventas", "Generación de reporte de factura de venta", 5));
            
            FRMUI parent = this.MdiParent as FRMUI;
            parent.FormBitacoraEventos.Actualizar();

            MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));
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
