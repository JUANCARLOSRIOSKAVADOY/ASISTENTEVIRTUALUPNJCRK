using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BeMaterial
    {
        public Int32 id { get; set; }
        public String descripcion { get; set; }
        public String image { get; set; }
        public Boolean estado { get; set; }
        public BeCurso curso { get; set; }
        public Boolean esEscritura { get; set; }
    }
}
