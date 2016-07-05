using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorRpt
    {
        public List<BeRptResultado> ObtenerResultado(Int32? usuarioid)
        {
            return new DalRpt().ObtenerResultado(usuarioid);
        }

    }
}
