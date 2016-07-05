using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Aplicacion.Controllers
{
    public class RespuestaController : Controller
    {
        //
        // GET: /Respuesta/

        public ActionResult ObtenerRespuestaByPregunta(Int32 preguntaid)
        {
            GestorRespuesta GestorRespuesta = new GestorRespuesta();
            List<BeRespuesta> respuestas = GestorRespuesta.ObtenerRespuestasByPregunta(preguntaid);
            ViewBag.respuestas = respuestas;
            return View("VistaRespuestasPorPregunta");
        }

        public ActionResult JsonObtenerRespuestaByPregunta(Int32 preguntaid)
        {
            GestorRespuesta GestorRespuesta = new GestorRespuesta();
            List<BeRespuesta> respuestas = GestorRespuesta.ObtenerRespuestasByPregunta(preguntaid);
            return Json(respuestas, JsonRequestBehavior.DenyGet);
        }

        public ActionResult VistaInsertarRespuesta(Int32 preguntaid) {
            ViewBag.preguntaid = preguntaid;
            return View("VistaInsertarRespuesta");
        }

        public ActionResult InsertarRespuesta(BeRespuesta obj) {
            GestorRespuesta GestorRespuesta = new GestorRespuesta();
            String Estado = "";
            String Mensaje = "";

            if (GestorRespuesta.InsertarRespuesta(obj))
            {
                Estado = "OK";
                Mensaje = "Registrado correctamente.";
            }
            else
            {
                Estado = "ERROR";
                Mensaje = "Error al registrar.";
            }

            return Json(new { Estado = Estado, Mensaje = Mensaje }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult EliminarRespuesta(Int32 respuestaid) {
            GestorRespuesta GestorRespuesta = new GestorRespuesta();
            String Estado = "";
            String Mensaje = "";

            if (GestorRespuesta.EliminarRespuesta(respuestaid))
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
    }
}
