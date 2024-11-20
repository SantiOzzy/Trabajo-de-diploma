using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Services;
using BE;
using BLL;

namespace Trabajo_de_campo
{
    public partial class FRMGestionDeUsuarios : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLUsuario NegociosUsuario = new BLLUsuario();
        BLLFamilia NegociosFamilia = new BLLFamilia();
        BLLEvento NegociosEvento = new BLLEvento();

        CryptoManager Encriptacion = new CryptoManager();

        public FRMGestionDeUsuarios()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);

            comboBox1.DataSource = NegociosFamilia.ObtenerPerfiles();
            comboBox1.DisplayMember = "Nombre";
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            RefrescarGrilla();

            if(BTNAñadir.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir");
            }
            else if(BTNModificar.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar");
            }
            else if (BTNEliminar.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar");
            }
            else if (BTNDesbloquear.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoDesbloquear");
            }
            else
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta");
            }
        }

        public void RefrescarGrilla()
        {
            if (ObtenerCondicionesQuery().Length > 5)
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, FechaNac, Email, NumTelefono, Username, Rol, Bloqueado, Desactivado", "Usuario", ObtenerCondicionesQuery());
                dt = TraducirTabla(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, FechaNac, Email, NumTelefono, Username, Rol, Bloqueado, Desactivado", "Usuario");
                dt = TraducirTabla(dt);
                dataGridView1.DataSource = dt;
            }

            ContarUsuarios();
        }

        private void FRMGestionDeUsuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void FRMGestionDeUsuarios_VisibleChanged(object sender, EventArgs e)
        {
            RefrescarGrilla();
            ContarUsuarios();
            LimpiarCampos();

            comboBox1.DataSource = NegociosFamilia.ObtenerPerfiles();
            comboBox1.DisplayMember = "Nombre";
        }

        private void BTNAñadir_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNAñadir, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"));
        }

        private void BTNModificar_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNModificar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"));
        }

        private void BTNEliminar_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNEliminar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar"));
        }

        private void BTNDesbloquear_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNDesbloquear, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoDesbloquear"));
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"))
            {
                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Consulta de usuarios", 6));
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                if(ValidarCampos() == true)
                {
                    Usuario user = new Usuario(Convert.ToInt32(numericUpDown1.Value), textBox1.Text, textBox2.Text, dateTimePicker1.Value, textBox3.Text, textBox4.Text, textBox5.Text, Encriptacion.GetSHA256(numericUpDown1.Value.ToString() + textBox2.Text.Replace(" ", "")), comboBox1.Text);

                    NegociosUsuario.RegistrarUsuario(user);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Registro de usuario", 2));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsuarioRegistrado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                if (ValidarCampos() == true)
                {
                    Usuario user = new Usuario(Convert.ToInt32(numericUpDown1.Value), textBox1.Text, textBox2.Text, dateTimePicker1.Value, textBox3.Text, textBox4.Text, textBox5.Text, "", comboBox1.Text);

                    NegociosUsuario.ModificarUsuario(user);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Modificación de usuario", 2));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsuarioModificado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();

                    if(user.DNI == SessionManager.ObtenerInstancia().ObtenerDatosUsuario().DNI)
                    {
                        SessionManager.ObtenerInstancia().IniciarSesion(user);

                        FRMUI p = this.MdiParent as FRMUI;

                        p.FormIniciarSesion.ModificarMenu(user.Rol, user.Nombre + " " + user.Apellido);
                    }
                }
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar"))
            {
                if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Usuario") == false)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.DNINoExiste"));
                }
                else if (NegociosUsuario.RevisarDesactivado(Convert.ToInt32(numericUpDown1.Value)))
                {
                    NegociosUsuario.ActivarUsuario(Convert.ToInt32(numericUpDown1.Value));

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Restauración de usuario", 2));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsuarioRestaurado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
                else
                {
                    NegociosUsuario.DesactivarUsuario(Convert.ToInt32(numericUpDown1.Value));

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Borrado lógico de usuario", 2));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsuarioDesactivado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoDesbloquear"))
            {
                if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Usuario") == false)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.DNINoExiste"));
                }
                else if (NegociosUsuario.RevisarBloqueado(Convert.ToInt32(numericUpDown1.Value)) == false)
                {
                    NegociosUsuario.BloquearUsuario(Convert.ToInt32(numericUpDown1.Value));

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Bloqueo manual de usuario", 2));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsuarioBloqueado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
                else
                {
                    NegociosUsuario.DesbloquearUsuario(Convert.ToInt32(numericUpDown1.Value));

                    DataTable dt = negocios.ObtenerTabla("DNI, Apellido", "Usuario", $"DNI = {numericUpDown1.Value}");

                    NegociosUsuario.ModificarContraseña(Convert.ToInt32(numericUpDown1.Value), Encriptacion.GetSHA256(dt.Rows[0][0].ToString() + dt.Rows[0][1].ToString().Replace(" ", "")));

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Desbloqueo de usuario", 2));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsuarioDesbloqueado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            FRMUI parent = this.MdiParent as FRMUI;
            parent.FormBitacoraEventos.Actualizar();

            RefrescarGrilla();
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));
        }

        private string ObtenerCondicionesQuery()
        {
            //Esta función devuelve las condiciones para el WHERE de la consulta de usuarios

            string c = "";

            if(checkBox1.Checked == true)
            {
                c += "DNI = " + numericUpDown1.Value.ToString() + " AND ";
            }

            if(checkBox2.Checked == true)
            {
                c += "Nombre = '" + textBox1.Text + "' AND ";
            }

            if (checkBox3.Checked == true)
            {
                c += "Apellido = '" + textBox2.Text + "' AND ";
            }

            if (checkBox4.Checked == true)
            {
                c += "FechaNac = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND ";
            }

            if (checkBox5.Checked == true)
            {
                c += "Email = '" + textBox3.Text + "' AND ";
            }

            if (checkBox6.Checked == true)
            {
                c += "NumTelefono = '" + textBox4.Text + "' AND ";
            }

            if (checkBox7.Checked == true)
            {
                c += "Username = '" + textBox5.Text + "' AND ";
            }

            if (checkBox8.Checked == true)
            {
                c += "Rol = '" + comboBox1.Text + "' AND ";
            }

            if (checkBox9.Checked == true)
            {
                c += "Bloqueado = '" + checkBox11.Checked.ToString() + "' AND ";
            }

            if (checkBox10.Checked == true)
            {
                c += "Desactivado = '" + checkBox12.Checked.ToString() + "' AND ";
            }

            if (c.Length > 5)
            {
                c = c.Substring(0, c.Length - 5);
            }

            return c;
        }

        private void ContarUsuarios()
        {
            //Este método colorea los usuarios que tienen desactivado = 1 y cuenta la cantidad de registros que se obtienen del
            //filtro ingresado para mostrarlo en el label4.

            int cont = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    cont++;

                    if (Convert.ToInt32(row.Cells[9].Value) == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.NoHayUsuarios"));
            }

            label4.Text = cont.ToString();
        }

        private void ActivarBotones(Button b, string modo)
        {
            //Este método hace que al seleccionar un modo el botón correspondiente quede inactivo y el resto de botones queden activos.

            textBox8.Text = modo;
            BTNAñadir.Enabled = true;
            BTNModificar.Enabled = true;
            BTNEliminar.Enabled = true;
            BTNCancelar.Enabled = true;
            BTNDesbloquear.Enabled = true;

            b.Enabled = false;

            LimpiarCampos();

            if(modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"))
            {
                foreach (CheckBox cb in this.Controls.OfType<CheckBox>())
                {
                    cb.Enabled = true;
                }
            }
            else
            {
                foreach (CheckBox cb in this.Controls.OfType<CheckBox>())
                {
                    cb.Enabled = false;
                }
            }

            if(modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar") || modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoDesbloquear"))
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                dateTimePicker1.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                dateTimePicker1.Enabled = true;
                comboBox1.Enabled = true;
            }

            if(modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                textBox5.Enabled = false;
            }
        }

        private void LimpiarCampos()
        {
            //Reinicia todos los campos del formulario

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now;
            numericUpDown1.Value = numericUpDown1.Minimum;

            foreach (CheckBox cb in this.Controls.OfType<CheckBox>())
            {
                cb.Checked = false;
            }
        }

        private bool ValidarCampos()
        {
            //Valida todos los campos para verificar que los datos ingresados son válidos (Se usa al añadir y modificar usuarios)

            MailAddress Correo;
            bool MailValido = false;
            bool NumeroTelefonoValido = Regex.IsMatch(textBox4.Text, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");

            if (textBox3.Text != "")
            {
                try
                {
                    Correo = new MailAddress(textBox3.Text);
                    MailValido = true;
                }
                catch (FormatException) { }
            }

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.LlenarCampos"));
            }
            else if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Usuario") && textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.DNIEnUso"));
            }
            else if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Usuario") == false && textBox8.Text != LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.DNINoExiste"));
            }
            else if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.FechaFutura"));
            }
            else if (dateTimePicker1.Value > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.MenorDeEdad"));
            }
            else if (dateTimePicker1.Value < DateTime.Now.AddYears(-110))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.FechaAntigua"));
            }
            else if(!Regex.IsMatch(textBox5.Text, @"^\S*$"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsernameEspacios"));
            }
            else if (MailValido == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.CorreoInvalido"));
            }
            else if (negocios.RevisarDisponibilidad(textBox3.Text, "Email", "Usuario") && textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.CorreoEnUso"));
            }
            else if (negocios.RevisarDisponibilidadConExcepcion(textBox3.Text, numericUpDown1.Value.ToString(), "DNI, Email", "Usuario") && textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.CorreoEnUso"));
            }
            else if (NumeroTelefonoValido == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.NumTelInvalido"));
            }
            else if (negocios.RevisarDisponibilidad(textBox5.Text, "Username", "Usuario") && textBox8.Text != LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsernameOcupado"));
            }
            else if (negocios.RevisarDisponibilidadConExcepcion(textBox5.Text, numericUpDown1.Value.ToString(), "DNI, Username", "Usuario") && textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeUsuarios.Etiquetas.UsernameOcupado"));
            }
            else
            {
                return true;
            }

            return false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                checkBox11.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                checkBox12.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[9].Value);

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch(Exception)
            {

            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ContarUsuarios();
        }

        DataTable TraducirTabla(DataTable dt)
        {
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Apellido");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaNac");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Username");
            dt.Columns[7].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Rol");
            dt.Columns[8].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Bloqueado");
            dt.Columns[9].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Desactivado");

            return dt;
        }
    }
}
