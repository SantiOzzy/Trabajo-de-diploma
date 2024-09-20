using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Permiso : Perfil
    {
        public override IList<Perfil> Hijos
        {
            get
            {
                return new List<Perfil>();
            }

        }

        public override void AgregarHijo(Perfil p)
        {

        }

        public override void VaciarHijos()
        {

        }
    }
}
