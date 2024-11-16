using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.Data;
using System.Threading.Tasks;
using DAL;
using BE;
using Services;

namespace BLL
{
    public class BLLEvento
    {
        Datos Data = new Datos();
        Negocios negocios = new Negocios();
        public void RegistrarEvento(Evento e)
        {
            Data.EjecutarComando("InsertarEvento", $"{e.Login}, '{e.Fecha}', '{e.Hora}', '{e.Modulo}', '{e.Evento_}', '{e.Criticidad}'");
        }

        public DataTable FiltrarDatos(DateTime FechaInicial, DateTime FechaFinal, DateTime FechaInicialDefault, DateTime FechaFinalDefault, string Criticidad, object Evento, string Login, object Modulo)
        {
            DataTable dt;

            if (FechaInicial == FechaInicialDefault && FechaFinal == FechaFinalDefault && Criticidad == "" && (Evento == null || Evento.ToString() == "") && Login == "" && (Modulo == null || Modulo.ToString() == ""))
            {
                dt = negocios.ObtenerTabla("*", "Evento", $"CONVERT(date,Fecha) >= '{DateTime.Now.AddDays(-3).ToString("yyyy-MM-ddTHH:mm:ss.fff")}' ORDER BY CodEvento DESC");
            }
            else
            {
                dt = negocios.ObtenerTabla("*", "Evento", $"Login LIKE '{Login}%' AND CONVERT(date,Fecha) >= '{FechaInicial.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND CONVERT(date,Fecha) <= '{FechaFinal.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND Modulo LIKE '{Modulo}%' AND Evento LIKE '{Evento}%' AND Criticidad LIKE '{Criticidad}%' ORDER BY CodEvento DESC");
            }

            //TRADUCCIÓN DE GRILLA
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodEvento");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Login");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Fecha");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Hora");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Modulo");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Evento");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Criticidad");

            return dt;
        }

        public Dictionary<string, string> CrearDiccionarioTraducido()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("Sesiones", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Sesiones"));
            dict.Add("Usuarios", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Usuarios"));
            dict.Add("Perfiles", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Perfiles"));
            dict.Add("Libros", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Libros"));
            dict.Add("Clientes", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Clientes"));
            dict.Add("Proveedores", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Proveedores"));
            dict.Add("Ventas", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Ventas"));
            dict.Add("Compras", LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Compras"));
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

            dict.Add("Pre registro de proveedor", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.PreRegistroProveedor"));
            dict.Add("Registro de proveedor", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroProveedor"));
            dict.Add("Modificación de proveedor", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionProveedor"));
            dict.Add("Borrado lógico de proveedor", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoProveedor"));
            dict.Add("Restauración de proveedor", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RestauracionProveedor"));
            dict.Add("Consulta de proveedores", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ConsultaProveedor"));

            dict.Add("Generación de factura", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.GeneracionFactura"));

            dict.Add("Registro de solicitud de cotización", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroSolicitudCotizacion"));
            dict.Add("Registro de orden de compra", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroOrdenCompra"));
            dict.Add("Recepción de productos", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RecepcionProductos"));
            dict.Add("Generación de reporte de solicitud de cotización", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ReporteSolicitud"));
            dict.Add("Generación de reporte de factura de compra", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ReporteCompra"));
            dict.Add("Generación de reporte de recepción de productos", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ReporteRecepcion"));

            dict.Add("Respaldo", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.Respaldo"));
            dict.Add("Restauración", LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.Restauracion"));

            return dict;
        }

        public DataTable ObtenerModulos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Display");
            dt.Columns.Add("ValorReal");

            dt.Rows.Add("", "");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Sesiones"), "Sesiones");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Usuarios"), "Usuarios");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Perfiles"), "Perfiles");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Libros"), "Libros");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Clientes"), "Clientes");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Proveedores"), "Proveedores");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Ventas"), "Ventas");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.Compras"), "Compras");
            dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Modulos.BaseDeDatos"), "Base de datos");

            return dt;
        }

        public DataTable ObtenerEventos(string Modulo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Display");
            dt.Columns.Add("ValorReal");

            switch (Modulo)
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
                case "Proveedores":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.PreRegistroProveedor"), "Pre registro de proveedor");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroProveedor"), "Registro de proveedor");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ModificacionProveedor"), "Modificación de proveedor");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.BorradoProveedor"), "Borrado lógico de proveedor");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RestauracionProveedor"), "Restauración de proveedor");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ConsultaProveedor"), "Consulta de proveedores");
                    break;
                case "Ventas":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.GeneracionFactura"), "Generación de factura");
                    break;
                case "Compras":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroSolicitudCotizacion"), "Registro de solicitud de cotización");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RegistroOrdenCompra"), "Registro de orden de compra");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.RecepcionProductos"), "Recepción de productos");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ReporteSolicitud"), "Generación de reporte de solicitud de cotización");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ReporteCompra"), "Generación de reporte de factura de compra");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.ReporteRecepcion"), "Generación de reporte de recepción de productos");
                    break;
                case "Base de datos":
                    dt.Rows.Add("", "");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.Respaldo"), "Respaldo");
                    dt.Rows.Add(LanguageManager.ObtenerInstancia().ObtenerTexto("Eventos.Restauracion"), "Restauración");
                    break;
                default:
                    throw new Exception();
            }

            return dt;
        }

        public void ValidarFechas(DateTime FechaInicial, DateTime FechaFinal)
        {
            if (FechaInicial > DateTime.Now)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMBitacoraEventos.Etiquetas.FechaMayorActual"));
            }
            else if (FechaInicial > FechaFinal)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMBitacoraEventos.Etiquetas.FechaMayorFinal"));
            }
        }
    }
}
