using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Services;
using BLL;
using BE;

namespace Trabajo_de_campo
{
    public partial class FRMOrdenCompra : Form, IObserver
    {
        Negocios negocios = new Negocios();
        FRMUI parent;
        public FRMOrdenCompra()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            DataTable dt = negocios.ObtenerTabla("*", "Libro", "Activo = 1");
            dt = TraducirTabla(dt);
            dataGridView1.DataSource = dt;

            ActualizarPrecioTotal();
        }

        private void FRMOrdenCompra_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            label5.Text = "0";

            dataGridView2.Rows.Clear();

            ActualizarPrecioTotal();

            parent.FormRegistrarProveedor.Hide();
            parent.FormPagarCompra.Hide();
        }

        private void FRMOrdenCompra_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;
        }

        private void BTNRegistrarProveedor_Click(object sender, EventArgs e)
        {
            parent.FormRegistrarProveedor.Show();
        }

        private void BTNRegistrarPago_Click(object sender, EventArgs e)
        {
            //if proveedor seleccionado en combobox = false -> no avanzar
            //else cargar en textbox cuenta bancaria (en FRMPagarProducto) la cuenta bancaria del proveedor seleccionado
            parent.FormPagarCompra.Show();
        }

        private void FRMOrdenCompra_VisibleChanged(object sender, EventArgs e)
        {
            RefrescarGrillas();
        }

        public void RefrescarGrillas()
        {
            DataTable dt = negocios.ObtenerTabla("*", "Libro", "Activo = 1");
            dt = TraducirTabla(dt);
            dataGridView1.DataSource = dt;

            CBProveedor.DataSource = negocios.ObtenerTabla("CUIT", "Proveedor", "Direccion IS NOT NULL");
            CBProveedor.DisplayMember = "CUIT";
            CBProveedor.ValueMember = "CUIT";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool LibroSeleccionado = false;
            try
            {
                foreach (DataGridViewRow dr in dataGridView2.Rows)
                {
                    if (dr.Cells[0].Value.ToString() == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {
                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.LibroYaSeleccionado"));
                        LibroSeleccionado = true;
                    }
                }

                if (LibroSeleccionado == false)
                {
                    string cantidad = Microsoft.VisualBasic.Interaction.InputBox(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.IngresarCantidad"), LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.Cantidad"));
                    string cotizacion = Microsoft.VisualBasic.Interaction.InputBox(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMOrdenCompra.Etiquetas.IngresarCotizacion"), LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.Cotizacion"));

                    bool ValidarNumero1 = cantidad.All(char.IsDigit);
                    bool ValidarNumero2 = cotizacion.All(char.IsDigit);

                    if (cantidad.Length >= 11)
                    {
                        cantidad = cantidad.Substring(0, 11);
                    }
                    if (cotizacion.Length >= 11)
                    {
                        cotizacion = cotizacion.Substring(0, 11);
                    }

                    if (ValidarNumero1 == false || ValidarNumero2 == false || cantidad == "" || Convert.ToInt64(cantidad) > 2147483647)
                    {
                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.NumeroInvalido"));
                    }
                    else if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value) < Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value) + Convert.ToInt32(cantidad))
                    {
                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMOrdenCompra.Etiquetas.SuperaMaxStock"));
                    }
                    else
                    {
                        dataGridView2.Rows.Add(dataGridView1.Rows[e.RowIndex].Cells[0].Value, dataGridView1.Rows[e.RowIndex].Cells[1].Value, dataGridView1.Rows[e.RowIndex].Cells[2].Value, cotizacion, cantidad);
                        ActualizarPrecioTotal();
                    }
                }

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.LibroEliminado") + dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
                dataGridView2.Rows.Remove(dataGridView2.Rows[e.RowIndex]);
                ActualizarPrecioTotal();

                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {

            }
        }

        private void ActualizarPrecioTotal()
        {
            int PrecioTotal = 0;

            try
            {
                foreach (DataGridViewRow dr in dataGridView2.Rows)
                {
                    PrecioTotal += (Convert.ToInt32(dr.Cells[3].Value) * Convert.ToInt32(dr.Cells[4].Value));
                }
            }
            catch (Exception)
            {

            }

            label6.Text = "$" + PrecioTotal.ToString();
            parent.FormPagarCompra.textBox3.Text = "$" + PrecioTotal.ToString();
        }

        DataTable TraducirTabla(DataTable dt)
        {
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Stock");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Activo");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MaxStock");
            dt.Columns[7].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MinStock");

            dataGridView2.Columns[0].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN");
            dataGridView2.Columns[1].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor");
            dataGridView2.Columns[2].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dataGridView2.Columns[3].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cotizacion");
            dataGridView2.Columns[4].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cantidad");

            return dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = negocios.ObtenerTabla("*", "Libro", $"Activo = 1 AND ISBN LIKE '{textBox1.Text}%'");
            dataGridView1.DataSource = dt;
        }

        private void BTNRegistrarOrden_Click(object sender, EventArgs e)
        {

        }
    }
}
