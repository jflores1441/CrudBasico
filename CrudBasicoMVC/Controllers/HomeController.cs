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

        //Método que insetará o actualizará un registro
        public ActionResult Crud(int id=0)
        {
            return View(
                //Cuando el valor de id=0 generará un nuevo registro, de lo contrario lo actualizará dependiendo del id que reciba
                id == 0 ? new Alumno() : alumno.Obtener(id) 
                );
        }
    }
}