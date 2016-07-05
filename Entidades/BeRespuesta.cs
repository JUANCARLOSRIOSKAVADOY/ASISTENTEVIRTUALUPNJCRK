using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BeRespuesta
    {
        public Int32 id { get; set; }
        public String descripcion { get; set; }
        public BePregunta pregunta { get; set; }
    }
}
