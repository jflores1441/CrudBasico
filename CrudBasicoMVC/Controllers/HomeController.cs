using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace CrudBasicoMVC.Controllers
{
    public class HomeController : Controller
    {
        Alumno alumno = new Alumno();
        private AlumnoCurso alumno_curso = new AlumnoCurso();
        private Curso curso = new Curso();
        private Adjunto adjunto = new Adjunto();

        // localhost: /Home/Index      En el archivo RouteConfig se establece que el Index se ejecuta por defecto
        // localhost: /Home
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }

        //localhost: /Home/Ver/1
        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }

        //Vista parcial-ignora el layout
        //Ruta home/Cursos/?Alumno_id=1
        public PartialViewResult Cursos(int Alumno_id)
        {
            ViewBag.CursosElegidos = alumno_curso.Listar(Alumno_id);//Enlistando los cursos que tiene por alumno

            /*Asignar el enlistado de los cursos en la Variable ViewBag.Cursos, para usarla en la vista parcial
             para usar el modelo curso y su método Todos(), se instancia un objeto con ese modelo como variable global 
             */
            ViewBag.Cursos = curso.Todos(Alumno_id);//Enlistando todoos los cursos DISPONIBLES

            alumno_curso.Alumno_id = Alumno_id;//modelo

            return PartialView(alumno_curso);
        }

        //Vista parcial-ignora el layout
        //Ruta home/Adjuntos/?Alumno_id=1
        public PartialViewResult Adjuntos(int Alumno_id)
        {
            /*Asignar el enlistado de los adjuntos en la Variable ViewBag.Adjuntos, para usarla en la vista parcial
            para usar el modelo adjunto y su método Listar(), se instancia un objeto con ese modelo como variable global 
            */
            ViewBag.Adjuntos = adjunto.Listar(Alumno_id);
            return PartialView(adjunto);
        }

        public JsonResult GuardarCurso(AlumnoCurso model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();

                if (rm.response)
                {
                    rm.function = "CargarCursos()";
                }
            }

            return Json(rm); //Usando la clase JSON , serializa lo que le pasemos en automatico
        }

        /* Param Archivo --> el nombre del parámetro debe coincidir con el Nombre del Input de la vista parcial donde cargaremos el adjunto
           para que cuando se envia al servidor nuestro controlador, nuestra accion lo detecte y sepa que estamos especificando un archivo*/

        public JsonResult GuardarAdjunto(Adjunto model, HttpPostedFileBase Archivo)
        {
            var rm = new ResponseModel();
            
            //Es importante validar desde el servidor porque el Javascript puede ser desactivado

            if (Archivo != null)
            {
                // Nombre del archivo, es decir, lo renombramos para que no se repita nunca , System.IO para usar Path, GetExtension va a a capturar la extension del archivo
                string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Archivo.FileName);

                // La ruta donde lo vamos guardar
                Archivo.SaveAs(Server.MapPath("~/uploads/" + archivo));

                // Establecemos en nuestro modelo el nombre del archivo
                model.Archivo = archivo;

                rm = model.Guardar();

                if (rm.response)
                {
                    rm.function = "CargarAdjuntos()";
                }
            }

            rm.SetResponse(false, "Debe adjuntar una archivo");

            return Json(rm);
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
        //JsonResult->nos permite retornar un JSON
        public JsonResult Guardar(Alumno model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm= model.Guardar();

                if (rm.response)//Si el estado del ResponseModel es true
                {
                    //rm.function = "SoyAlgo()";//Ejemplo para ejecutar una funcion
                    rm.href = Url.Content("~/home");//Redirecciona al Home
                }
            }

            return Json(rm); //Usando la clase JSON , serializa lo que le pasemos en automatico, en este caso devuelve el ResponseModel
        }


        public ActionResult Eliminar(int id)
        {
            alumno.id = id;
            alumno.Eliminar();
            return Redirect("~/home");
        }

    }
}