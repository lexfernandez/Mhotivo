using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        //
        // GET: /Course/

        public ActionResult Index()
        {
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }

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
                    TempData["MessageInfo"] = new MessageModel
                                              {
                                                  Type = "SUCCESS",
                                                  Title = "Agregado",
                                                  Content = "El grupo fue agregado exitosamente!"
                                              };
                }
                else
                {
                    TempData["MessageInfo"] = new MessageModel
                                              {
                                                  Type = "INFO",
                                                  Title = "Data validation",
                                                  Content = "The data is no valid!"
                                              };
                }
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "ERROR",
                                              Title = "Error",
                                              Content = "Something went wrong, please try again!"
                                          };
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
            string content = "El curso " + role.Name + " ha sido modificado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "SUCCESS",
                                          Title = title,
                                          Content = content
                                      };

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

                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "SUCCESS",
                                              Title = " eliminado",
                                              Content = " eliminado exitosamente!"
                                          };

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "ERROR",
                                              Title = "Error en eliminación",
                                              Content =
                                                  "El grupo no pudo ser eliminado correctamente, por favor intente nuevamente!"
                                          };
                return View("Index");
            }
        }
    }
}