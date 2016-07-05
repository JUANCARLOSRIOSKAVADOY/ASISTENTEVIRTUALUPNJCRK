using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Aplicacion.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            GestorCurso GestorCurso = new GestorCurso();
            List<BeCurso> lstCurso = GestorCurso.ObtenerListadoCursoPorEstado(true);
            ViewBag.ListadoCurso = lstCurso;

            ViewBag.ListadoMenu = new List<BeMenu>();

            return View();
        }

        public ActionResult Login() {
            return View("Login");
        }

        public ActionResult Bienvenida() {

            GestorMenu GestorMenu = new GestorMenu();

            Int32 usuarioid = 0;

            if (Request.Cookies["IdUsuario"] != null)
            {
                usuarioid = Convert.ToInt32(Request.Cookies["IdUsuario"].Value);
            }

            ViewBag.ListadoMenu = GestorMenu.ObtenerListadoMenuByRol(usuarioid);

            return View("Bienvenida");
        }

    }
}
