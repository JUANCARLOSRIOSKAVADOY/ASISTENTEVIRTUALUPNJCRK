using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Aplicacion.Controllers
{
    public class EvaluacionController : Controller
    {
        //
        // GET: /Evaluacion/

        public ActionResult VistaEvaluacionById(Int32 evaluacionid)
        {
            GestorEvaluacion GestorEvaluacion = new GestorEvaluacion();
            ViewBag.Evaluacion = GestorEvaluacion.ObtenerEvaluacionById(evaluacionid);
            return View();
        }

    }
}
