using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Familia : Perfil
    {
        private IList<Perfil> _hijos;

        public Familia()
        {
            _hijos = new List<Perfil>();
        }

        public override IList<Perfil> Hijos
        {
            get
            {
                return _hijos.ToArray();
            }

        }

        public override void VaciarHijos()
        {
            _hijos = new List<Perfil>();
        }
        public override void AgregarHijo(Perfil p)
        {
            _hijos.Add(p);
        }
    }
}
