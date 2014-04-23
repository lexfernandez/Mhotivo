using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class GradeController : Controller
    {
        private readonly GradeRepository _gradeRepo = GradeRepository.Instance;
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }

            return View(_gradeRepo.Query(x => x).ToList()
                .Select(x => new DisplayGradeModel
                {
                    GradeId = x.Id,
                    Name =  x.Name,
                    EducationLevel = x.EducationLevel
                    
                }));
        }
       
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var thisGrade = _gradeRepo.GetById(id);
            var grade = new GradeEditModel
            {
                
                Id = thisGrade.Id,
                Name = thisGrade.Name,
                EducationLevel = thisGrade.EducationLevel
            };

            return View("Edit", grade);
        }

        [HttpPost]
        public ActionResult Edit(GradeEditModel modelGrade)
        {
            var myGrade = _gradeRepo.GetById(modelGrade.Id);

            myGrade.Name = modelGrade.Name;
            myGrade.EducationLevel = modelGrade.EducationLevel;
           

            var grade = _gradeRepo.Update(myGrade);
            const string title = "Grado Actualizado";
            var content = "El Alumno " + myGrade.Name + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            var grade = _gradeRepo.Delete(id);

            const string title = "Alumno ha sido Eliminado del Grado";
            var content = "El Alumno " + grade.Name + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ContactAdd(long id)
        {
            var model = new ContactInformationRegisterModel
            {
                PeopleId = (int)id,
                Controller = "Parent"
            };
            return View("ContactAdd", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(GradeRegisterModel modelGrade)
        {
            var myGrade = new Grade
            {
                Name= modelGrade.Name,
                EducationLevel = modelGrade.EducationLevel
            };

            var grade = _gradeRepo.Create(myGrade);
            const string title = "Alumno Agregado al Grado";
            var content = "El Alumno " + myGrade.Name+ " ha sido agregado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "SUCCESS",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var thisgrade = _gradeRepo.GetById(id);
            var grade = new DisplayGradeModel
            {
                GradeId = thisgrade.Id,
                Name = thisgrade.Name,
                EducationLevel = thisgrade.EducationLevel
            };

            return View("Details", grade);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            var thisGrade = _gradeRepo.GetById(id);
            var grade = new GradeEditModel
            {
               
                Id = thisGrade.Id,
                Name = thisGrade.Name,
                EducationLevel = thisGrade.EducationLevel
            };
            return View("DetailsEdit", grade);
        }

        [HttpPost]
        public ActionResult DetailsEdit(GradeEditModel modelGrade)
        {
            var myGrade = _gradeRepo.GetById(modelGrade.Id);
            myGrade.Name = modelGrade.Name;
            myGrade.EducationLevel = modelGrade.EducationLevel;
           
            var grade = _gradeRepo.Update(myGrade);
            const string title = "Padre o Tutor Actualizado";
            var content = "El Alumno " + myGrade.Name+ " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Details/" + modelGrade.Id);
        }
    }
}
