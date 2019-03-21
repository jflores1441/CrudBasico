using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace CrudBasicoMVC.Controllers
{
    public class HomeController : Controller
    {
        private Alumno alumno = new Alumno();

        // localhost: /Home/Index      En el archivo RouteConfig se establece que el Index se ejecuta por defecto
        // localhost: /Home
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }

        //localhost: /Home/Ver/id
        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }
    }
}