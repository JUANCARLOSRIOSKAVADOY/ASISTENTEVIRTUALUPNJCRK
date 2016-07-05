using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorEvaluacion
    {
        public BeEvaluacion ObtenerEvaluacionById(Int32 evaluacionid) {
            return new DalEvaluacion().ObtenerEvaluacionById(evaluacionid);
        }
    }
}
