using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Services;
using BLL;
using BE;

namespace Trabajo_de_campo
{
    public partial class FRMRespaldos : Form, IObserver
    {
        BLLRespaldo NegociosRespaldo = new BLLRespaldo();
        BLLEvento NegociosEvento = new BLLEvento();
        BLLDV NegociosDV = new BLLDV();
        public FRMRespaldos()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMRespaldos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void FRMRespaldos_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void FRMRespaldos_VisibleChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void PBBackup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        private void PBRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "SQL Backup Files (*.bak)|*.bak";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName; ;
            }
        }

        private void BTNRespaldar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    Path.GetFullPath(textBox1.Text);
                    NegociosRespaldo.Respaldar(textBox1.Text);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Base de datos", "Respaldo", 1));
                    FRMUI parent = this.MdiParent as FRMUI;
                    parent.FormBitacoraEventos.Actualizar();

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRespaldos.Etiquetas.BackupExitoso"));
                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRespaldos.Etiquetas.BackupError") + $": {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRespaldos.Etiquetas.BackupVacio"));
            }
        }

        private void BTNRestaurar_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                try
                {
                    if (File.Exists(textBox2.Text) && Path.GetExtension(textBox2.Text) == ".bak")
                    {
                        NegociosRespaldo.Restaurar(textBox2.Text);

                        NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Base de datos", "Restauración", 1));

                        NegociosDV.RecalcularBD();

                        FRMUI parent = this.MdiParent as FRMUI;
                        parent.FormBitacoraEventos.Actualizar();
                        parent.FormBitacoraCambios.Actualizar();
                        parent.FormGenerarReporteFactura.Actualizar();
                        parent.FormGestionClientes.RefrescarGrilla();
                        parent.FormGestionFamilias.LlenarCombobox();
                        parent.FormGestionPerfiles.LlenarCombobox();
                        parent.FormGestionLibros.RefrescarGrilla();
                        parent.FormGestionProveedores.RefrescarGrilla();
                        parent.FormGestionUsuarios.RefrescarGrilla();
                        parent.FormSolicitudCotizacion.RefrescarGrillas();
                        parent.FormOrdenCompra.RefrescarGrillas();
                        parent.FormRecepcionProductos.CargarCB();

                        parent.FormSeleccionarLibros.Hide();
                        parent.FormCobrarVenta.Hide();
                        parent.FormGenerarFactura.Hide();
                        parent.FormRegistrarCliente.Hide();
                        parent.FormRegistrarProveedor.Hide();
                        parent.FormPreRegistrarProveedor.Hide();
                        parent.FormPagarCompra.Hide();

                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRespaldos.Etiquetas.RestoreExitoso"));
                        textBox2.Text = "";
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRespaldos.Etiquetas.RestoreError") + $": {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRespaldos.Etiquetas.RestoreVacio"));
            }
        }
    }
}
