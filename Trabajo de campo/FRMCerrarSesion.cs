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

            foreach (ToolStripMenuItem itemMenu in parent.menuStrip1.Items)
            {
                itemMenu.Visible = false;

                foreach (ToolStripItem subItem in itemMenu.DropDownItems)
                {
                    if (subItem is ToolStripMenuItem)
                    {
                        subItem.Visible = false;
                    }
                }
            }

            parent.nombreToolStripMenuItem.Visible = true;
            parent.usuarioToolStripMenuItem.Visible = true;
            parent.ayudaToolStripMenuItem.Visible = true;

            parent.iniciarSesiónToolStripMenuItem.Visible = true;
            parent.cambiarIdiomaToolStripMenuItem.Visible = true;

            parent.nombreToolStripMenuItem.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCerrarSesion.Etiquetas.SinSesionIniciada");

            foreach(Form frm in Application.OpenForms.Cast<Form>().ToArray())
            {
                if (!(frm is FRMUI))
                {
                    frm.Hide();
                }
            }
        }
    }
}
