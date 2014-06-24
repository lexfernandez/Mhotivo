using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public GradeController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        //
        // GET: /Grade/

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
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
            var content = "El Alumno " + myGrade.Name + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

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

            if (IsNameAvailble(modelGrade.Name))
            {
                Grade grade = _gradeRepository.Create(myGrade);
                _gradeRepository.SaveChanges();
                const string title = "Alumno Agregado al Grado";
                var content = "El Alumno " + myGrade.Name + " ha sido agregado exitosamente.";
                _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);
            }
            else
            {
                const string title = "Error";
                var content = "El Alumno " + myGrade.Name + " no fue agregado.";
                _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.ErrorMessage);
            }
            

            

            return RedirectToAction("Index");
        }

        public bool IsNameAvailble(string name)
        {
            var tag = _gradeRepository.First(g => g.Name.CompareTo(name) == 0);
            if (tag == null)
            {
                return true;
            }

            return false;
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
            var content = "El Alumno " + myGrade.Name + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

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
            var content = "El Alumno " + grade.Name + " ha sido eliminado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

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