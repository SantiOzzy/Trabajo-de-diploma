using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLPermiso
    {
        DALFamilia DataPerfil = new DALFamilia();
        Datos Data = new Datos();

        public DataTable ObtenerPermisos()
        {
            return Data.LlenarTabla("CodPermiso, Nombre", "Permiso");
        }
    }
}
