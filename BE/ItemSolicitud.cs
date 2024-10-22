using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ItemSolicitud
    {
        public string ISBN;
        public int CodSolicitud;

        public ItemSolicitud(string iSBN, int codSolicitud)
        {
            ISBN = iSBN;
            CodSolicitud = codSolicitud;
        }
    }
}
