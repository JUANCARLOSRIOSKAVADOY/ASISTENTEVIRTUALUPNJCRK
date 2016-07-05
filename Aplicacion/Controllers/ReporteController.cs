using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Aplicacion.Controllers
{
    public class ReporteController : Controller
    {
        //
        // GET: /Reporte/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Resultado() {

            GestorUsuario GestorUsuario = new GestorUsuario();            

            GestorMenu GestorMenu = new GestorMenu();
            ViewBag.ListadoMenu = GestorMenu.ObtenerListadoMenuByRol(GestorUsuario.ObtenerUsuarioLogueado().usuarioid);

            GestorRpt GestorRpt = new GestorRpt();
            ViewBag.ListadoResultado = GestorRpt.ObtenerResultado(null);

            return View("Resultado");
        }

        public ActionResult ResultadoByEstud() {
            GestorUsuario GestorUsuario = new GestorUsuario();
            GestorRpt GestorRpt = new GestorRpt();
            ViewBag.ListadoResultado = GestorRpt.ObtenerResultado(GestorUsuario.ObtenerUsuarioLogueado().usuarioid);

            ViewBag.ListadoMenu = new List<BeMenu>();
            return View("ResultadoByEstud");
        }

    }
}
