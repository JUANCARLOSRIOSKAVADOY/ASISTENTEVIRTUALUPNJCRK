using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public class GestorCursoMaterial
    {
        public BeCursoMaterial ObtenerListadoMaterialPorCurso(Int32 cursoid)
        {
            return new DalCursoMaterial().ObtenerListadoMaterialPorCurso(cursoid);
        }

        public Boolean EliminarCursoMaterial(Int32 cursoid, Int32 materialid)
        {
            return new DalCursoMaterial().EliminarCursoMaterial(cursoid, materialid);
        }

        public Boolean InsertarCursoMaterial(BeCurso objCurso,BeMaterial objMaterial)
        {
            return new DalCursoMaterial().InsertarCursoMaterial(objCurso,objMaterial);
        }

        public List<BeCursoMaterial> ObtenerListadoCursoMaterial(Int32 TotalRegistros,Int32 RegistroActual,Int32 cursoid, Int32 materialid)
        {
            return new DalCursoMaterial().ObtenerListadoCursoMaterial(TotalRegistros, RegistroActual, cursoid, materialid);
        }

        public Int32 ObtenerTotalListadoCursoMaterial(Int32 cursoid, Int32 materialid)
        {
            return new DalCursoMaterial().ObtenerTotalListadoCursoMaterial(cursoid, materialid);
        }

        public BeCursoMaterial ObtenerCursoMaterialById(Int32 cursoid, Int32 materialid)
        {
            return new DalCursoMaterial().ObtenerCursoMaterialById(cursoid, materialid);
        }        
    }
}
