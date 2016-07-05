using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public class GestorMaterialDidactico
    {
        public Boolean InsertarMateria(BeMaterial obj)
        {
            return new DalMaterial().InsertarMateria(obj);
        }

        public Boolean ActualizarMateria(BeMaterial obj)
        {
            return new DalMaterial().ActualizarMateria(obj);
        }

        public List<BeMaterial> ObtenerListadoMateria(Int32 TotalRegistros, Int32 RegistroActual, String Descripcion, Boolean? Estado)
        {
            return new DalMaterial().ObtenerListadoMateria(TotalRegistros, RegistroActual, Descripcion, Estado);
        }

        public Int32 ObtenerTotalListadoMateria(String Descripcion, Boolean? Estado)
        {
            return new DalMaterial().ObtenerTotalListadoMateria(Descripcion, Estado);
        }

        public BeMaterial ObtenerMaterialById(Int32 materialid)
        {
            return new DalMaterial().ObtenerMaterialById(materialid);
        }
    }
}
