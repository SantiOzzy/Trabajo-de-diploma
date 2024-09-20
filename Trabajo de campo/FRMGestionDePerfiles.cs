using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Services;

namespace Trabajo_de_campo
{
    public partial class FRMGestionDePerfiles : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLFamilia NegociosFamilia = new BLLFamilia();
        BLLPermiso NegociosPermiso = new BLLPermiso();
        BLLEvento NegociosEvento = new BLLEvento();

        bool CambioManual = true;

        FRMUI parent;

        public FRMGestionDePerfiles()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMGestionDePerfiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            parent.FormGestionFamilias.Hide();
            treeView1.Nodes.Clear();
            TXTPerfil.Text = "";
            parent.FormGestionFamilias.TXTFamilia.Text = "";
        }

        private void FRMGestionDePerfiles_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;

            LlenarCombobox();
        }

        private void BTNConfigurarFamilias_Click(object sender, EventArgs e)
        {
            parent.FormGestionFamilias.Show();
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

            CBPerfiles.DataSource = NegociosFamilia.ObtenerPerfiles();
            CBPerfiles.DisplayMember = "Nombre";
            CBFamilias.ValueMember = "CodFamilia";

            CambioManual = true;
        }

        private void BTNCrearPerfil_Click(object sender, EventArgs e)
        {
            if (TXTPerfil.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.IngreseNombre"));
            }
            else if (negocios.RevisarDisponibilidad(TXTPerfil.Text, "Nombre", "Familia") == false)
            {
                int CodPerfil = NegociosFamilia.ObtenerSiguienteCodigo();

                NegociosFamilia.CrearComponente(CodPerfil, TXTPerfil.Text, true);

                LlenarCombobox();

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Perfiles", "Creación de perfil", 2));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.PerfilCreado"));

                parent.FormGestionUsuarios.comboBox1.DataSource = NegociosFamilia.ObtenerPerfiles();
                parent.FormGestionUsuarios.comboBox1.DisplayMember = "Nombre";
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.ComponenteYaExiste"));
            }
        }

        private void BTNEliminarPerfil_Click(object sender, EventArgs e)
        {
            if (TXTPerfil.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.IngreseNombre"));
            }
            else if (negocios.RevisarDisponibilidad(TXTPerfil.Text, "Rol", "Usuario"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.PerfilEnUso"));
            }
            else if (negocios.RevisarDisponibilidad(TXTPerfil.Text, "Nombre", "Familia") && NegociosFamilia.VerificarTipo(TXTPerfil.Text) == true)
            {
                NegociosFamilia.EliminarRegistro(TXTPerfil.Text, "Nombre", "Familia");

                LlenarCombobox();

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Perfiles", "Borrado de perfil", 2));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.PerfilEliminado"));

                parent.FormGestionUsuarios.comboBox1.DataSource = NegociosFamilia.ObtenerPerfiles();
                parent.FormGestionUsuarios.comboBox1.DisplayMember = "Nombre";
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.PerfilNoExiste"));
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo hitTest = treeView1.HitTest(e.Location);

            if (hitTest.Location != TreeViewHitTestLocations.PlusMinus)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent != treeView1.Nodes[0])
                    {
                        treeView1.Nodes.Remove(e.Node.Parent);
                    }
                    else
                    {
                        treeView1.Nodes.Remove(e.Node);
                    }
                }
            }
        }

        private void BTNAgregarFamilia_Click(object sender, EventArgs e)
        {
            bool FamiliaYaAgregada = false;
            bool PermisoYaAgregado = false;

            try
            {
                foreach (TreeNode n in treeView1.Nodes[0].Nodes)
                {
                    if (n.Text == CBFamilias.Text)
                    {
                        FamiliaYaAgregada = true;
                    }
                }

                if (FamiliaYaAgregada == false)
                {
                    TreeNode nodo = treeView1.Nodes[0].Nodes.Add(CBFamilias.Text);

                    foreach (DataRow dr2 in NegociosFamilia.ObtenerPermisosPorNombreFamilia(CBFamilias.Text).Rows)
                    {
                        foreach (TreeNode n in treeView1.Nodes[0].Nodes)
                        {
                            if (n.Text == dr2[0].ToString())
                            {
                                PermisoYaAgregado = true;
                            }
                            else if (n.Nodes.Count != 0)
                            {
                                foreach (TreeNode n1 in n.Nodes)
                                {
                                    if (n1.Text == dr2[0].ToString())
                                    {
                                        PermisoYaAgregado = true;
                                    }
                                }
                            }
                        }

                        nodo.Nodes.Add(dr2[0].ToString());
                    }
                }
                else
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.FamiliaYaSeleccionada"));
                }

                if(PermisoYaAgregado == true)
                {
                    foreach (TreeNode n in treeView1.Nodes[0].Nodes)
                    {
                        if (n.Text == CBFamilias.Text)
                        {
                            treeView1.Nodes.Remove(n);
                        }
                    }

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.PermisoDeFamiliaRepetido"));
                }
            }
            catch (Exception) { }
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
                    else if(n.Nodes.Count != 0)
                    {
                        foreach (TreeNode n1 in n.Nodes)
                        {
                            if (n1.Text == CBPermisos.Text)
                            {
                                PermisoYaAgregado = true;
                            }
                        }
                    }
                }

                if (PermisoYaAgregado == false)
                {
                    treeView1.Nodes[0].Nodes.Add(CBPermisos.Text);
                }
                else
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.PermisoYaSeleccionado"));
                }
            }
            catch (Exception) { }
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                NegociosFamilia.ActualizarPerfil(treeView1.Nodes[0], CBPermisos, CBFamilias, CBPerfiles);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Perfiles", "Modificación de perfil", 2));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.CambiosGuardados"));

                parent.FormIniciarSesion.ModificarMenu(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Rol, SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Nombre + " " + SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Apellido);
            }
            catch (Exception) { MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.ErrorAlGuardar")); }
        }

        private void CBPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count == 0 || treeView1.Nodes[0].Text != CBPerfiles.Text)
            {
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(CBPerfiles.Text);

                DataRowView r = CBPerfiles.SelectedItem as DataRowView;

                foreach (DataRow dr in NegociosFamilia.ObtenerPermisosFamilia(r.Row[0].ToString()).Rows)
                {
                    treeView1.Nodes[0].Nodes.Add(dr[0].ToString());
                }

                foreach (DataRow dr in NegociosFamilia.ObtenerFamiliasPerfil(r.Row[0].ToString()).Rows)
                {
                    TreeNode nodo = treeView1.Nodes[0].Nodes.Add(dr[0].ToString());

                    foreach (DataRow dr2 in NegociosFamilia.ObtenerPermisosPorNombreFamilia(dr[0].ToString()).Rows)
                    {
                        nodo.Nodes.Add(dr2[0].ToString());
                    }
                }
            }
            else
            {
                if (CambioManual == true)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDePerfiles.Etiquetas.PerfilYaSeleccionado"));
                }
            }
        }

        private void FRMGestionDePerfiles_VisibleChanged(object sender, EventArgs e)
        {
            CambioManual = false;
            LlenarCombobox();
            CambioManual = true;
        }
    }
}
