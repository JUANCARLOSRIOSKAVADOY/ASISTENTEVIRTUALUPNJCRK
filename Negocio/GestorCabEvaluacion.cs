using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorCabEvaluacion
    {
        public Boolean InsertarCabEvaluacion(BeCabEvaluacion obj, List<BeEvaluacion> detalle)
        {
            return new DalCabEvaluacion().InsertarCabEvaluacion(obj, detalle);
        }

        public List<BeCabEvaluacion> ObtenerListadoCalificacion(Int32 usuarioid) {
            return new DalCabEvaluacion().ObtenerListadoCalificacion(usuarioid);
        }

        public Boolean CalificarNota(Int32 cabevaluacionid, Decimal nota) {
            return new DalCabEvaluacion().CalificarNota(cabevaluacionid, nota);
        }
    }
}
