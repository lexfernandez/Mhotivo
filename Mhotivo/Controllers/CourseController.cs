using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        //
        // GET: /Course/

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            IQueryable<Course> v = _courseRepository.Query(x => x).Include("Area");

            return View(v);
        }

        //
        // GET: /Course/Create

        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Add(Course group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _courseRepository.Create(group);
                    _courseRepository.SaveChanges();
                    _viewMessageLogic.SetNewMessage("Agregado", "El grupo fue agregado exitosamente.", ViewMessageType.SuccessMessage);
                }
                else
                {
                    _viewMessageLogic.SetNewMessage("Validación de Información", "La información no es válida.", ViewMessageType.InformationMessage);
                }
            }
            catch
            {
                _viewMessageLogic.SetNewMessage("Error", "Algo salió mal, por favor intente de nuevo.", ViewMessageType.ErrorMessage);
            }
            IQueryable<Course> groups = _courseRepository.Query(x => x);
            return RedirectToAction("Index", groups);
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id)
        {
            Course c = _courseRepository.GetById(id);

            return View(c);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            Course role = _courseRepository.Update(course);
            const string title = "Curso Actualizado";
            var content = "El curso " + role.Name + " ha sido modificado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);


            return RedirectToAction("Index");
        }

        //
        // POST: /Course/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Course group = _courseRepository.GetById(id);
                _courseRepository.Delete(group);
                _courseRepository.SaveChanges();
                _viewMessageLogic.SetNewMessage("Eliminado", "Eliminado exitosamente.", ViewMessageType.SuccessMessage);

                return RedirectToAction("Index");
            }
            catch
            {
                _viewMessageLogic.SetNewMessage("Error en eliminación", "El grupo no pudo ser eliminado correctamente, por favor intente nuevamente.", ViewMessageType.ErrorMessage);
                return View("Index");
            }
        }
    }
}