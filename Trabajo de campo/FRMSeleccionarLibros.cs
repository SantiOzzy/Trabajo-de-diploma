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
using BE;
using BLL;
using Services;

namespace Trabajo_de_campo
{
    public partial class FRMSeleccionarLibros : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLFactura NegociosFactura = new BLLFactura();
        BLLLibro NegociosLibro = new BLLLibro();
        CryptoManager Encriptar = new CryptoManager();
        FRMUI parent;

        public FRMSeleccionarLibros()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            ActualizarTabla();

            ActualizarPrecioTotal();
        }

        private void FRMRealizarVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            label5.Text = "0";

            dataGridView2.Rows.Clear();

            foreach (DataGridViewRow row in parent.FormGenerarFactura.dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    int index = dataGridView2.Rows.Add();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dataGridView2.Rows[index].Cells[i].Value = row.Cells[i].Value;
                    }
                }
            }

            ActualizarPrecioTotal();
        }

        private void FRMRegistrarVenta_VisibleChanged(object sender, EventArgs e)
        {
            ActualizarTabla();
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
                    try
                    {
                        string cantidad = Microsoft.VisualBasic.Interaction.InputBox(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.IngresarCantidad"), LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.Cantidad"));
                        NegociosLibro.ValidarLibroParaVenta(cantidad, Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value));
                        dataGridView2.Rows.Add(dataGridView1.Rows[e.RowIndex].Cells[0].Value, dataGridView1.Rows[e.RowIndex].Cells[1].Value, dataGridView1.Rows[e.RowIndex].Cells[2].Value, dataGridView1.Rows[e.RowIndex].Cells[3].Value, cantidad);
                        ActualizarPrecioTotal();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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

            label5.Text = PrecioTotal.ToString();
        }

        private void BTNSeleccionarLibros_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount == 0)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.SeleccionarProductos"));
            }
            else
            {
                parent.FormGenerarFactura.dataGridView1.DataSource = null;
                parent.FormGenerarFactura.Productos = new DataTable();

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    Type columnType = column.ValueType ?? typeof(string);
                    parent.FormGenerarFactura.Productos.Columns.Add(column.HeaderText, columnType);
                }

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow dataRow = parent.FormGenerarFactura.Productos.NewRow();

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            object cellValue = cell.Value ?? DBNull.Value;
                            dataRow[cell.ColumnIndex] = Convert.ChangeType(cellValue, parent.FormGenerarFactura.Productos.Columns[cell.ColumnIndex].DataType);
                        }

                        parent.FormGenerarFactura.Productos.Rows.Add(dataRow);
                    }
                }

                parent.FormGenerarFactura.dataGridView1.DataSource = parent.FormGenerarFactura.Productos.Copy();

                parent.fact.Cobro.PrecioTotal = Convert.ToInt32(label5.Text);

                parent.FormGenerarFactura.label8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.MontoTotal") + label5.Text;

                this.Hide();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.ProductosSeleccionados"));
            }
        }

        private void FRMSeleccionarLibros_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;
        }

        void ActualizarTabla()
        {
            dataGridView2.Columns[0].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN");
            dataGridView2.Columns[1].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor");
            dataGridView2.Columns[2].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dataGridView2.Columns[3].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio");
            dataGridView2.Columns[4].HeaderText = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cantidad");

            dataGridView1.DataSource = NegociosLibro;
        }
    }
}
