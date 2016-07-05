using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorCurso
    {
        #region Curso

        public List<BeCurso> ObtenerListadoCursoPorEstado(Boolean? Estado)
        {
            return new DalCurso().ObtenerListadoCursoPorEstado(Estado);
        }

        public Boolean InsertarCurso(BeCurso obj)
        {
            return new DalCurso().InsertarCurso(obj);
        }

        public Boolean ActualizarCurso(BeCurso obj)
        {
            return new DalCurso().ActualizarCurso(obj);
        }

        public List<BeCurso> ObtenerListadoCurso(Int32 TotalRegistros, Int32 RegistroActual, String Descripcion, Boolean? Estado)
        {
            return new DalCurso().ObtenerListadoCurso(TotalRegistros, RegistroActual, Descripcion, Estado);
        }

        public Int32 ObtenerTotalListadoCurso(String Descripcion, Boolean? Estado)
        {
            return new DalCurso().ObtenerTotalListadoCurso(Descripcion, Estado);
        }

        public BeCurso ObtenerCursolById(Int32 cursoid)
        {
            return new DalCurso().ObtenerCursolById(cursoid);
        }

        #endregion
    }
}
