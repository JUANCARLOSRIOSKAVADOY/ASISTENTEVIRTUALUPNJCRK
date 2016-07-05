using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BeRptResultado
    {
        public String usuario { get; set; }
        public String curso { get; set; }
        public String material { get; set; }
        public Int32 total { get; set; }
        public Int32 acertado { get; set; }
        public Int32 errado { get; set; }
        public Int32 nota { get; set; }
        public String nivel { get; set; }
    }
}
