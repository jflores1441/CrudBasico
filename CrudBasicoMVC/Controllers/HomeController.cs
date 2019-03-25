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
        public ActionResult Crud(int id = 0)
        {
            return View(
                //Cuando el valor de id=0 generará un nuevo registro, de lo contrario lo actualizará dependiendo del id que reciba
                id == 0 ? new Alumno() : alumno.Obtener(id)
                );
        }

        //public ActionResult Guardar(Alumno model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Guardar();
        //        return Redirect("~/home");
        //    }
        //    else
        //    {
        //        return View("~/views/home/crud.cshtml", model);
        //    }

        //}

        //Método que usaremos a partir de que queremos introducir un script de AJAX y usar JSON
        //Ref vease el Model llamado ResponseModel
        public JsonResult Guardar(Alumno model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm= model.Guardar();

                if (rm.response)
                {
                    rm.href = Url.Content("~/home");
                }
            }

            return Json(rm); //Usando la clase JSON , serializa lo que le pasemos en automatico
        }


        public ActionResult Eliminar(int id)
        {
            alumno.id = id;
            alumno.Eliminar();
            return Redirect("~/home");
        }

    }
}