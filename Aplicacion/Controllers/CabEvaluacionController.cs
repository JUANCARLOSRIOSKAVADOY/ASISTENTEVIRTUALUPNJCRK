using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Aplicacion.Controllers
{
    public class CabEvaluacionController : Controller
    {
        //
        // GET: /CabEvaluacion/

        public ActionResult Calificacion()
        {
            GestorUsuario GestorUsuario = new GestorUsuario();
            BeUsuario UsuarioLogueado = GestorUsuario.ObtenerUsuarioLogueado();

            GestorMenu GestorMenu = new GestorMenu();
            ViewBag.ListadoMenu = GestorMenu.ObtenerListadoMenuByRol(UsuarioLogueado.usuarioid);

            GestorCabEvaluacion GestorCabEvaluacion = new GestorCabEvaluacion();
            ViewBag.Calificacion = GestorCabEvaluacion.ObtenerListadoCalificacion(UsuarioLogueado.usuarioid);

            return View("Calificacion");
        }

        public ActionResult SaveNota(Int32 cabevaluacionid, Decimal nota)
        {
            GestorCabEvaluacion GestorCabEvaluacion = new GestorCabEvaluacion();
            String Estado = "";
            String Mensaje = "";

            if (GestorCabEvaluacion.CalificarNota(cabevaluacionid,nota))
            {
                Estado = "OK";
                Mensaje = "Nota actualizada.";
            }
            else
            {
                Estado = "ERROR";
                Mensaje = "Error al actualizar nota.";
            }

            return Json(new { Estado = Estado, Mensaje = Mensaje }, JsonRequestBehavior.DenyGet);
        }

    }
}
