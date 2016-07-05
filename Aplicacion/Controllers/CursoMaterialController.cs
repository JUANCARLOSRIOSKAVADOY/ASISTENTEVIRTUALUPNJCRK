using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Aplicacion.Controllers
{
    public class CursoMaterialController : Controller
    {
        //
        // GET: /CursoMaterial/

        public ActionResult Index()
        {
            GestorCurso GestorCurso = new GestorCurso();
            Int32 totalCurso = GestorCurso.ObtenerTotalListadoCurso("", null);
            ViewBag.ListadoCurso = GestorCurso.ObtenerListadoCurso(totalCurso, 0, "", null);

            GestorMaterialDidactico GestorMaterialDidactico = new GestorMaterialDidactico();
            Int32 totalMaterial = GestorMaterialDidactico.ObtenerTotalListadoMateria("", null);
            ViewBag.ListadoMaterial = GestorMaterialDidactico.ObtenerListadoMateria(totalMaterial, 0, "", null);

            GestorMenu GestorMenu = new GestorMenu();

            Int32 usuarioid = 0;

            if (Request.Cookies["IdUsuario"] != null)
            {
                usuarioid = Convert.ToInt32(Request.Cookies["IdUsuario"].Value);
            }

            ViewBag.ListadoMenu = GestorMenu.ObtenerListadoMenuByRol(usuarioid);

            return View("CRUD/CursoMaterial");
        }

        public ActionResult GuardarCursoMaterial(BeCursoMaterial obj)
        {
            GestorCursoMaterial GestorCursoMaterial = new GestorCursoMaterial();
            String Estado = "";
            String Mensaje = "";

            if (GestorCursoMaterial.ObtenerCursoMaterialById(obj.curso.id,obj.material.id) == null)
            {
                if (GestorCursoMaterial.InsertarCursoMaterial(obj.curso,obj.material))
                {
                    Estado = "OK";
                    Mensaje = "Insertado correctamente.";
                }
                else
                {
                    Estado = "ERROR";
                    Mensaje = "Error al insertar.";
                }
            }
            else
            {
                /*if (GestorCurso.ActualizarCurso(obj))
                {
                    Estado = "OK";
                    Mensaje = "Actualizado correctamente.";
                }
                else
                {
                    Estado = "ERROR";
                    Mensaje = "Error al insertar.";
                }*/
            }

            return Json(new { Estado = Estado, Mensaje = Mensaje }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult OpListadoCursoMaterial(dynamicDataTable Parametros,
            Int32 DropdownCursoFiltro, Int32 DropdownMaterialFiltro)
        {
            Int32 x = Convert.ToInt32(Parametros.sEcho);
            String columnas = Parametros.sColumns;
            Int32 idOrderCol = Convert.ToInt32(Parametros.iSortCol_0);
            String orden = Parametros.sSortDir_0;
            String texto_para_la_busqueda = Parametros.sSearch;

            GestorCursoMaterial GestorCursoMaterial = new GestorCursoMaterial();

            List<BeCursoMaterial> listado = GestorCursoMaterial.ObtenerListadoCursoMaterial(
                Parametros.iDisplayLength, Parametros.iDisplayStart, DropdownCursoFiltro, DropdownMaterialFiltro);

            Int32 TotalPaginas = GestorCursoMaterial.ObtenerTotalListadoCursoMaterial(DropdownCursoFiltro, DropdownMaterialFiltro);

            List<String[]> Resultado = new List<String[]>();

            foreach (BeCursoMaterial obj in listado)
            {
                String botones_disponibles = "";

                botones_disponibles += "<a href='javascript:CursoMaterial.load_eliminar(" + obj.curso.id + ","+ obj.material.id +");' class='btn btn-default btn-sm mr5'> <i class='fa fa-remove text-danger'></i> </a>";
                /*String botones_disponibles = "";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_ubicacion_evento(" + objEvento.Ubicacion.idUbicacion + ");' class=\"btn btn-primary btn-xs\"><i class=\"icon-globe\"></i></a>";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_evento(" + objEvento.id + ");' class=\"btn btn-danger btn-xs\"><i class=\"icon-align-justify\"></i></a>";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_evento_participante(" + objEvento.id + ");' class=\"btn btn-success btn-xs\"><i class=\"icon-group\"></i></a>";*/

                Resultado.Add(new String[] {
                        //"<a class='pull-left thumb p-thumb'><img src="+obj.foto+"></a>",
                        obj.curso.descripcion,
                        obj.material.descripcion,
                        botones_disponibles
                    }
                );
            }
            //Retorno el Json que alimentara al datatables
            return Json(new
            {
                sEcho = x,
                iTotalRecords = TotalPaginas,
                iTotalDisplayRecords = TotalPaginas,
                aaData = Resultado
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerCursoMaterialById(Int32 cursoid, Int32 materialid)
        {
            GestorCursoMaterial GestorCursoMaterial = new GestorCursoMaterial();

            BeCursoMaterial obj = GestorCursoMaterial.ObtenerCursoMaterialById(cursoid,materialid);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarCursoMaterialById(Int32 cursoid, Int32 materialid)
        {
            GestorCursoMaterial GestorCursoMaterial = new GestorCursoMaterial();
            String Estado = "";
            String Mensaje = "";

            if (GestorCursoMaterial.EliminarCursoMaterial(cursoid, materialid))
            {
                Estado = "OK";
                Mensaje = "Eliminado correctamente.";
            }
            else
            {
                Estado = "ERROR";
                Mensaje = "Error al eliminar.";
            }

            return Json(new { Estado = Estado, Mensaje = Mensaje }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ObtenerListadoMaterialByCurso(Int32 cursoid) {
            GestorCursoMaterial GestorCursoMaterial = new GestorCursoMaterial();
            List<BeMaterial> lstMaterial = GestorCursoMaterial.ObtenerListadoMaterialPorCurso(cursoid).lstMaterial;
            return Json(lstMaterial, JsonRequestBehavior.DenyGet);
        }
    }
}
