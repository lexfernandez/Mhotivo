using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class BenefactorController : Controller
    {
        private readonly IBenefactorRepository _benefactorRepository;
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IStudentRepository _studentRepository;

        public BenefactorController(IBenefactorRepository benefactorRepository, IStudentRepository studentRepository,
            IContactInformationRepository contactInformationRepository)
        {
            _benefactorRepository = benefactorRepository;
            _studentRepository = studentRepository;
            _contactInformationRepository = contactInformationRepository;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }

            return View(_benefactorRepository.GettAllBenefactors());
        }

        [HttpGet]
        public ActionResult ContactEdit(long id)
        {
            ContactInformation thisContactInformation = _contactInformationRepository.GetById(id);
            var contactInformation = new ContactInformationEditModel
                                     {
                                         Type = thisContactInformation.Type,
                                         Value = thisContactInformation.Value,
                                         Id = thisContactInformation.Id,
                                         People = thisContactInformation.People,
                                         Controller = "Benefactor"
                                     };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View("Edit", _benefactorRepository.GetBenefactorEditModelById(id));
        }

        [HttpPost]
        public ActionResult Edit(BenefactorEditModel modelBenefactor)
        {
            if (modelBenefactor.Capacity < modelBenefactor.StudentsCount)
            {
                string title = "Beneficiario No Puede Tener Menos de " + modelBenefactor.StudentsCount;
                string content = "Elimine algunos estudiantes antes de continuar.";

                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "INFO",
                                              Title = title,
                                              Content = content
                                          };
                return RedirectToAction("Index");
            }
            else
            {
                Benefactor myBenefactor = _benefactorRepository.GetById(modelBenefactor.Id);
                _benefactorRepository.UpdateBenefactorFromBenefactorEditModel(modelBenefactor, myBenefactor);

                const string title = "Beneficiario Actualizado";
                string content = "El Beneficiario " + myBenefactor.FullName + " ha sido actualizado exitosamente.";

                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "INFO",
                                              Title = title,
                                              Content = content
                                          };
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            Benefactor benefactor = _benefactorRepository.Delete(id);

            const string title = "Padre o Tutor Eliminado";
            string content = "El Padre o Tutor " + benefactor.FullName + " ha sido eliminado exitosamente.";
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
                            Controller = "Benefactor"
                        };
            return View("ContactAdd", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(BenefactorRegisterModel modelBenefactor)
        {
            Benefactor myBenefactor = _benefactorRepository.GenerateBenefactorFromRegisterModel(modelBenefactor);

            Benefactor benefactor = _benefactorRepository.Create(myBenefactor);
            const string title = "Padre o Tutor Agregado";
            string content = "El Padre o Tutor " + myBenefactor.FullName + "ha sido agregado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "SUCCESS",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            DisplayBenefactorModel benefactor = _benefactorRepository.GetBenefactorDisplayModelById(id);

            return View("Details", benefactor);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            BenefactorEditModel benefactor = _benefactorRepository.GetBenefactorEditModelById(id);

            return View("DetailsEdit", benefactor);
        }

        [HttpPost]
        public ActionResult DetailsEdit(BenefactorEditModel modelBenefactor)
        {
            if (modelBenefactor.StudentsCount > modelBenefactor.Capacity)
            {
                string title = "Beneficiario No Puede Tener Menos de " + modelBenefactor.StudentsCount;
                string content = "Elimine algunos estudiantes antes de continuar.";

                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "INFO",
                                              Title = title,
                                              Content = content
                                          };
                return RedirectToAction("DetailsEdit/" + modelBenefactor.Id);
            }
            else
            {
                Benefactor myBenefactor = _benefactorRepository.GetById(modelBenefactor.Id);
                _benefactorRepository.UpdateBenefactorFromBenefactorEditModel(modelBenefactor, myBenefactor);

                const string title = "Beneficiario Actualizado";
                string content = "El Beneficiario " + myBenefactor.FullName + " ha sido actualizado exitosamente.";

                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "INFO",
                                              Title = title,
                                              Content = content
                                          };
                return RedirectToAction("Details/" + modelBenefactor.Id);
            }
        }

        [HttpGet]
        public ActionResult StudentEdit(long id)
        {
            Student thisStudent = _studentRepository.GetById(id);
            var student = new StudentBenefactorEditModel
                          {
                              OldId = (int) id,
                              Id = thisStudent.Benefactor == null ? -1 : thisStudent.Benefactor.Id
                          };
            ViewBag.NewID = new SelectList(_studentRepository.Query(x => x), "Id", "FullName", student.OldId);
            return View("StudentEdit", student);
        }

        [HttpGet]
        public ActionResult StudentAdd(long id)
        {
            var student = new StudentBenefactorEditModel
                          {
                              Id = (int) id
                          };
            ViewBag.NewID = new SelectList(_studentRepository.Query(x => x), "Id", "FullName");
            return View("StudentAdd", student);
        }

        [HttpPost]
        public ActionResult StudentAdd(StudentBenefactorEditModel modelStudent)
        {
            Benefactor benefactor = _benefactorRepository.GetById(modelStudent.Id);
            if (benefactor != null)
            {
                if (benefactor.Capacity > benefactor.Students.Count)
                {
                    Student myStudent = _studentRepository.GetById(modelStudent.NewId);
                    myStudent.Benefactor = benefactor;
                    _studentRepository.Update(myStudent);
                }
            }
            return RedirectToAction("Details/" + modelStudent.Id);
        }

        [HttpPost]
        public ActionResult StudentEdit(StudentBenefactorEditModel modelStudent)
        {
            if (modelStudent.NewId <= 0)
            {
                Student myStudent = _studentRepository.GetById(modelStudent.OldId);
                myStudent.Benefactor = null;
                Student student = _studentRepository.Update(myStudent);
                myStudent = _studentRepository.GetById(modelStudent.NewId);
            }
            else if (modelStudent.OldId != modelStudent.NewId)
            {
                Student myStudent = _studentRepository.GetById(modelStudent.NewId);
                if (myStudent.Benefactor == null || myStudent.Benefactor.Id != modelStudent.Id)
                {
                    myStudent.Benefactor = _benefactorRepository.GetById(modelStudent.Id);
                    Student student = _studentRepository.Update(myStudent);
                    myStudent = _studentRepository.GetById(modelStudent.OldId);
                    myStudent.Benefactor = null;
                    student = _studentRepository.Update(myStudent);
                }
            }
            return RedirectToAction("Details/" + modelStudent.Id);
        }

        [HttpPost]
        public ActionResult DeleteStudent(long id)
        {
            Student myStudent = _studentRepository.GetById(id);
            long ID = myStudent.Benefactor.Id;
            myStudent.Benefactor = null;
            Student student = _studentRepository.Update(myStudent);

            const string title = "Estudiante Eliminado";
            string content = "El estudiante " + myStudent.FullName + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Details/" + ID);
        }
    }
}