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
        BLLFactura NegociosFactura = new BLLFactura();
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
            dataGridView1.DataSource = NegociosFactura.ObtenerTablaReporte();
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
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
