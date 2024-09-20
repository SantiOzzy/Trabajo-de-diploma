using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using Services;

namespace BLL
{
    public class BLLRespaldo
    {
        DALRespaldo DataRespaldo = new DALRespaldo();
        public void Respaldar(string path)
        {
            DataRespaldo.Respaldar(path);
        }

        public void Restaurar(string path)
        {
            DataRespaldo.Restaurar(path);
        }
    }
}
