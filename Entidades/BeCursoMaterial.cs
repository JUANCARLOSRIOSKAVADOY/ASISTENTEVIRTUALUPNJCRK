using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BeCursoMaterial
    {
        public BeCurso curso { get; set; }
        public List<BeMaterial> lstMaterial { get; set; }
        public BeMaterial material { get; set; }
    }
}
