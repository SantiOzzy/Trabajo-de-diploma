using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Services;
using BE;
using System.IO;

namespace Trabajo_de_campo
{
    public partial class FRMUI : Form, IObserver
    {
        internal FRMCambiarContraseña FormCambiarContraseña = new FRMCambiarContraseña();
        internal FRMCerrarSesion FormCerrarSesion = new FRMCerrarSesion();
        internal FRMIniciarSesion FormIniciarSesion = new FRMIniciarSesion();
        internal FRMCambiarIdioma FormCambiarIdioma = new FRMCambiarIdioma();
        internal FRMSeleccionarLibros FormSeleccionarLibros = new FRMSeleccionarLibros();
        internal FRMRegistrarCliente FormRegistrarCliente = new FRMRegistrarCliente();
        internal FRMGestionDeUsuarios FormGestionUsuarios = new FRMGestionDeUsuarios();
        internal FRMGestionDePerfiles FormGestionPerfiles = new FRMGestionDePerfiles();
        internal FRMGestionDeFamilias FormGestionFamilias = new FRMGestionDeFamilias();
        internal FRMGestionDeLibros FormGestionLibros = new FRMGestionDeLibros();
        internal FRMGestionDeClientes FormGestionClientes = new FRMGestionDeClientes();
        internal FRMCobrarVenta FormCobrarVenta = new FRMCobrarVenta();
        internal FRMGenerarFactura FormGenerarFactura = new FRMGenerarFactura();
        internal FRMGenerarReporteFactura FormGenerarReporteFactura = new FRMGenerarReporteFactura();
        internal FRMBitacoraEventos FormBitacoraEventos = new FRMBitacoraEventos();
        internal FRMBitacoraCambios FormBitacoraCambios = new FRMBitacoraCambios();
        internal FRMRespaldos FormRespaldos = new FRMRespaldos();
        internal FRMSolicitudCotizacion FormSolicitudCotizacion = new FRMSolicitudCotizacion();
        internal FRMPreRegistrarProveedor FormPreRegistrarProveedor = new FRMPreRegistrarProveedor();
        internal FRMOrdenCompra FormOrdenCompra = new FRMOrdenCompra();
        internal FRMRegistrarProveedor FormRegistrarProveedor = new FRMRegistrarProveedor();
        internal FRMPagarCompra FormPagarCompra = new FRMPagarCompra();
        internal FRMRecepcionProductos FormRecepcionProductos = new FRMRecepcionProductos();
        internal FRMGestionDeProveedores FormGestionProveedores = new FRMGestionDeProveedores();
        internal FRMReparacionBD FormReparacionBD = new FRMReparacionBD();
        internal FRMReporteInteligente FormReporteInteligente = new FRMReporteInteligente();

        public Factura fact = new Factura();

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
                FormReparacionBD.MdiParent = this;
                FormRespaldos.MdiParent = this;
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

        private void generarReporteInteligenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReporteInteligente.MdiParent = this;
            FormReporteInteligente.Show();
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionManager.ObtenerInstancia().ObtenerDatosUsuario() != null)
                {
                    if (SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Rol == "Admin")
                    {
                        string rutaTemporal = Path.Combine(Path.GetTempPath(), "Ayuda.pdf");

                        File.WriteAllBytes(rutaTemporal, Properties.Resources.AyudaAdmin);

                        Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
                    }
                    else
                    {
                        string rutaTemporal = Path.Combine(Path.GetTempPath(), "Ayuda.pdf");

                        File.WriteAllBytes(rutaTemporal, Properties.Resources.AyudaUser);

                        Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void gestionDeUsuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GDU.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Gestión_de_usuarios);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void gestionDePerfilesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GDP.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Gestión_de_perfiles);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void bitacoraDeEventosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "BDE.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Bitácora_de_eventos);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void respaldosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "R.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Respaldos);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void gestionDeLibrosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GDL.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Maestros_negocio);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void gestionDeClientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GDC.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Maestros_negocio);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void bitacoraDeCambiosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "BDC.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Bitácora_de_cambios);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void gestionDeProveedoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GDP.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Maestros_negocio);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "IS.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Iniciar_sesión);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "CS.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Cerrar_sesión);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void cambiarContraseñaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "CC.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Cambiar_contraseña);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void cambiarIdiomaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "CI.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Cambiar_idioma);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void facturarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "F.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Facturar);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void generarSolicitudDeCotizacionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GSDC.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Generar_solicitud_de_cotización);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void generarOrdenDeCompraToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GODC.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Generar_orden_de_compra);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void verificarRecepciónDeProductosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "VRDP.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Verificar_recepción_de_productos);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void generarReporteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GRV.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Generar_reporte_de_venta);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }

        private void generarReporteInteligenteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string rutaTemporal = Path.Combine(Path.GetTempPath(), "GRI.pdf");

            File.WriteAllBytes(rutaTemporal, Properties.Resources.Generar_reporte_inteligente);

            Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
        }
    }
}
