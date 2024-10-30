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
    public partial class FRMCerrarSesion : Form, IObserver
    {
        BLLEvento NegociosEvento = new BLLEvento();
        public FRMCerrarSesion()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMCerrarSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BTNConfirmar_Click(object sender, EventArgs e)
        {
            NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Sesiones", "Cierre de sesión", 1));

            SessionManager.ObtenerInstancia().CerrarSesion();

            ModificarMenu();

            MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCerrarSesion.Etiquetas.SesionCerrada"));

            Hide();
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            Hide();
        }

        public void ModificarMenu()
        {
            FRMUI parent = this.MdiParent as FRMUI;

            parent.cerrarSesiónToolStripMenuItem.Visible = false;
            parent.cambiarContraseñaToolStripMenuItem.Visible = false;

            parent.administradorToolStripMenuItem.Visible = false;
            parent.gestionDeUsuariosToolStripMenuItem.Visible = false;
            parent.gestionDePerfilesToolStripMenuItem.Visible = false;
            parent.bitacoraDeEventosToolStripMenuItem.Visible = false;
            parent.respaldosToolStripMenuItem.Visible = false;

            parent.maestrosToolStripMenuItem.Visible = false;
            parent.gestionDeLibrosToolStripMenuItem.Visible = false;
            parent.gestionDeClientesToolStripMenuItem.Visible = false;
            parent.bitacoraDeCambiosToolStripMenuItem.Visible = false;
            parent.gestionDeProveedoresToolStripMenuItem.Visible = false;

            parent.ventaToolStripMenuItem.Visible = false;
            parent.facturarToolStripMenuItem.Visible = false;

            parent.comprasToolStripMenuItem.Visible = false;
            parent.generarSolicitudDeCotizacionToolStripMenuItem.Visible = false;
            parent.generarOrdenDeCompraToolStripMenuItem.Visible = false;
            parent.verificarRecepciónDeProductosToolStripMenuItem.Visible = false;

            parent.reportesToolStripMenuItem.Visible = false;
            parent.generarReporteToolStripMenuItem.Visible = false;

            parent.nombreToolStripMenuItem.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCerrarSesion.Etiquetas.SinSesionIniciada");

            parent.FormBitacoraCambios.Hide();
            parent.FormBitacoraEventos.Hide();
            parent.FormCambiarContraseña.Hide();
            parent.FormCambiarIdioma.Hide();
            parent.FormCerrarSesion.Hide();
            parent.FormCobrarVenta.Hide();
            parent.FormGenerarFactura.Hide();
            parent.FormGenerarReporteFactura.Hide();
            parent.FormGestionClientes.Hide();
            parent.FormGestionFamilias.Hide();
            parent.FormGestionLibros.Hide();
            parent.FormGestionPerfiles.Hide();
            parent.FormGestionUsuarios.Hide();
            parent.FormIniciarSesion.Hide();
            parent.FormRegistrarCliente.Hide();
            parent.FormRespaldos.Hide();
            parent.FormSeleccionarLibros.Hide();
            parent.FormOrdenCompra.Hide();
            parent.FormSolicitudCotizacion.Hide();
            parent.FormRecepcionProductos.Hide();
            parent.FormGestionProveedores.Hide();
        }
    }
}
