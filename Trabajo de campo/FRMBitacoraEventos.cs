using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;
using Services;

namespace Trabajo_de_campo
{
    public partial class FRMBitacoraEventos : Form, IObserver
    {
        BLLUsuario NegociosUsuario = new BLLUsuario();
        BLLEvento NegociosEvento = new BLLEvento();
        Negocios negocios = new Negocios();

        Dictionary<string, string> dict = new Dictionary<string, string>();
        public FRMBitacoraEventos()
        {
            InitializeComponent();
            CrearDiccionarioTraducido();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            CrearDiccionarioTraducido();
            LlenarCombobox();
            Actualizar();

            CBLogin.Text = "";
        }

        private void FRMBitacoraEventos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            LlenarCombobox();

            textBox1.Text = "";
            textBox2.Text = "";
            CBLogin.Text = "";
            CBModulo.Text = "";
            CBEvento.Text = "";
            CBCriticidad.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;
        }

        private void FRMBitacoraEventos_Load(object sender, EventArgs e)
        {
            LlenarCombobox();

            textBox1.Text = "";
            textBox2.Text = "";
            CBLogin.Text = "";
            CBModulo.Text = "";
            CBEvento.Text = "";
            CBCriticidad.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;

            Actualizar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = NegociosUsuario.ObtenerUsuario(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Nombre;
                textBox2.Text = NegociosUsuario.ObtenerUsuario(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Apellido;

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception) { }
        }

        public void Actualizar()
        {
            DataTable dt;

            //FILTRADO DE DATOS
            if (dateTimePicker1.Value == dateTimePicker1.MinDate && dateTimePicker2.Value == dateTimePicker2.MinDate && CBCriticidad.Text == "" && CBEvento.Text == "" && CBLogin.Text == "" && CBModulo.Text == "")
            {
                dt = negocios.ObtenerTabla("*", "Evento", $"CONVERT(date,Fecha) >= '{DateTime.Now.AddDays(-3).ToString("yyyy-MM-ddTHH:mm:ss.fff")}' ORDER BY CodEvento DESC");
            }
            else
            {
                dt = negocios.ObtenerTabla("*", "Evento", $"Login LIKE '{CBLogin.Text}%' AND CONVERT(date,Fecha) >= '{dateTimePicker1.Value.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND CONVERT(date,Fecha) <= '{dateTimePicker2.Value.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND Modulo LIKE '{CBModulo.SelectedValue}%' AND Evento LIKE '{CBEvento.SelectedValue}%' AND Criticidad LIKE '{CBCriticidad.Text}%' ORDER BY CodEvento DESC");
            }

            //TRADUCCIÓN DE GRILLA
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodEvento");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Login");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Fecha");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Hora");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Modulo");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Evento");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Criticidad");

            foreach(DataRow dr in dt.Rows)
            {
                try
                {
                    dr[4] = dict[dr[4].ToString()];
                    dr[5] = dict[dr[5].ToString()];
                }
                catch(Exception ex) { }
            }

            dataGridView1.DataSource = dt;
        }

        public void LlenarCombobox()
        {
            //Logins de usuarios
            CBLogin.DataSource = negocios.ObtenerTabla("Username", "Usuario");
            CBLogin.DisplayMember = "Username";
            CBLogin.ValueMember = "Username";

            //Modulos traducidos
            DataTable dt = new DataTable();
            dt.Columns.Add("Display");
            dt.Columns.Add("ValorReal");

            dt.Rows.Add("", "");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Sesiones"), "Sesiones");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Usuarios"), "Usuarios");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Perfiles"), "Perfiles");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Libros"), "Libros");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Clientes"), "Clientes");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Ventas"), "Ventas");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.BaseDeDatos"), "Base de datos");

            CBModulo.DataSource = dt;
            CBModulo.DisplayMember = "Display";
            CBModulo.ValueMember = "ValorReal";
        }

        private void BTNLimpiar_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;
            textBox1.Text = "";
            textBox2.Text = "";
            CBLogin.Text = "";
            CBModulo.Text = "";
            CBEvento.Text = "";
            CBCriticidad.Text = "";

            Actualizar();
        }

        private void CBLogin_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void CBModulo_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Display");
            dt.Columns.Add("ValorReal");

            CBEvento.DataSource = null;
            string modulo = CBModulo.SelectedValue.ToString();

            switch (modulo)
            {
                case "Sesiones":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.LogIn"), "Inicio de sesión");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.LogOut"), "Cierre de sesión");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.CambioContra"), "Cambio de contraseña");
                    break;
                case "Usuarios":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroUsuario"), "Registro de usuario");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionUsuario"), "Modificación de usuario");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoUsuario"), "Borrado lógico de usuario");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RestauracionUsuario"), "Restauración de usuario");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BloqueoUsuario"), "Bloqueo manual de usuario");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.DesbloqueoUsuario"), "Desbloqueo de usuario");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ConsultaUsuarios"), "Consulta de usuarios");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ReporteEvento"), "Generación de reporte de evento");
                    break;
                case "Perfiles":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.CreacionPerfil"), "Creación de perfil");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionPerfil"), "Modificación de perfil");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoPerfil"), "Borrado de perfil");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.CreacionFamilia"), "Creación de familia");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionFamilia"), "Modificación de familia");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoFamilia"), "Borrado de familia");
                    break;
                case "Libros":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroLibro"), "Registro de libro");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionLibro"), "Modificación de libro");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoLibro"), "Borrado lógico de libro");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RestauracionLibro"), "Restauración de libro");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ActualizacionEstadoLibro"), "Actualización de estado de libro");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ConsultaLibros"), "Consulta de libros");
                    break;
                case "Clientes":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroCliente"), "Registro de cliente");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionCliente"), "Modificación de cliente");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoCliente"), "Borrado lógico de cliente");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RestauracionCliente"), "Restauración de cliente");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ConsultaClientes"), "Consulta de clientes");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.SerializacionXMLClientes"), "Serialización XML de clientes");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.DeserializacionXMLClientes"), "Des-serialización XML de clientes");
                    break;
                case "Ventas":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.GeneracionFactura"), "Generación de factura");
                    break;
                case "Base de datos":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.Respaldo"), "Respaldo");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.Restauracion"), "Restauración");
                    break;
                default:
                    CBEvento.DataSource = null;
                    break;
            }

            CBEvento.DataSource = dt;
            CBEvento.DisplayMember = "Display";
            CBEvento.ValueMember = "ValorReal";

            Actualizar();
        }

        private void CBEvento_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void CBCriticidad_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha actual");
                dataGridView1.DataSource = null;
            }
            else if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha final");
                dataGridView1.DataSource = null;
            }
            else
            {
                Actualizar();
            }
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha actual");
                dataGridView1.DataSource = null;
            }
            else if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha final");
                dataGridView1.DataSource = null;
            }
            else
            {
                Actualizar();
            }
        }

        private void BTNImprimir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos en la grilla para imprimir");
            }
            else
            {
                try
                {
                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                    string PaginaHTML_Texto = Properties.Resources.ReporteEvento.ToString();
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

                    //DATOS DE EVENTO

                    string filas = string.Empty;

                    //

                    DataTable dt;
                    if (dateTimePicker1.Value == dateTimePicker1.MinDate && dateTimePicker2.Value == dateTimePicker2.MinDate && CBCriticidad.Text == "" && CBEvento.Text == "" && CBLogin.Text == "" && CBModulo.Text == "")
                    {
                        dt = negocios.ObtenerTabla("*", "Evento", $"CAST(Fecha AS date) >= '{DateTime.Now.AddDays(-3)}' ORDER BY CodEvento DESC");
                    }
                    else
                    {
                        dt = negocios.ObtenerTabla("*", "Evento", $"Login LIKE '{CBLogin.Text}%' AND CAST(Fecha AS date) >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND CAST(Fecha AS date) <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' AND Modulo LIKE '{CBModulo.Text}%' AND Evento LIKE '{CBEvento.Text}%' AND Criticidad LIKE '{CBCriticidad.Text}%' ORDER BY CodEvento DESC");
                    }

                    foreach (DataRow r in dt.Rows)
                    {
                        filas += "<tr>";
                        filas += "<td>" + r[0].ToString() + "</td>";
                        filas += "<td>" + r[1].ToString() + "</td>";
                        filas += "<td>" + r[2].ToString() + "</td>";
                        filas += "<td>" + r[3].ToString() + "</td>";
                        filas += "<td>" + r[4].ToString() + "</td>";
                        filas += "<td>" + r[5].ToString() + "</td>";
                        filas += "<td>" + r[6].ToString() + "</td>";
                        filas += "</tr>";
                    }
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);

                    //DATOS DE USUARIO

                    dt = negocios.ObtenerTabla("DNI, Username, (Nombre + ' ' + Apellido), Rol, FechaNac, Email, NumTelefono", "Usuario", $"Username = '{dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value}'");

                    filas = string.Empty;

                    filas += "<tr>";
                    filas += "<td>" + dt.Rows[0][0].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][1].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][2].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][3].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][4].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][5].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][6].ToString() + "</td>";
                    filas += "</tr>";

                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTEEVENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteEvento"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANTIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cantidad"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODEVENTO", dataGridView1.Columns[0].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LOGIN", dataGridView1.Columns[1].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHEVENTO", dataGridView1.Columns[2].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@HORA", dataGridView1.Columns[3].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MODULO", dataGridView1.Columns[4].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EVENTO", dataGridView1.Columns[5].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CRITICIDAD", dataGridView1.Columns[6].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@USERNAME", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Username"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ROL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Rol"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NACIMIENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaNac"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));

                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                        {
                            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();
                            pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteEvento")));

                            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
                            img.ScaleToFit(150, 150);
                            img.Alignment = iTextSharp.text.Image.UNDERLYING;

                            img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
                            pdfDoc.Add(img);


                            using (StringReader sr = new StringReader(PaginaHTML_Texto))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                            }

                            pdfDoc.Close();
                            stream.Close();
                        }

                        NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Generación de reporte de evento", 5));
                        Actualizar();

                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al imprimir: " + ex);
                }
            }
        }

        private void FRMBitacoraEventos_VisibleChanged(object sender, EventArgs e)
        {
            LlenarCombobox();

            textBox1.Text = "";
            textBox2.Text = "";
            CBLogin.Text = "";
            CBModulo.Text = "";
            CBEvento.Text = "";
            CBCriticidad.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;

            Actualizar();
        }

        //Este método sirve para crear un diccionario con todos los módulos y eventos traducidos al idioma seleccionado, de manera que se puedan mostrar en la grilla traducidos
        private void CrearDiccionarioTraducido()
        {
            dict.Clear();

            dict.Add("Sesiones", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Sesiones"));
            dict.Add("Usuarios", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Usuarios"));
            dict.Add("Perfiles", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Perfiles"));
            dict.Add("Libros", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Libros"));
            dict.Add("Clientes", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Clientes"));
            dict.Add("Ventas", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Ventas"));
            dict.Add("Base de datos", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.BaseDeDatos"));

            dict.Add("Inicio de sesión", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.LogIn"));
            dict.Add("Cierre de sesión", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.LogOut"));
            dict.Add("Cambio de contraseña", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.CambioContra"));

            dict.Add("Registro de usuario", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroUsuario"));
            dict.Add("Modificación de usuario", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionUsuario"));
            dict.Add("Borrado lógico de usuario", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoUsuario"));
            dict.Add("Restauración de usuario", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RestauracionUsuario"));
            dict.Add("Bloqueo manual de usuario", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BloqueoUsuario"));
            dict.Add("Desbloqueo de usuario", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.DesbloqueoUsuario"));
            dict.Add("Consulta de usuarios", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ConsultaUsuarios"));
            dict.Add("Generación de reporte de evento", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ReporteEvento"));

            dict.Add("Creación de perfil", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.CreacionPerfil"));
            dict.Add("Modificación de perfil", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionPerfil"));
            dict.Add("Borrado de perfil", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoPerfil"));
            dict.Add("Creación de familia", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.CreacionFamilia"));
            dict.Add("Modificación de familia", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionFamilia"));
            dict.Add("Borrado de familia", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoFamilia"));

            dict.Add("Registro de libro", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroLibro"));
            dict.Add("Modificación de libro", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionLibro"));
            dict.Add("Borrado lógico de libro", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoLibro"));
            dict.Add("Restauración de libro", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RestauracionLibro"));
            dict.Add("Actualización de estado de libro", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ActualizacionEstadoLibro"));
            dict.Add("Consulta de libros", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ConsultaLibros"));

            dict.Add("Registro de cliente", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroCliente"));
            dict.Add("Modificación de cliente", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionCliente"));
            dict.Add("Borrado lógico de cliente", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoCliente"));
            dict.Add("Restauración de cliente", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RestauracionCliente"));
            dict.Add("Consulta de clientes", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ConsultaClientes"));
            dict.Add("Serialización XML de clientes", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.SerializacionXMLClientes"));
            dict.Add("Des-serialización XML de clientes", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.DeserializacionXMLClientes"));

            dict.Add("Generación de factura", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.GeneracionFactura"));

            dict.Add("Respaldo", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.Respaldo"));
            dict.Add("Restauración", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.Restauración"));
        }
    }
}
