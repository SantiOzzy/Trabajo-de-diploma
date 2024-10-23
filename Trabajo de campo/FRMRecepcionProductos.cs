using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services;
using BLL;
using BE;

namespace Trabajo_de_campo
{
    public partial class FRMRecepcionProductos : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLOrdenCompra NegociosOrdenCompra = new BLLOrdenCompra();
        public FRMRecepcionProductos()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMRecepcionProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BTNGuardarCambios_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells[4].Value.ToString().Length > 0)
                {
                    NegociosOrdenCompra.RecibirProducto(dr.Cells[0].Value.ToString(), Convert.ToInt32(dr.Cells[4].Value), dr.Cells[5].Value.ToString());
                }
            }

            MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRecepcionProductos.Etiquetas.CambiosGuardados"));

            CargarCB();
        }

        private void CargarCB()
        {
            CBOrdenCompra.DataSource = negocios.ObtenerTabla("DISTINCT CodOrdenCompra", "ItemOrden", "FechaEntrega IS NULL");
            CBOrdenCompra.DisplayMember = "CodOrdenCompra";
            CBOrdenCompra.ValueMember = "CodOrdenCompra";

            CBOrdenCompra.SelectedItem = null;
        }

        private void FRMRecepcionProductos_VisibleChanged(object sender, EventArgs e)
        {
            CargarCB();
        }

        private void CBOrdenCompra_TextChanged(object sender, EventArgs e)
        {
            if (negocios.RevisarDisponibilidad(CBOrdenCompra.Text, "CodOrdenCompra", "ItemOrden WHERE FechaEntrega IS NULL"))
            {
                DataTable dt = negocios.ObtenerTabla("Libro.ISBN, Autor, Nombre, StockCompra, StockRecepcion, FechaEntrega", "ItemOrden INNER JOIN Libro ON Libro.ISBN = ItemOrden.ISBN", $"CodOrdenCompra = {CBOrdenCompra.Text} AND FechaEntrega IS NULL");
                dataGridView1.DataSource = dt;

                dt = negocios.ObtenerTabla("FechaCreacion, CodFactura, OrdenCompra.CUIT, Nombre", "OrdenCompra INNER JOIN Proveedor ON Proveedor.CUIT = OrdenCompra.CUIT", $"CodOrdenCompra = {CBOrdenCompra.Text}");

                textBox1.Text = dt.Rows[0][0].ToString();
                textBox2.Text = dt.Rows[0][1].ToString();
                textBox3.Text = dt.Rows[0][2].ToString();
                textBox4.Text = dt.Rows[0][3].ToString();
            }
            else
            {
                dataGridView1.DataSource = null;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string cantidadRecibida = Microsoft.VisualBasic.Interaction.InputBox(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRecepcionProductos.Etiquetas.IngresarCantidadRecibida"), LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRecepcionProductos.Etiquetas.CantidadRecibida"));

                bool ValidarNumero = cantidadRecibida.All(char.IsDigit);

                if (cantidadRecibida.Length >= 11)
                {
                    cantidadRecibida = cantidadRecibida.Substring(0, 11);
                }

                if (ValidarNumero == false || cantidadRecibida == "" || Convert.ToInt64(cantidadRecibida) > 2147483647)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.NumeroInvalido"));
                }

                dataGridView1.Rows[e.RowIndex].Cells[4].Value = cantidadRecibida;
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch(Exception ex) { }
        }
    }
}
