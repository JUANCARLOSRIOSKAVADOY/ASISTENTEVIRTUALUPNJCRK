using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BeCabEvaluacion
    {
        public Int32 cabevaluacionid { get; set; }
        public BeCurso curso { get; set; }
        public BeMaterial material { get; set; }
        public BeUsuario usuario { get; set; }
        public Boolean esEscritura { get; set; }
        public BeEvaluacion evaluacion { get; set; }
        public Decimal nota { get; set; }
    }
}
