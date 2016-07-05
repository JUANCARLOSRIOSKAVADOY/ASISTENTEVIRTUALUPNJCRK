using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BeEvaluacion
    {
        public Int32 evaluacionid { get; set; }
        public String pregunta { get; set; }
        public String respuesta { get; set; }
        public Boolean? esCorrecta { get; set; }
    }
}
