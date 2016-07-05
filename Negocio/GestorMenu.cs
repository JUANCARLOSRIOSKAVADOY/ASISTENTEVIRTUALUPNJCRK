using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestorMenu
    {
        public List<BeMenu> ObtenerListadoMenuByRol(Int32 usuarioid)
        {
            return new DalMenu().ObtenerListadoMenuByRol(usuarioid);
        }
    }
}
