using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Aplicacion.Controllers
{
    public class MaterialController : Controller
    {
        //
        // GET: /Material/

        public ActionResult Index()
        {

            GestorMenu GestorMenu = new GestorMenu();

            Int32 usuarioid = 0;

            if (Request.Cookies["IdUsuario"] != null)
            {
                usuarioid = Convert.ToInt32(Request.Cookies["IdUsuario"].Value);
            }

            ViewBag.ListadoMenu = GestorMenu.ObtenerListadoMenuByRol(usuarioid);

            return View("CRUD/Material");
        }

        [HttpGet]
        public ActionResult ViewMaterialByCurso(Int32 cursoid) {
            GestorCursoMaterial GestorCursoMaterial = new GestorCursoMaterial();
            BeCursoMaterial objCursoMaterial = GestorCursoMaterial.ObtenerListadoMaterialPorCurso(cursoid);
            ViewBag.cursoid = cursoid;
            ViewBag.CursoMaterial = objCursoMaterial;

            ViewBag.ListadoMenu = new List<BeMenu>();

            return View();
        }

        public ActionResult GuardarMaterial(BeMaterial obj) {
            GestorMaterialDidactico GestorMaterialDidactico = new GestorMaterialDidactico();
            String Estado = "";
            String Mensaje = "";

            if (obj.id == 0){
                if (GestorMaterialDidactico.InsertarMateria(obj))
                {
                    Estado = "OK";
                    Mensaje = "Insertado correctamente.";
                }
                else {
                    Estado = "ERROR";
                    Mensaje = "Error al insertar.";
                }
            }
            else {
                if (GestorMaterialDidactico.ActualizarMateria(obj))
                {
                    Estado = "OK";
                    Mensaje = "Actualizado correctamente.";
                }
                else
                {
                    Estado = "ERROR";
                    Mensaje = "Error al insertar.";
                }
            }

            return Json(new { Estado = Estado,Mensaje = Mensaje }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult OpListadoMaterial(dynamicDataTable Parametros,
            String TextBoxDescripcion, Int32 DropdownEstado)
        {
            Int32 x = Convert.ToInt32(Parametros.sEcho);
            String columnas = Parametros.sColumns;
            Int32 idOrderCol = Convert.ToInt32(Parametros.iSortCol_0);
            String orden = Parametros.sSortDir_0;
            String texto_para_la_busqueda = Parametros.sSearch;
            Boolean? bEstado = null;

            if (DropdownEstado == 1) {
                bEstado = true;
            } else if (DropdownEstado == 0) {
                bEstado = false;
            }


            GestorMaterialDidactico GestorMaterialDidactico = new GestorMaterialDidactico();

            List<BeMaterial> listado = GestorMaterialDidactico.ObtenerListadoMateria(
                Parametros.iDisplayLength, Parametros.iDisplayStart, TextBoxDescripcion, bEstado);

            Int32 TotalPaginas = GestorMaterialDidactico.ObtenerTotalListadoMateria(TextBoxDescripcion, bEstado);

            List<String[]> Resultado = new List<String[]>();

            foreach (BeMaterial obj in listado)
            {
                String botones_disponibles = "";

                botones_disponibles += "<a href='javascript:Material.load_editar("+ obj.id +");' class='btn btn-default btn-sm mr5'> <i class='ti-pencil text-danger'></i> </a>";
                /*String botones_disponibles = "";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_ubicacion_evento(" + objEvento.Ubicacion.idUbicacion + ");' class=\"btn btn-primary btn-xs\"><i class=\"icon-globe\"></i></a>";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_evento(" + objEvento.id + ");' class=\"btn btn-danger btn-xs\"><i class=\"icon-align-justify\"></i></a>";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_evento_participante(" + objEvento.id + ");' class=\"btn btn-success btn-xs\"><i class=\"icon-group\"></i></a>";*/

                Resultado.Add(new String[] {
                        //"<a class='pull-left thumb p-thumb'><img src="+obj.foto+"></a>",
                        obj.id.ToString(),
                        obj.descripcion,
                        obj.estado ? "ACTIVO":"INACTIVO",
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

        public ActionResult ObtenerMaterialById(Int32 materialid) {
            GestorMaterialDidactico GestorMaterialDidactico = new GestorMaterialDidactico();

            BeMaterial obj = GestorMaterialDidactico.ObtenerMaterialById(materialid);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ViewMaterialByCursoEntrenamiento(Int32 cursoid)
        {
            GestorCursoMaterial GestorCursoMaterial = new GestorCursoMaterial();
            BeCursoMaterial objCursoMaterial = GestorCursoMaterial.ObtenerListadoMaterialPorCurso(cursoid);
            ViewBag.cursoid = cursoid;
            ViewBag.CursoMaterial = objCursoMaterial;

            ViewBag.ListadoMenu = new List<BeMenu>();

            return View("MaterialEntrenamiento");
        }
    }
}
