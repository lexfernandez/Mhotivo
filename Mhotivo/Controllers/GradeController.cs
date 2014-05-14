using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        //
        // GET: /Grade/

        public ActionResult Index()
        {
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }

            IEnumerable<DisplayGradeModel> displayGradeModels =
                _gradeRepository.Query(x => x).ToList().Select(x => new DisplayGradeModel
                                                                    {
                                                                        Id = x.Id,
                                                                        Name = x.Name,
                                                                        EducationLevel = x.EducationLevel
                                                                    });

            return View(displayGradeModels);
        }

        //
        // GET: /Grade/Details/5

        public ActionResult Details(long id)
        {
            Grade thisgrade = _gradeRepository.GetById(id);
            var grade = new DisplayGradeModel
                        {
                            Id = thisgrade.Id,
                            Name = thisgrade.Name,
                            EducationLevel = thisgrade.EducationLevel
                        };

            return View("Details", grade);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            Grade thisGrade = _gradeRepository.GetById(id);
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
            Grade myGrade = _gradeRepository.GetById(modelGrade.Id);
            myGrade.Name = modelGrade.Name;
            myGrade.EducationLevel = modelGrade.EducationLevel;

            Grade grade = _gradeRepository.Update(myGrade);
            _gradeRepository.SaveChanges();
            const string title = "Padre o Tutor Actualizado";
            string content = "El Alumno " + myGrade.Name + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Details/" + modelGrade.Id);
        }

        //
        // GET: /Grade/Add

        public ActionResult Add()
        {
            return View("Create");
        }

        //
        // POST: /Grade/Add

        [HttpPost]
        public ActionResult Add(GradeRegisterModel modelGrade)
        {
            var myGrade = new Grade
                          {
                              Name = modelGrade.Name,
                              EducationLevel = modelGrade.EducationLevel
                          };

            Grade grade = _gradeRepository.Create(myGrade);
            _gradeRepository.SaveChanges();
            const string title = "Alumno Agregado al Grado";
            string content = "El Alumno " + myGrade.Name + " ha sido agregado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "SUCCESS",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        //
        // GET: /Grade/Edit/5

        public ActionResult Edit(int id)
        {
            Grade thisGrade = _gradeRepository.GetById(id);
            var grade = new GradeEditModel
                        {
                            Id = thisGrade.Id,
                            Name = thisGrade.Name,
                            EducationLevel = thisGrade.EducationLevel
                        };

            return View("Edit", grade);
        }

        //
        // POST: /Grade/Edit/5

        [HttpPost]
        public ActionResult Edit(GradeEditModel modelGrade)
        {
            Grade myGrade = _gradeRepository.GetById(modelGrade.Id);

            myGrade.Name = modelGrade.Name;
            myGrade.EducationLevel = modelGrade.EducationLevel;

            Grade grade = _gradeRepository.Update(myGrade);
            _gradeRepository.SaveChanges();
            const string title = "Grado Actualizado";
            string content = "El Alumno " + myGrade.Name + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }


        //
        // POST: /Grade/Delete/5

        [HttpPost]
        public ActionResult Delete(long id)
        {
            Grade grade = _gradeRepository.GetById(id);
            _gradeRepository.Delete(grade);
            _gradeRepository.SaveChanges();

            const string title = "Alumno ha sido Eliminado del Grado";
            string content = "El Alumno " + grade.Name + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ContactAdd(long id)
        {
            var model = new ContactInformationRegisterModel
                        {
                            Id = (int) id,
                            Controller = "Parent"
                        };
            return View("ContactAdd", model);
        }
    }
}