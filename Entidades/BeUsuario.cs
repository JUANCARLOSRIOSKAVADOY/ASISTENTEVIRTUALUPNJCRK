using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BeUsuario
    {
        public Int32 usuarioid { get; set; }
        public String nombres { get; set; }
        public String ap_paterno { get; set; }
        public String ap_materno { get; set; }
        public Boolean esAdmin { get; set; }
        public Boolean esEstudiante { get; set; }
        public Boolean esDocente { get; set; }
        public String dni { get; set; }
        public String password { get; set; }

    }
}
