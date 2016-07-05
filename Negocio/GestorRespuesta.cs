using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public class GestorRespuesta
    {
        public List<BeRespuesta> ObtenerRespuestasByPregunta(Int32 preguntaid){
            return new DalRespuesta().ObtenerRespuestasByPregunta(preguntaid);
        }

        public List<BeRespuesta> ObtenerRespuestasEntrenamientoByPreg(Int32 cursoid, Int32 materialid, String descripcion)
        {
            return new DalRespuesta().ObtenerRespuestasEntrenamientoByPreg(cursoid, materialid, descripcion);
        }

        public Boolean InsertarRespuesta(BeRespuesta obj)
        {
            return new DalRespuesta().InsertarRespuesta(obj);
        }

        public Boolean EliminarRespuesta(Int32 preguntaid)
        {
            return new DalRespuesta().EliminarRespuesta(preguntaid);
        }
    }
}
