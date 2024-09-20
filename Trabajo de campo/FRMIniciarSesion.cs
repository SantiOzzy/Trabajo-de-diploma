using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;
using Services;

namespace Trabajo_de_campo
{
    public partial class FRMIniciarSesion : Form, IObserver
    {
        BLLUsuario NegociosUsuario = new BLLUsuario();
        BLLFamilia NegociosFamilia = new BLLFamilia();
        BLLEvento NegociosEvento = new BLLEvento();
        CryptoManager Encriptar = new CryptoManager();

        bool MostrarContraseña = false;

        public FRMIniciarSesion()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);

            textBox1.Text = "a";
            textBox2.Text = "aaaa1111";
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMIniciarSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";

            pictureBox1.BackgroundImage = Properties.Resources._3844477_disable_eye_inactive_see_show_view_watch_110343;
            MostrarContraseña = false;
            textBox2.PasswordChar = '*';

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BTNIniciarSesion_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.LlenarCampos"));
            }
            else if (NegociosUsuario.RevisarDesactivado(textBox1.Text))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.UsuarioDesactivado"));
            }
            else if (NegociosUsuario.RevisarBloqueado(textBox1.Text))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.UsuarioBloqueado"));
            }
            else
            {
                Usuario user = NegociosUsuario.RevisarLogIn(textBox1.Text, Encriptar.GetSHA256(textBox2.Text));

                if (user == null)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.DatosIncorrectos"));
                    if (NegociosUsuario.IntentoFallido(textBox1.Text))
                    {
                        NegociosEvento.RegistrarEvento(new Evento(textBox1.Text, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Sesiones", "Bloqueo de usuario por contraseña incorrecta", 1));

                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.TresFallos"));
                    }
                }
                else
                {
                    SessionManager.ObtenerInstancia().IniciarSesion(user);

                    ModificarMenu(user.Rol, user.Nombre + " " + user.Apellido);

                    NegociosUsuario.ReiniciarIntentosFallidos(textBox1.Text);

                    this.Hide();

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Sesiones", "Inicio de sesión", 1));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.LogIn") + user.Rol);

                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        public void ModificarMenu(string rol, string nombre)
        {
            FRMUI parent = this.MdiParent as FRMUI;

            parent.cerrarSesiónToolStripMenuItem.Visible = true;
            parent.cambiarContraseñaToolStripMenuItem.Visible = true;

            parent.administradorToolStripMenuItem.Visible = false;
            parent.gestionDeUsuariosToolStripMenuItem.Visible = false;
            parent.gestionDePerfilesToolStripMenuItem.Visible = false;
            parent.bitacoraDeEventosToolStripMenuItem.Visible = false;
            parent.respaldosToolStripMenuItem.Visible = false;

            parent.maestrosToolStripMenuItem.Visible = false;
            parent.gestionDeLibrosToolStripMenuItem.Visible = false;
            parent.gestionDeClientesToolStripMenuItem.Visible = false;
            parent.bitacoraDeCambiosToolStripMenuItem.Visible = false;

            parent.ventaToolStripMenuItem.Visible = false;
            parent.facturarToolStripMenuItem.Visible = false;

            parent.comprasToolStripMenuItem.Visible = false;
            parent.generarSolicitudDeCotizacionToolStripMenuItem.Visible = false;

            parent.reportesToolStripMenuItem.Visible = false;
            parent.generarReporteToolStripMenuItem.Visible = false;

            foreach (DataRow dr in NegociosFamilia.ObtenerPermisosPorNombreFamilia(rol).Rows)
            {
                OtorgarVisibilidad(parent.menuStrip1.Items, dr[0].ToString(), parent);
            }

            foreach (DataRow dr in NegociosFamilia.ObtenerFamiliasPerfilPorNombre(rol).Rows)
            {
                foreach (DataRow dr2 in NegociosFamilia.ObtenerPermisosPorNombreFamilia(dr[0].ToString()).Rows)
                {
                    OtorgarVisibilidad(parent.menuStrip1.Items, dr2[0].ToString(), parent);
                }
            }

            parent.nombreToolStripMenuItem.Text = nombre;
        }

        public void OtorgarVisibilidad(ToolStripItemCollection items, string NombrePermiso, FRMUI parent)
        {
            foreach (ToolStripMenuItem item in items)
            {
                if (item.Name == (NombrePermiso + "ToolStripMenuItem"))
                {
                    item.Visible = true;
                    item.OwnerItem.Visible = true;
                }
                OtorgarVisibilidad(item.DropDownItems, NombrePermiso, parent);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MostrarContraseña == false)
            {
                pictureBox1.BackgroundImage = Properties.Resources._3844476_eye_see_show_view_watch_110339;
                MostrarContraseña = true;
                textBox2.PasswordChar = '\0';
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources._3844477_disable_eye_inactive_see_show_view_watch_110343;
                MostrarContraseña = false;
                textBox2.PasswordChar = '*';
            }
        }
    }
}
