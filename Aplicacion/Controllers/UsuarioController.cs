using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;

namespace Aplicacion.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult verificarLogin(BeUsuario obj)
        {
            GestorUsuario GestorUsuario = new GestorUsuario();

            try
            {
                BeUsuario UsuarioLogueado = GestorUsuario.Login(obj);
                if (UsuarioLogueado != null)
                {

                    HttpCookie Usuario = Request.Cookies.Get("IdUsuario");
                    if (Usuario == null)
                    {
                        // Para crear una cookie
                        Usuario = new HttpCookie("IdUsuario", UsuarioLogueado.usuarioid.ToString());
                        // AñadiR la cookie a nuestro usuario
                        Response.Cookies.Add(Usuario);
                    }
                    else
                    {
                        //Modificar la cookie
                        Usuario.Value = UsuarioLogueado.usuarioid.ToString();
                        Response.Cookies.Set(Usuario);
                    }

                    return Json(new { usuarioValido = true, mensaje = "", url = "/Home/Bienvenida" },JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(new { usuarioValido = false, mensaje = "Datos de Usuario inválidos.", url = "" }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = "errores" }, JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult generarPsw()
        {
            return Content(Utilitarios.Utility.EncryptMD5("virtual"));
        }

        public ActionResult InsertarUsuario(BeUsuario obj) {
            GestorUsuario GestorUsuario = new GestorUsuario();
            String Estado = "";
            String Mensaje = "";

            if (GestorUsuario.InsertarUsuario(obj))
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

        public ActionResult cerrarSesion()
        {
            // Borrar una
            Response.Cookies.Remove("IdUsuario");
            return View("Login");
        }
    }
}
