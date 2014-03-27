using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{

    public class CourseController : Controller
    {

        private readonly CourseRepository _courseRepository = CourseRepository.Instance;

        //
        // GET: /Course/

        public ActionResult Index()
        {
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }

            var v = _courseRepository.Query(x => x).Include("Area");

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
                        MessageType = "SUCCESS",
                        MessageTitle = "Agregado",
                        MessageContent = "El grupo fue agregado exitosamente!"
                    };
                }
                else
                {
                    TempData["MessageInfo"] = new MessageModel
                    {
                        MessageType = "INFO",
                        MessageTitle = "Data validation",
                        MessageContent = "The data is no valid!"
                    };
                }

            }
            catch (Exception exception)
            {
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "ERROR",
                    MessageTitle = "Error",
                    MessageContent = "Something went wrong, please try again!"
                };
            }
            var groups = _courseRepository.Query(x => x);
            return RedirectToAction("Index", groups);
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id)
        {
            var c = _courseRepository.GetById(id);

            return View(c);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            var role = _courseRepository.UpdateNew(course);
            const string title = "Curso Actualizado";
            var content = "El curso " + role.Name + " ha sido modificado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "SUCCESS",
                MessageTitle = title,
                MessageContent = content
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
                var group = _courseRepository.GetById(id);
                _courseRepository.Delete(group);
                _courseRepository.SaveChanges();

                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "SUCCESS",
                    MessageTitle = " eliminado",
                    MessageContent = " eliminado exitosamente!"
                };

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "ERROR",
                    MessageTitle = "Error en eliminación",
                    MessageContent = "El grupo no pudo ser eliminado correctamente, por favor intente nuevamente!"
                };
                return View("Index");
            }
        }
    }
}
