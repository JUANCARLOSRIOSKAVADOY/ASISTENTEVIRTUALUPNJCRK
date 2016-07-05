using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorUsuario
    {
        public BeUsuario Login(BeUsuario obj)
        {
            return new DalUsuario().Login(obj.dni,obj.password);
        }

        public BeUsuario ObtenerUsuarioId(Int32 usuarioid)
        {
            return new DalUsuario().ObtenerUsuarioId(usuarioid);
        }

        public Boolean InsertarUsuario(BeUsuario obj)
        {
            return new DalUsuario().InsertarUsuario(obj);
        }

        public BeUsuario ObtenerUsuarioLogueado()
        {
            return new DalUsuario().ObtenerUsuarioLogueado();
        }

    }
}
