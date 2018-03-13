using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutorialCSRF.Controllers
{
    public class CSRFController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        //EN ESTE ActionResult SERÁ DONDE VERIFICAREMOS SI EL
        //USUARIO TIENE PERMISO PARA INICIAR SESIÓN. SI ES ASI,
        //CREAREMOS UNA Session PARA ÉL.
        [HttpPost]
        public ActionResult Login(String usuario
             , String password)
        {
            usuario = usuario.ToLower();
            //COMPROBAMOS SI EL NOMBRE DE USUARIO Y LA CONTRASEÑA
            //SON CORRECTOS
            if (usuario == "administrador" && password == "administrador")
            {
                //EL NOMBRE DE USUARIO Y LA CONTRASEÑA SON
                //CORRECTOS, ASÍ QUE CREAMOS UNA Session CON SU NOMBRE
                Session["CLIENTE"] = usuario;
                //LE REDIRIGIMOS A LA PÁGINA DE Productos
                return RedirectToAction("Productos");
            }
            else
            {
                //EL USUARIO NO EXISTE, ASÍ QUE NO CREAMOS Session,
                //SINO QUE LE DEVOLVEMOS UN MENSAJE
                ViewBag.Mensaje =
                    "Usuario/Password incorrectos";
                return View();
            }
        }

        // GET: Productos
        public ActionResult Productos()
        {
            //COMPROBAMOS SI EL USUARIO HA INICIADO SESIÓN
            if (Session["CLIENTE"] == null)
            {
                //SI NO SE HA VALIDADO, LE DEVOLVEMOS A LA PAGINA DE LOGIN
                return RedirectToAction("Login");
            }
            else
            {
                //COMO EL USUARIO SE HA VALIDADO, LE PERMITIMOS
                //ACCEDER A LA SECCION DE Productos
                return View();
            }
        }

        [HttpPost]
        public ActionResult Productos(String[] producto)
        {
            //ALMACENAMOS LOS PRODUCTOS ELEGIDOS
            //EN UNA VARIABLE TEMPORAL DEL SERVIDOR
            TempData["PRODUCTOS"] = producto

            //MANDAMOS AL USUARIO A LA PÁGINA DONDE VERÁ
            //EL RESULTADO DE SU COMPRA
            return RedirectToAction("Compra");
        }


    }
}