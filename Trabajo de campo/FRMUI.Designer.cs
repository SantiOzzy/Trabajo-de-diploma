
namespace Trabajo_de_campo
{
    partial class FRMUI
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMUI));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nombreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administradorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDePerfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraDeEventosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.respaldosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maestrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeLibrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraDeCambiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeProveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarIdiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ventaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarSolicitudDeCotizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarOrdenDeCompraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verificarRecepciónDeProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarReporteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nombreToolStripMenuItem,
            this.administradorToolStripMenuItem,
            this.maestrosToolStripMenuItem,
            this.usuarioToolStripMenuItem,
            this.ventaToolStripMenuItem,
            this.comprasToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1028, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nombreToolStripMenuItem
            // 
            this.nombreToolStripMenuItem.Name = "nombreToolStripMenuItem";
            this.nombreToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.nombreToolStripMenuItem.Text = "Sin sesión iniciada";
            // 
            // administradorToolStripMenuItem
            // 
            this.administradorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDeUsuariosToolStripMenuItem,
            this.gestionDePerfilesToolStripMenuItem,
            this.bitacoraDeEventosToolStripMenuItem,
            this.respaldosToolStripMenuItem});
            this.administradorToolStripMenuItem.Name = "administradorToolStripMenuItem";
            this.administradorToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.administradorToolStripMenuItem.Text = "Administrador";
            this.administradorToolStripMenuItem.Visible = false;
            // 
            // gestionDeUsuariosToolStripMenuItem
            // 
            this.gestionDeUsuariosToolStripMenuItem.Name = "gestionDeUsuariosToolStripMenuItem";
            this.gestionDeUsuariosToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.gestionDeUsuariosToolStripMenuItem.Text = "Gestión de usuarios";
            this.gestionDeUsuariosToolStripMenuItem.Visible = false;
            this.gestionDeUsuariosToolStripMenuItem.Click += new System.EventHandler(this.gestionDeUsuariosToolStripMenuItem_Click);
            // 
            // gestionDePerfilesToolStripMenuItem
            // 
            this.gestionDePerfilesToolStripMenuItem.Name = "gestionDePerfilesToolStripMenuItem";
            this.gestionDePerfilesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.gestionDePerfilesToolStripMenuItem.Text = "Gestión de perfiles";
            this.gestionDePerfilesToolStripMenuItem.Visible = false;
            this.gestionDePerfilesToolStripMenuItem.Click += new System.EventHandler(this.gestiónDePerfilesToolStripMenuItem_Click);
            // 
            // bitacoraDeEventosToolStripMenuItem
            // 
            this.bitacoraDeEventosToolStripMenuItem.Name = "bitacoraDeEventosToolStripMenuItem";
            this.bitacoraDeEventosToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bitacoraDeEventosToolStripMenuItem.Text = "Bitácora de eventos";
            this.bitacoraDeEventosToolStripMenuItem.Click += new System.EventHandler(this.bitacoraDeEventosToolStripMenuItem_Click);
            // 
            // respaldosToolStripMenuItem
            // 
            this.respaldosToolStripMenuItem.Name = "respaldosToolStripMenuItem";
            this.respaldosToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.respaldosToolStripMenuItem.Text = "Respaldos";
            this.respaldosToolStripMenuItem.Click += new System.EventHandler(this.respaldosToolStripMenuItem_Click);
            // 
            // maestrosToolStripMenuItem
            // 
            this.maestrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDeLibrosToolStripMenuItem,
            this.gestionDeClientesToolStripMenuItem,
            this.bitacoraDeCambiosToolStripMenuItem,
            this.gestionDeProveedoresToolStripMenuItem});
            this.maestrosToolStripMenuItem.Name = "maestrosToolStripMenuItem";
            this.maestrosToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.maestrosToolStripMenuItem.Text = "Maestros";
            this.maestrosToolStripMenuItem.Visible = false;
            // 
            // gestionDeLibrosToolStripMenuItem
            // 
            this.gestionDeLibrosToolStripMenuItem.Name = "gestionDeLibrosToolStripMenuItem";
            this.gestionDeLibrosToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.gestionDeLibrosToolStripMenuItem.Text = "Gestión de libros";
            this.gestionDeLibrosToolStripMenuItem.Visible = false;
            this.gestionDeLibrosToolStripMenuItem.Click += new System.EventHandler(this.gestionDeLibrosToolStripMenuItem_Click);
            // 
            // gestionDeClientesToolStripMenuItem
            // 
            this.gestionDeClientesToolStripMenuItem.Name = "gestionDeClientesToolStripMenuItem";
            this.gestionDeClientesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.gestionDeClientesToolStripMenuItem.Text = "Gestión de clientes";
            this.gestionDeClientesToolStripMenuItem.Visible = false;
            this.gestionDeClientesToolStripMenuItem.Click += new System.EventHandler(this.gestionDeClientesToolStripMenuItem_Click);
            // 
            // bitacoraDeCambiosToolStripMenuItem
            // 
            this.bitacoraDeCambiosToolStripMenuItem.Name = "bitacoraDeCambiosToolStripMenuItem";
            this.bitacoraDeCambiosToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.bitacoraDeCambiosToolStripMenuItem.Text = "Bitácora de cambios";
            this.bitacoraDeCambiosToolStripMenuItem.Click += new System.EventHandler(this.bitacoraDeCambiosToolStripMenuItem_Click);
            // 
            // gestionDeProveedoresToolStripMenuItem
            // 
            this.gestionDeProveedoresToolStripMenuItem.Name = "gestionDeProveedoresToolStripMenuItem";
            this.gestionDeProveedoresToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.gestionDeProveedoresToolStripMenuItem.Text = "Gestión de proveedores";
            this.gestionDeProveedoresToolStripMenuItem.Click += new System.EventHandler(this.gestiónDeProveedoresToolStripMenuItem_Click);
            // 
            // usuarioToolStripMenuItem
            // 
            this.usuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarSesiónToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem,
            this.cambiarContraseñaToolStripMenuItem,
            this.cambiarIdiomaToolStripMenuItem});
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            // 
            // iniciarSesiónToolStripMenuItem
            // 
            this.iniciarSesiónToolStripMenuItem.Name = "iniciarSesiónToolStripMenuItem";
            this.iniciarSesiónToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.iniciarSesiónToolStripMenuItem.Text = "Iniciar sesión";
            this.iniciarSesiónToolStripMenuItem.Click += new System.EventHandler(this.iniciarSesiónToolStripMenuItem1_Click);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar sesión";
            this.cerrarSesiónToolStripMenuItem.Visible = false;
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem1_Click);
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            this.cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            this.cambiarContraseñaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cambiarContraseñaToolStripMenuItem.Text = "Cambiar contraseña";
            this.cambiarContraseñaToolStripMenuItem.Visible = false;
            this.cambiarContraseñaToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click);
            // 
            // cambiarIdiomaToolStripMenuItem
            // 
            this.cambiarIdiomaToolStripMenuItem.Name = "cambiarIdiomaToolStripMenuItem";
            this.cambiarIdiomaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cambiarIdiomaToolStripMenuItem.Text = "Cambiar idioma";
            this.cambiarIdiomaToolStripMenuItem.Click += new System.EventHandler(this.cambiarIdiomaToolStripMenuItem_Click);
            // 
            // ventaToolStripMenuItem
            // 
            this.ventaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.facturarToolStripMenuItem});
            this.ventaToolStripMenuItem.Name = "ventaToolStripMenuItem";
            this.ventaToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.ventaToolStripMenuItem.Text = "Ventas";
            this.ventaToolStripMenuItem.Visible = false;
            // 
            // facturarToolStripMenuItem
            // 
            this.facturarToolStripMenuItem.Name = "facturarToolStripMenuItem";
            this.facturarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.facturarToolStripMenuItem.Text = "Facturar";
            this.facturarToolStripMenuItem.Visible = false;
            this.facturarToolStripMenuItem.Click += new System.EventHandler(this.registrarVentaToolStripMenuItem_Click);
            // 
            // comprasToolStripMenuItem
            // 
            this.comprasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarSolicitudDeCotizacionToolStripMenuItem,
            this.generarOrdenDeCompraToolStripMenuItem,
            this.verificarRecepciónDeProductosToolStripMenuItem});
            this.comprasToolStripMenuItem.Name = "comprasToolStripMenuItem";
            this.comprasToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.comprasToolStripMenuItem.Text = "Compras";
            this.comprasToolStripMenuItem.Visible = false;
            // 
            // generarSolicitudDeCotizacionToolStripMenuItem
            // 
            this.generarSolicitudDeCotizacionToolStripMenuItem.Name = "generarSolicitudDeCotizacionToolStripMenuItem";
            this.generarSolicitudDeCotizacionToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.generarSolicitudDeCotizacionToolStripMenuItem.Text = "Generar solicitud de cotización";
            this.generarSolicitudDeCotizacionToolStripMenuItem.Visible = false;
            this.generarSolicitudDeCotizacionToolStripMenuItem.Click += new System.EventHandler(this.generarSolicitudDeCotizacionToolStripMenuItem_Click);
            // 
            // generarOrdenDeCompraToolStripMenuItem
            // 
            this.generarOrdenDeCompraToolStripMenuItem.Name = "generarOrdenDeCompraToolStripMenuItem";
            this.generarOrdenDeCompraToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.generarOrdenDeCompraToolStripMenuItem.Text = "Generar orden de compra";
            this.generarOrdenDeCompraToolStripMenuItem.Click += new System.EventHandler(this.generarOrdenDeCompraToolStripMenuItem_Click);
            // 
            // verificarRecepciónDeProductosToolStripMenuItem
            // 
            this.verificarRecepciónDeProductosToolStripMenuItem.Name = "verificarRecepciónDeProductosToolStripMenuItem";
            this.verificarRecepciónDeProductosToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.verificarRecepciónDeProductosToolStripMenuItem.Text = "Verificar recepción de productos";
            this.verificarRecepciónDeProductosToolStripMenuItem.Click += new System.EventHandler(this.verificarRecepciónDeProductosToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarReporteToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.reportesToolStripMenuItem.Text = "Reportes";
            this.reportesToolStripMenuItem.Visible = false;
            // 
            // generarReporteToolStripMenuItem
            // 
            this.generarReporteToolStripMenuItem.Name = "generarReporteToolStripMenuItem";
            this.generarReporteToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.generarReporteToolStripMenuItem.Text = "Generar reporte";
            this.generarReporteToolStripMenuItem.Visible = false;
            this.generarReporteToolStripMenuItem.Click += new System.EventHandler(this.generarReporteToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            this.ayudaToolStripMenuItem.Click += new System.EventHandler(this.ayudaToolStripMenuItem_Click);
            // 
            // FRMUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Trabajo_de_campo.Properties.Resources.libros_usados_y_antiguos;
            this.ClientSize = new System.Drawing.Size(1028, 614);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FRMUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Librería UAI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ToolStripMenuItem nombreToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ventaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem administradorToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem maestrosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem comprasToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem iniciarSesiónToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem facturarToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem gestionDeUsuariosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem gestionDeLibrosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem generarSolicitudDeCotizacionToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem generarReporteToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem gestionDePerfilesToolStripMenuItem;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem bitacoraDeEventosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem gestionDeClientesToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem bitacoraDeCambiosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem respaldosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem gestionDeProveedoresToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem generarOrdenDeCompraToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem verificarRecepciónDeProductosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cambiarIdiomaToolStripMenuItem;
    }
}

