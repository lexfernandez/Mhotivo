using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class ClassActivityController : Controller
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IClassActivityRepository _classActivityRepository;

        public ClassActivityController(IClassActivityRepository classActivityRepository,
            IAcademicYearRepository academicYearRepository)
        {
            _classActivityRepository = classActivityRepository;
            _academicYearRepository = academicYearRepository;
        }

        public ActionResult Index()
        {
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }

            return View(_classActivityRepository.Query(x => x).ToList()
                .Select(x => new DisplayClassActivityModel
                             {
                                 AcademicYear = Convert.ToString(x.AcademicYear.Year.Year),
                                 DisplayName = x.Name,
                                 Type = x.Type,
                                 Description = x.Description,
                                 Value = Convert.ToString(x.Value),
                                 Id = x.Id
                             }));
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            ClassActivity thisClassActivity = _classActivityRepository.GetById(id);
            var classActivity = new ClassActivityEditModel
                                {
                                    AcademicYearId = thisClassActivity.AcademicYear.Id,
                                    DisplayName = thisClassActivity.Name,
                                    Type = thisClassActivity.Type,
                                    Description = thisClassActivity.Description,
                                    Value = thisClassActivity.Value,
                                    Id = thisClassActivity.Id
                                };

            var listTypes = new List<string>();
            listTypes.Add("Exam");
            listTypes.Add("Quiz");
            listTypes.Add("Homework");
            listTypes.Add("Classwork");
            ViewBag.Type = new SelectList(listTypes);
            ViewBag.AcademicYearId = new SelectList(_academicYearRepository.Query(x => x), "Id", "Year.Year");

            return View("Edit", classActivity);
        }

        [HttpPost]
        public ActionResult Edit(ClassActivityEditModel modelClassActivity)
        {
            ClassActivity myClassActivity = _classActivityRepository.GetById(modelClassActivity.Id);
            myClassActivity.Name = modelClassActivity.DisplayName;
            myClassActivity.Type = modelClassActivity.Type;
            myClassActivity.Description = modelClassActivity.Description;
            myClassActivity.Value = modelClassActivity.Value;
            myClassActivity.AcademicYear = _academicYearRepository.GetById(modelClassActivity.AcademicYearId);

            ClassActivity classactivity = _classActivityRepository.Update(myClassActivity);
            _classActivityRepository.SaveChanges();
            const string title = "Actividad Actualizada";
            string content = "La actividad --" + classactivity.Name + "-- ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            ClassActivity classactivity = _classActivityRepository.Delete(_classActivityRepository.GetById(id));
            _classActivityRepository.SaveChanges();

            const string title = "Actividad Eliminada";
            string content = "La activdad --" + classactivity.Name + "-- ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            var listTypes = new List<string>();
            listTypes.Add("Exam");
            listTypes.Add("Quiz");
            listTypes.Add("Homework");
            listTypes.Add("Classwork");
            ViewBag.Type = new SelectList(listTypes);
            ViewBag.AcademicYearId = new SelectList(_academicYearRepository.Query(x => x), "Id", "Year.Year");
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(ClassActivityRegisterModel modelClassActivity)
        {
            var myClassActivity = new ClassActivity
                                  {
                                      Name = modelClassActivity.DisplayName,
                                      Type = modelClassActivity.Type,
                                      Description = modelClassActivity.Description,
                                      Value = modelClassActivity.Value,
                                      AcademicYear = _academicYearRepository.GetById(modelClassActivity.AcademicYearId)
                                  };

            ClassActivity classactivity = _classActivityRepository.Create(myClassActivity);
            _classActivityRepository.SaveChanges();
            const string title = "Actividad Agregada";
            string content = "La Actividad --" + classactivity.Name + "-- ha sido agregada exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "SUCCESS",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }
    
    }
}