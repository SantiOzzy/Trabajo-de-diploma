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

//CBPermisos.SelectedValue.ToString()

namespace Trabajo_de_campo
{
    public partial class FRMGestionDeFamilias : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLFamilia NegociosFamilia = new BLLFamilia();
        BLLPermiso NegociosPermiso = new BLLPermiso();
        BLLEvento NegociosEvento = new BLLEvento();

        bool CambioManual = true;

        FRMUI parent;

        public FRMGestionDeFamilias()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMGestionDeFamilias_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            treeView1.Nodes.Clear();
            TXTFamilia.Text = "";
        }

        public void LlenarCombobox()
        {
            CambioManual = false;

            CBPermisos.DataSource = NegociosPermiso.ObtenerPermisos();
            CBPermisos.DisplayMember = "Nombre";
            CBPermisos.ValueMember = "CodPermiso";

            CBFamilias.DataSource = NegociosFamilia.ObtenerFamilias();
            CBFamilias.DisplayMember = "Nombre";
            CBFamilias.ValueMember = "CodFamilia";

            CambioManual = true;
        }

        private void FRMGestionDeFamilias_Load(object sender, EventArgs e)
        {
            LlenarCombobox();
            parent = this.MdiParent as FRMUI;
        }

        private void BTNCrearFamilia_Click(object sender, EventArgs e)
        {
            if(TXTFamilia.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.IngresarNombreFamilia"));
            }
            else if (negocios.RevisarDisponibilidad(TXTFamilia.Text, "Nombre", "Familia") == false)
            {
                int CodFamilia = NegociosFamilia.ObtenerSiguienteCodigo();

                NegociosFamilia.CrearComponente(CodFamilia, TXTFamilia.Text, false);

                LlenarCombobox();

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Perfiles", "Creación de familia", 2));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.FamiliaCreada"));

                parent.FormGestionPerfiles.LlenarCombobox();
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.ComponenteYaExiste"));
            }
        }

        private void BTNAgregarPermiso_Click(object sender, EventArgs e)
        {
            bool PermisoYaAgregado = false;

            try
            {
                foreach (TreeNode n in treeView1.Nodes[0].Nodes)
                {
                    if (n.Text == CBPermisos.Text)
                    {
                        PermisoYaAgregado = true;
                    }
                }

                if (PermisoYaAgregado == false)
                {
                    treeView1.Nodes[0].Nodes.Add(CBPermisos.Text);
                }
                else
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.PermisoYaSeleccionado"));
                }
            }
            catch (Exception) { }
        }

        private void CBFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count == 0 || treeView1.Nodes[0].Text != CBFamilias.Text)
            {
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(CBFamilias.Text);

                DataRowView r = CBFamilias.SelectedItem as DataRowView;

                foreach (DataRow dr in NegociosFamilia.ObtenerPermisosFamilia(r.Row[0].ToString()).Rows)
                {
                    treeView1.Nodes[0].Nodes.Add(dr[0].ToString());
                }
            }
            else
            {
                if (CambioManual == true)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.FamiliaYaSeleccionada"));
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                treeView1.Nodes.Remove(e.Node);
            }
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                NegociosFamilia.ActualizarFamilia(treeView1.Nodes[0], CBPermisos, CBFamilias);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Perfiles", "Modificación de familia", 2));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.CambiosGuardados"));

                parent.FormIniciarSesion.ModificarMenu(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Rol, SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Nombre + " " + SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Apellido);
            }
            catch (Exception) { MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.ErrorAlGuardar")); }
        }

        private void BTNEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (TXTFamilia.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.IngresarNombreFamilia"));
            }
            else if(negocios.RevisarDisponibilidad(TXTFamilia.Text, "F2.Nombre", "Familia INNER JOIN Familia F2 ON Familia.CodComp = F2.CodFamilia"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.FamiliaEnUso"));
            }
            else if (negocios.RevisarDisponibilidad(TXTFamilia.Text, "Nombre", "Familia") && NegociosFamilia.VerificarTipo(TXTFamilia.Text) == false)
            {
                NegociosFamilia.EliminarRegistro(TXTFamilia.Text, "Nombre", "Familia");

                LlenarCombobox();

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Perfiles", "Borrado de familia", 2));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.FamiliaEliminada"));

                parent.FormGestionPerfiles.LlenarCombobox();
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeFamilias.Etiquetas.FamiliaNoExiste"));
            }
        }

        private void FRMGestionDeFamilias_VisibleChanged(object sender, EventArgs e)
        {
            CambioManual = false;
            LlenarCombobox();
            CambioManual = true;
        }
    }
}
