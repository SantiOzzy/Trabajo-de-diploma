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
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            VaciarCampos();
        }

        void VaciarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";

            pictureBox1.BackgroundImage = Properties.Resources._3844477_disable_eye_inactive_see_show_view_watch_110343;
            MostrarContraseña = false;
            textBox2.PasswordChar = '*';
        }

        private void BTNIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                NegociosUsuario.IniciarSesion(textBox1.Text, textBox2.Text);

                Usuario user = NegociosUsuario.ObtenerUsuario(textBox1.Text);

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMIniciarSesion.Etiquetas.LogIn") + user.Rol);

                ModificarMenu(user.Rol, user.Nombre + " " + user.Apellido);

                this.Hide();
                VaciarCampos();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ModificarMenu(string rol, string nombre)
        {
            FRMUI parent = this.MdiParent as FRMUI;

            foreach(ToolStripMenuItem itemMenu in parent.menuStrip1.Items)
            {
                foreach (ToolStripItem subItem in itemMenu.DropDownItems)
                {
                    if (subItem is ToolStripMenuItem)
                    {
                        subItem.Visible = false;
                    }
                }
            }

            parent.cambiarIdiomaToolStripMenuItem.Visible = true;
            parent.cerrarSesiónToolStripMenuItem.Visible = true;
            parent.cambiarContraseñaToolStripMenuItem.Visible = true;

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

                MostrarPermisosSubFamilias(dr);
            }

            parent.nombreToolStripMenuItem.Text = nombre;
        }

        private void MostrarPermisosSubFamilias(DataRow dr1)
        {
            FRMUI parent = this.MdiParent as FRMUI;

            foreach (DataRow dr in NegociosFamilia.ObtenerFamiliasPerfilPorNombre(dr1[0].ToString()).Rows)
            {
                foreach (DataRow dr2 in NegociosFamilia.ObtenerPermisosPorNombreFamilia(dr[0].ToString()).Rows)
                {
                    OtorgarVisibilidad(parent.menuStrip1.Items, dr2[0].ToString(), parent);
                }

                MostrarPermisosSubFamilias(dr);
            }
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
