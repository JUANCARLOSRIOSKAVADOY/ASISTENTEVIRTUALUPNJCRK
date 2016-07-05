using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public class GestorPregunta
    {
        public List<BePregunta> ObtenerListadoPreguntaPorCurMat(Int32 cursoid, Int32 materialid)
        {
            return new DalPregunta().ObtenerListadoPreguntaPorCurMat(cursoid,materialid);
        }

        public List<BePregunta> ObtenerListadoPregunta(Int32 TotalRegistros, Int32 RegistroActual, Int32 cursoid, Int32 materialid)
        {
            return new DalPregunta().ObtenerListadoPregunta(TotalRegistros, RegistroActual, cursoid, materialid);
        }

        public Int32 ObtenerTotalListadoPregunta(Int32 cursoid, Int32 materialid)
        {
            return new DalPregunta().ObtenerTotalListadoPregunta(cursoid, materialid);
        }

        public Boolean InsertarPregunta(BePregunta obj)
        {
            return new DalPregunta().InsertarPregunta(obj);
        }

        public BePregunta ObtenerPreguntaById(Int32 preguntaid)
        {
            return new DalPregunta().ObtenerPreguntaById(preguntaid);
        }

        public Boolean ActualizarPregunta(BePregunta obj)
        {
            return new DalPregunta().ActualizarPregunta(obj);
        }

    }
}
