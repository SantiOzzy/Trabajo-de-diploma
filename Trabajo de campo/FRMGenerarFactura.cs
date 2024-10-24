﻿using System;
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
    public partial class FRMGenerarFactura : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLFactura NegociosFactura = new BLLFactura();
        BLLEvento NegociosEvento = new BLLEvento();
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

        private void FRMGenerarFactura_Activated(object sender, EventArgs e)
        {
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
    }
}
