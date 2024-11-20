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
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Services;
using BE;
using BLL;

namespace Trabajo_de_campo
{
    public partial class FRMGestionDeClientes : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLCliente NegociosCliente = new BLLCliente();
        BLLEvento NegociosEvento = new BLLEvento();
        CryptoManager Encriptacion = new CryptoManager();
        Serializacion serializacion = new Serializacion();

        public FRMGestionDeClientes()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            RefrescarGrilla();

            if (BTNAñadir.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir");
            }
            else if (BTNModificar.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar");
            }
            else if (BTNEliminar.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar");
            }
            else
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta");
            }
        }

        private void FRMGestionDeClientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void FRMGestionDeClientes_VisibleChanged(object sender, EventArgs e)
        {
            RefrescarGrilla();
            LimpiarCampos();
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

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                NegociosCliente.BotonAplicar(textBox8.Text, Convert.ToInt32(numericUpDown1.Value), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

                if(ex is ApplicationException)
                {
                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    RefrescarGrilla();

                    LimpiarCampos();

                    FRMUI parent = this.MdiParent as FRMUI;
                    parent.FormBitacoraEventos.Actualizar();
                }
                if(ex is ArithmeticException)
                {
                    RefrescarGrilla();

                    FRMUI parent = this.MdiParent as FRMUI;
                    parent.FormBitacoraEventos.Actualizar();
                }
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));
        }

        private void ActivarBotones(Button b, string modo)
        {
            //Este método hace que al seleccionar un modo el botón correspondiente quede inactivo y el resto de botones queden activos.

            textBox8.Text = modo;
            BTNAñadir.Enabled = true;
            BTNModificar.Enabled = true;
            BTNEliminar.Enabled = true;
            BTNCancelar.Enabled = true;

            b.Enabled = false;

            LimpiarCampos();

            if (modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"))
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

            if (modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar"))
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
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
            numericUpDown1.Value = numericUpDown1.Minimum;

            foreach (CheckBox cb in this.Controls.OfType<CheckBox>())
            {
                cb.Checked = false;
            }
        }

        public void RefrescarGrilla()
        {
            if (NegociosCliente.ObtenerCondicionesQuery(numericUpDown1.Value.ToString(), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, checkBox12.ToString(), checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, checkBox5.Checked, checkBox6.Checked, checkBox7.Checked).Length > 5)
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente", NegociosCliente.ObtenerCondicionesQuery(numericUpDown1.Value.ToString(), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, checkBox12.ToString(), checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, checkBox5.Checked, checkBox6.Checked, checkBox7.Checked));
                dt = NegociosCliente.TraducirTablaClientes(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
                }

                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente");
                dt = NegociosCliente.TraducirTablaClientes(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
                }

                dataGridView1.DataSource = dt;
            }

            ContarClientes();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {

            }
        }

        private void ContarClientes()
        {
            int cont = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    cont++;

                    if (Convert.ToInt32(row.Cells[6].Value) == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ErrorEnConteo"));
            }

            label4.Text = cont.ToString();
        }

        private void BTNSerializar_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = $"XML Files|*.xml";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    serializacion.SerializarXML(saveFileDialog1.FileName, dataGridView1);

                    MostrarXmlEnListbox(saveFileDialog1.FileName);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Serialización XML de clientes", 5));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DatosSerializados"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{"FRMGestionDeClientes.Etiquetas.ErrorSerializacion"}: {ex.Message}\n{"FRMGestionDeClientes.Etiquetas.Detalles"}: {ex.InnerException?.Message}");
            }
        }

        private void BTNDesserializar_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = $"XML Files|*.xml";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = serializacion.DeserializarXML(openFileDialog1.FileName);

                    dataGridView1.DataSource = dt;
                    ContarClientes();

                    listBox1.Items.Clear();

                    foreach(DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add($"{LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI")}: {dr[0]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre")}: {dr[1]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Apellido")}: {dr[2]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion")}: {dr[3]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email")}: {dr[4]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono")}: {dr[5]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Activo")}: {dr[6]}");
                    }

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Des-serialización XML de clientes", 5));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DatosDeserializados"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{"FRMGestionDeClientes.Etiquetas.ErrorDeserializacion"}: {ex.Message}\n{"FRMGestionDeClientes.Etiquetas.Detalles"}: {ex.InnerException?.Message}");
            }
        }

        private void MostrarXmlEnListbox(string path)
        {
            listBox1.Items.Clear();

            string[] lineas = File.ReadAllLines(path);

            foreach (string linea in lineas)
            {
                listBox1.Items.Add(linea);
            }
        }

        private void BTNLimpiar_Click(object sender, EventArgs e)
        {
            DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente");
            dt = NegociosCliente.TraducirTablaClientes(dt);

            foreach (DataRow dr in dt.Rows)
            {
                dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
            }

            dataGridView1.DataSource = dt;

            ContarClientes();
        }
    }
}
