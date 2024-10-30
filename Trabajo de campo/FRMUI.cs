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

namespace Trabajo_de_campo
{
    public partial class FRMUI : Form, IObserver
    {
        public FRMCambiarContraseña FormCambiarContraseña = new FRMCambiarContraseña();
        public FRMCerrarSesion FormCerrarSesion = new FRMCerrarSesion();
        public FRMIniciarSesion FormIniciarSesion = new FRMIniciarSesion();
        public FRMCambiarIdioma FormCambiarIdioma = new FRMCambiarIdioma();
        public FRMSeleccionarLibros FormSeleccionarLibros = new FRMSeleccionarLibros();
        public FRMRegistrarCliente FormRegistrarCliente = new FRMRegistrarCliente();
        public FRMGestionDeUsuarios FormGestionUsuarios = new FRMGestionDeUsuarios();
        public FRMGestionDePerfiles FormGestionPerfiles = new FRMGestionDePerfiles();
        public FRMGestionDeFamilias FormGestionFamilias = new FRMGestionDeFamilias();
        public FRMGestionDeLibros FormGestionLibros = new FRMGestionDeLibros();
        public FRMGestionDeClientes FormGestionClientes = new FRMGestionDeClientes();
        public FRMCobrarVenta FormCobrarVenta = new FRMCobrarVenta();
        public FRMGenerarFactura FormGenerarFactura = new FRMGenerarFactura();
        public FRMGenerarReporteFactura FormGenerarReporteFactura = new FRMGenerarReporteFactura();
        public FRMBitacoraEventos FormBitacoraEventos = new FRMBitacoraEventos();
        public FRMBitacoraCambios FormBitacoraCambios = new FRMBitacoraCambios();
        public FRMRespaldos FormRespaldos = new FRMRespaldos();
        public FRMSolicitudCotizacion FormSolicitudCotizacion = new FRMSolicitudCotizacion();
        public FRMPreRegistrarProveedor FormPreRegistrarProveedor = new FRMPreRegistrarProveedor();
        public FRMOrdenCompra FormOrdenCompra = new FRMOrdenCompra();
        public FRMRegistrarProveedor FormRegistrarProveedor = new FRMRegistrarProveedor();
        public FRMPagarCompra FormPagarCompra = new FRMPagarCompra();
        public FRMRecepcionProductos FormRecepcionProductos = new FRMRecepcionProductos();
        public FRMGestionDeProveedores FormGestionProveedores = new FRMGestionDeProveedores();

        public Factura fact = new Factura();
        public Cobro cobro = new Cobro();

        public FRMUI()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void iniciarSesiónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SessionManager.ObtenerInstancia().ObtenerDatosUsuario() != null)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMUI.Etiquetas.SesionOcupada"));
            }
            else
            {
                FormIniciarSesion.MdiParent = this;
                FormIniciarSesion.Show();
            }
        }

        private void cerrarSesiónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCerrarSesion.MdiParent = this;
            FormCerrarSesion.Show();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCambiarContraseña.MdiParent = this;
            FormCambiarContraseña.Show();
        }

        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCambiarIdioma.MdiParent = this;
            FormCambiarIdioma.Show();
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionUsuarios.MdiParent = this;
            FormGestionUsuarios.Show();
        }

        private void gestiónDePerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionPerfiles.MdiParent = this;
            FormGestionFamilias.MdiParent = this;
            FormGestionPerfiles.Show();
        }

        private void bitacoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBitacoraEventos.MdiParent = this;
            FormBitacoraEventos.Show();
        }

        private void respaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRespaldos.MdiParent = this;
            FormRespaldos.Show();
        }

        private void gestionDeLibrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormGenerarFactura.Visible == false)
            {
                FormGestionLibros.MdiParent = this;
                FormGestionLibros.Show();
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMUI.Etiquetas.CerrarVenta"));
            }
        }

        private void gestionDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionClientes.MdiParent = this;
            FormGestionClientes.Show();
        }

        private void bitacoraDeCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBitacoraCambios.MdiParent = this;
            FormBitacoraCambios.Show();
        }

        private void gestiónDeProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionProveedores.MdiParent = this;
            FormGestionProveedores.Show();
        }

        private void registrarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormGestionLibros.Visible == false)
            {
                FormCobrarVenta.MdiParent = this;
                FormSeleccionarLibros.MdiParent = this;
                FormRegistrarCliente.MdiParent = this;
                FormGenerarFactura.MdiParent = this;
                FormGenerarFactura.Show();
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMUI.Etiquetas.CerrarLibros"));
            }
        }

        private void generarSolicitudDeCotizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSolicitudCotizacion.MdiParent = this;
            FormPreRegistrarProveedor.MdiParent = this;
            FormSolicitudCotizacion.Show();
        }

        private void generarOrdenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOrdenCompra.MdiParent = this;
            FormRegistrarProveedor.MdiParent = this;
            FormPagarCompra.MdiParent = this;
            FormOrdenCompra.Show();
        }

        private void verificarRecepciónDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRecepcionProductos.MdiParent = this;
            FormRecepcionProductos.Show();
        }

        private void generarReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGenerarReporteFactura.MdiParent = this;
            FormGenerarReporteFactura.Show();
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMUI.Etiquetas.Ayuda"));
        }
    }
}
