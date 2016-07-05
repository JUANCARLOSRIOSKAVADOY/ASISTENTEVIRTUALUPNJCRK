using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;

namespace Aplicacion.Controllers
{
    public class CursoController : Controller
    {
        //
        // GET: /Curso/

        public ActionResult Index()
        {
            GestorMenu GestorMenu = new GestorMenu();

            Int32 usuarioid = 0;

            if (Request.Cookies["IdUsuario"] != null)
            {
                usuarioid = Convert.ToInt32(Request.Cookies["IdUsuario"].Value);
            }

            ViewBag.ListadoMenu = GestorMenu.ObtenerListadoMenuByRol(usuarioid);

            return View("CRUD/Curso");
        }

        public ActionResult GuardarCurso(BeCurso obj)
        {
            GestorCurso GestorCurso = new GestorCurso();
            String Estado = "";
            String Mensaje = "";

            if (obj.id == 0)
            {
                if (GestorCurso.InsertarCurso(obj))
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
                if (GestorCurso.ActualizarCurso(obj))
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

            return Json(new { Estado = Estado, Mensaje = Mensaje }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult OpListadoCurso(dynamicDataTable Parametros,
            String TextBoxDescripcion, Int32 DropdownEstado)
        {
            Int32 x = Convert.ToInt32(Parametros.sEcho);
            String columnas = Parametros.sColumns;
            Int32 idOrderCol = Convert.ToInt32(Parametros.iSortCol_0);
            String orden = Parametros.sSortDir_0;
            String texto_para_la_busqueda = Parametros.sSearch;
            Boolean? bEstado = null;

            if (DropdownEstado == 1)
            {
                bEstado = true;
            }
            else if (DropdownEstado == 0)
            {
                bEstado = false;
            }


            GestorCurso GestorCurso = new GestorCurso();

            List<BeCurso> listado = GestorCurso.ObtenerListadoCurso(
                Parametros.iDisplayLength, Parametros.iDisplayStart, TextBoxDescripcion, bEstado);

            Int32 TotalPaginas = GestorCurso.ObtenerTotalListadoCurso(TextBoxDescripcion, bEstado);

            List<String[]> Resultado = new List<String[]>();

            foreach (BeCurso obj in listado)
            {
                String botones_disponibles = "";

                botones_disponibles += "<a href='javascript:Curso.load_editar(" + obj.id + ");' class='btn btn-default btn-sm mr5'> <i class='ti-pencil text-danger'></i> </a>";
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

        public ActionResult ObtenerCursoById(Int32 cursoid)
        {
            GestorCurso GestorCurso = new GestorCurso();

            BeCurso obj = GestorCurso.ObtenerCursolById(cursoid);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpListadoCursoEntrenamiento()
        {
            GestorCurso GestorCurso = new GestorCurso();
            List<BeCurso> lstCurso = GestorCurso.ObtenerListadoCursoPorEstado(true);
            ViewBag.ListadoCurso = lstCurso;

            ViewBag.ListadoMenu = new List<BeMenu>();

            return View("CursoEntrenamiento");
        }

    }
}
