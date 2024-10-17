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
using BE;
using BLL;

namespace Trabajo_de_campo
{
    public partial class FRMBitacoraCambios : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLLibro_C NegociosLibro_C = new BLLLibro_C();
        BLLEvento NegociosEvento = new BLLEvento();
        public FRMBitacoraCambios()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            Actualizar();
        }

        private void BTNLimpiar_Click(object sender, EventArgs e)
        {
            CBIsbn.Text = "";
            CBNombre.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;
        }

        private void FRMBitacoraCambios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            CBIsbn.Text = "";
            CBNombre.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;
        }

        public void Actualizar()
        {
            DataTable dt;
            if (dateTimePicker1.Value == dateTimePicker1.MinDate && dateTimePicker2.Value == dateTimePicker2.MinDate && CBIsbn.Text == "" && CBNombre.Text == "")
            {
                dt = negocios.ObtenerTabla("*", "Libro_C", $"CONVERT(date,Fecha) >= '{DateTime.Now.AddMonths(-1).ToString("yyyy-MM-ddTHH:mm:ss.fff")}'");
            }
            else
            {
                dt = negocios.ObtenerTabla("*", "Libro_C", $"ISBN LIKE '{CBIsbn.Text}%' AND CONVERT(date,Fecha) >= '{dateTimePicker1.Value.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND CONVERT(date,Fecha) <= '{dateTimePicker2.Value.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND Nombre LIKE '{CBNombre.Text}%' ORDER BY CAST(Fecha + ' ' + Hora AS date) DESC");
            }
            
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Fecha");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Hora");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Stock");
            dt.Columns[7].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Activo");
            dt.Columns[8].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MaxStock");
            dt.Columns[9].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MinStock");
            dataGridView1.DataSource = dt;
        }

        private void FRMBitacoraCambios_Load(object sender, EventArgs e)
        {
            LlenarCombobox();

            CBIsbn.Text = "";
            CBNombre.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;

            Actualizar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception) { }
        }

        public void LlenarCombobox()
        {
            CBIsbn.DataSource = negocios.ObtenerTabla("ISBN", "Libro");
            CBIsbn.DisplayMember = "ISBN";
            CBIsbn.ValueMember = "ISBN";
        }

        private void CBIsbn_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void CBNombre_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha actual");
                dataGridView1.DataSource = null;
            }
            else if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha final");
                dataGridView1.DataSource = null;
            }
            else
            {
                Actualizar();
            }
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha actual");
                dataGridView1.DataSource = null;
            }
            else if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha final");
                dataGridView1.DataSource = null;
            }
            else
            {
                Actualizar();
            }
        }

        private void BTNActivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value))
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMBitacoraCambios.Etiquetas.EstadoEnUso"));
                }
                else
                {
                    //NegociosLibro_C.ActivarEstado(new Libro_C(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value)));
                    NegociosLibro_C.ActivarEstado(new Libro_C("'" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", "'" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString() + "'", "'" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'", "'" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString() + "'", "'" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString() + "'", Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value)));

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Libros", "Actualización de estado de libro", 4));
                    FRMUI parent = this.MdiParent as FRMUI;
                    parent.FormBitacoraEventos.Actualizar();

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMBitacoraCambios.Etiquetas.EstadoActivado"));

                    Actualizar();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Seleccione un evento para activar");
            }
        }
    }
}
