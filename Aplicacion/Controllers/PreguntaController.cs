using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Aplicacion.Controllers
{
    public class PreguntaController : Controller
    {
        //
        // GET: /Pregunta/
        public ActionResult Index() {
            GestorCurso GestorCurso = new GestorCurso();
            Int32 totalCurso = GestorCurso.ObtenerTotalListadoCurso("", null);
            ViewBag.ListadoCurso = GestorCurso.ObtenerListadoCurso(totalCurso, 0, "", null);

            ViewBag.ListadoMaterial = new List<BeMaterial>();

            GestorMenu GestorMenu = new GestorMenu();

            Int32 usuarioid = 0;

            if (Request.Cookies["IdUsuario"] != null)
            {
                usuarioid = Convert.ToInt32(Request.Cookies["IdUsuario"].Value);
            }

            ViewBag.ListadoMenu = GestorMenu.ObtenerListadoMenuByRol(usuarioid);

            return View("CRUD/Pregunta");
        }

        public ActionResult ObtenerPregunta(Int32 preguntaid, String descripcionPregunta, Boolean esInicio, Int32 materialid, String final_span = "", Boolean? esCorrecto = null)
        {
            GestorPregunta GestorPregunta = new GestorPregunta();
            GestorMaterialDidactico GestorMaterialDidactico = new GestorMaterialDidactico();
            ViewBag.Material = GestorMaterialDidactico.ObtenerMaterialById(materialid);

            ViewBag.Pregunta = new BePregunta() {
                id = preguntaid,
                descripcion = descripcionPregunta
            };


            if (esInicio)
            {
                if (Session["lstEvaluacion"] != null)
                {
                    Session["lstEvaluacion"] = null;
                }

                List<BeEvaluacion> lstEvaluacion = new List<BeEvaluacion>();
                lstEvaluacion.Add(
                    new BeEvaluacion() {
                        pregunta = descripcionPregunta
                    }
                );

                Session["lstEvaluacion"] = lstEvaluacion;

            }
            else {
                List<BeEvaluacion> lstEvaTmp = Session["lstEvaluacion"] as List<BeEvaluacion>;
                lstEvaTmp[lstEvaTmp.Count - 1].respuesta = final_span;
                lstEvaTmp[lstEvaTmp.Count - 1].esCorrecta = esCorrecto;

                //List<BeEvaluacion> lstEvaluacion = new List<BeEvaluacion>();
                lstEvaTmp.Add(
                    new BeEvaluacion()
                    {
                        pregunta = descripcionPregunta
                    }
                );
            }

            return View("Pregunta");
        }

        public ActionResult FinishEvaluacion(Int32 courseid,Int32 materialid, String final_span, Boolean esCorrecto, Boolean esEscritura) {
            if (Session["lstEvaluacion"] != null)
            {
                List<BeEvaluacion> lstEvaTmp = Session["lstEvaluacion"] as List<BeEvaluacion>;
                lstEvaTmp[lstEvaTmp.Count - 1].respuesta = final_span;
                lstEvaTmp[lstEvaTmp.Count - 1].esCorrecta = esCorrecto;

                Session["lstEvaluacion"] = lstEvaTmp;
            }

            GestorUsuario GestorUsuario = new GestorUsuario();

            BeCabEvaluacion obj = new BeCabEvaluacion()
            {
                curso = new BeCurso() {
                    id = courseid
                },
                material = new BeMaterial() {
                    id = materialid
                },
                usuario = new BeUsuario() {
                    usuarioid = GestorUsuario.ObtenerUsuarioLogueado().usuarioid
                },
                esEscritura = esEscritura
            };

            GestorCabEvaluacion GestorCabEvaluacion = new GestorCabEvaluacion();
            Boolean exito = GestorCabEvaluacion.InsertarCabEvaluacion(obj, Session["lstEvaluacion"] as List<BeEvaluacion> );

            return Json(1,JsonRequestBehavior.DenyGet);
        }

        public ActionResult StartQuestion(Int32 materialid) {

            List<BeEvaluacion> evaluacion = new List<BeEvaluacion>();

            GestorMaterialDidactico GestorMaterialDidactico = new GestorMaterialDidactico();
            BeMaterial Material = GestorMaterialDidactico.ObtenerMaterialById(materialid);
            ViewBag.Material = Material;
            //Session["logado"] == null;

            return View("StartQuestion");
        }

        public ActionResult JsonListadoPreguntasRandom(Int32 cursoid, Int32 materialid) {
            GestorPregunta GestorPregunta = new GestorPregunta();
            List<BePregunta> preguntas = GestorPregunta.ObtenerListadoPreguntaPorCurMat(cursoid, materialid);
            return Json(preguntas,JsonRequestBehavior.DenyGet);
        }

        public ActionResult OpListadoPregunta(dynamicDataTable Parametros,
            Int32 DropdownCursoFiltro, Int32 DropdownMaterialFiltro)
        {
            Int32 x = Convert.ToInt32(Parametros.sEcho);
            String columnas = Parametros.sColumns;
            Int32 idOrderCol = Convert.ToInt32(Parametros.iSortCol_0);
            String orden = Parametros.sSortDir_0;
            String texto_para_la_busqueda = Parametros.sSearch;

            GestorPregunta GestorPregunta = new GestorPregunta();

            List<BePregunta> listado = GestorPregunta.ObtenerListadoPregunta(
                Parametros.iDisplayLength, Parametros.iDisplayStart, DropdownCursoFiltro, DropdownMaterialFiltro);

            Int32 TotalPaginas = GestorPregunta.ObtenerTotalListadoPregunta(DropdownCursoFiltro, DropdownMaterialFiltro);

            List<String[]> Resultado = new List<String[]>();

            foreach (BePregunta obj in listado)
            {
                String botones_disponibles = "";

                botones_disponibles += "<a href='javascript:Pregunta.load_editar(" + obj.id + ");' class='btn btn-default btn-sm mr5'> <i class='fa fa-edit text-danger'></i> </a>";
                botones_disponibles += "<a href='javascript:Pregunta.load_respuesta(" + obj.id + ");' class='btn btn-default btn-sm mr5'> <i class='fa fa-list-ol text-danger'></i> </a>";
                botones_disponibles += "<a href='javascript:Pregunta.load_ins_respuesta(" + obj.id + ");' class='btn btn-default btn-sm mr5'> <i class='fa fa-plus-circle text-danger'></i> </a>";
                /*String botones_disponibles = "";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_ubicacion_evento(" + objEvento.Ubicacion.idUbicacion + ");' class=\"btn btn-primary btn-xs\"><i class=\"icon-globe\"></i></a>";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_evento(" + objEvento.id + ");' class=\"btn btn-danger btn-xs\"><i class=\"icon-align-justify\"></i></a>";
                botones_disponibles += "<a href='javascript:BuscarEvento.load_modal_evento_participante(" + objEvento.id + ");' class=\"btn btn-success btn-xs\"><i class=\"icon-group\"></i></a>";*/

                Resultado.Add(new String[] {
                        //"<a class='pull-left thumb p-thumb'><img src="+obj.foto+"></a>",
                        obj.descripcion,
                        /*obj.curso.descripcion,
                        obj.material.descripcion,*/
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

        public ActionResult GuardarPregunta(BePregunta obj)
        {
            GestorPregunta GestorPregunta = new GestorPregunta();
            String Estado = "";
            String Mensaje = "";

            if (obj.id == 0)
            {
                if (GestorPregunta.InsertarPregunta(obj))
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
                if (GestorPregunta.ActualizarPregunta(obj))
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

        public ActionResult ObtenerPreguntaById(Int32 preguntaid)
        {
            GestorPregunta GestorPregunta = new GestorPregunta();
            BePregunta obj = GestorPregunta.ObtenerPreguntaById(preguntaid);
            return Json(obj, JsonRequestBehavior.DenyGet);
        }

        public ActionResult PreguntaEntrenamiento(Int32 cursoid, Int32 materialid) {

            GestorCurso GestorCurso = new GestorCurso();
            GestorMaterialDidactico GestorMaterialDidactico = new GestorMaterialDidactico();

            ViewBag.ListadoMenu = new List<BeMenu>();
            ViewBag.Curso = GestorCurso.ObtenerCursolById(cursoid);
            ViewBag.Material = GestorMaterialDidactico.ObtenerMaterialById(materialid);
            return View("PreguntaEntrenamiento");
        }

        public ActionResult JsonListadoRespuestasByPregEntrenamiento(String pregunta,Int32 cursoid, Int32 materialid)
        {
            GestorRespuesta GestorRespuesta = new GestorRespuesta();
            List<BeRespuesta> respuestas = GestorRespuesta.ObtenerRespuestasEntrenamientoByPreg(cursoid, materialid, pregunta);
            return Json(respuestas, JsonRequestBehavior.DenyGet);
        }

    }
}
