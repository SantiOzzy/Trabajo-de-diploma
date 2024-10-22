﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLSolicitudCotizacion
    {
        Datos Data = new Datos();
        DALSolicitudCotizacion DatosSolicitudCotizacion = new DALSolicitudCotizacion();
        public void RegistrarSolicitudCotizacion(SolicitudCotizacion sol)
        {
            Data.EjecutarComando("InsertarSolicitudCotizacion", $"'{sol.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss.fff")}'");
        }

        public int ObtenerCodSolicitudCotizacion()
        {
            return DatosSolicitudCotizacion.ObtenerCodSolicitudCotizacion();
        }

        public void RegistrarItems(SolicitudCotizacion sol)
        {
            foreach (var item in sol.items)
            {
                Data.EjecutarComando("RegistrarItemSolicitud", $"{item.CodSolicitud}, '{item.ISBN}'");
            }
        }

        public void RegistrarProveedores(SolicitudCotizacion sol, int CodSol)
        {
            foreach (var prov in sol.proveedores)
            {
                Data.EjecutarComando("RegistrarProveedorSolicitud", $"{CodSol}, '{prov.CUIT}'");
            }
        }
    }
}