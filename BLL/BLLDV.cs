﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE;
using Services;
using DAL;

namespace BLL
{
    public class BLLDV
    {
        Datos datos = new Datos();
        DALDV DatosDV = new DALDV();
        CryptoManager Encriptacion = new CryptoManager();

        public void RecalcularDVTabla(string Tabla)
        {
            Object_DV DV = new Object_DV(Tabla, RecalcularDVHTabla(Tabla), RecalcularDVVTabla(Tabla));

            DatosDV.RecalcularDVHTabla(Tabla, DV.DVH);
            DatosDV.RecalcularDVVTabla(Tabla, DV.DVV);
        }

        private string RecalcularDVHTabla(string Tabla)
        {
            DataTable dt = datos.LlenarTabla("*", Tabla);

            string DatosConcatenados = "";

            foreach(DataRow r in dt.Rows)
            {
                foreach(DataColumn c in dt.Columns)
                {
                    DatosConcatenados += r[c].ToString();
                }
            }

            return Encriptacion.GetSHA256(DatosConcatenados);
        }

        private string RecalcularDVVTabla(string Tabla)
        {
            DataTable dt = datos.LlenarTabla("*", Tabla);

            string DatosConcatenados = "";

            foreach (DataColumn c in dt.Columns)
            {
                foreach (DataRow r in dt.Rows)
                {
                    DatosConcatenados += r[c].ToString();
                }
            }

            return Encriptacion.GetSHA256(DatosConcatenados);
        }

        public void ComprobarDV()
        {
            DataTable dt = datos.LlenarTabla("*", "DV");

            foreach(DataRow r in dt.Rows)
            {
                Object_DV DV = new Object_DV(r[0].ToString(), RecalcularDVHTabla(r[0].ToString()), RecalcularDVVTabla(r[0].ToString()));

                DataTable DVTabla = DatosDV.ObtenerDV(DV.Tabla);

                if(DVTabla.Rows[0][1].ToString() != DV.DVH || DVTabla.Rows[0][2].ToString() != DV.DVV)
                {
                    throw new TaskCanceledException($"{DV.Tabla}");
                }
            }
        }

        public void RecalcularBD()
        {
            DataTable dt = datos.LlenarTabla("*", "DV");

            foreach (DataRow r in dt.Rows)
            {
                RecalcularDVTabla(r[0].ToString());
            }
        }
    }
}