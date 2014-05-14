using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository, IParentRepository parentRepository,
            IContactInformationRepository contactInformationRepository)
        {
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _contactInformationRepository = contactInformationRepository;
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

            return View(_studentRepository.GetAllStudents());
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
                                         Controller = "Student"
                                     };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            StudentEditModel student = _studentRepository.GetStudentEditModelById(id);

            ViewBag.Tutor1Id = new SelectList(_parentRepository.Query(x => x), "Id", "FullName",
                student.FirstParent);
            ViewBag.Tutor2Id = new SelectList(_parentRepository.Query(x => x), "Id", "FullName",
                student.SecondParent);

            return View("Edit", student);
        }

        [HttpPost]
        public ActionResult Edit(StudentEditModel modelStudent)
        {
            Student myStudent = _studentRepository.GetById(modelStudent.Id);
            _studentRepository.UpdateStudentFromStudentEditModel(modelStudent, myStudent);

            const string title = "Estudiante Actualizado";
            string content = "El estudiante " + myStudent.FullName + " ha sido actualizado exitosamente.";

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
            Student student = _studentRepository.Delete(id);

            const string title = "Estudiante Eliminado";
            string content = "El estudiante " + student.FullName + " ha sido eliminado exitosamente.";
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
                            Controller = "Student"
                        };
            return View("ContactAdd", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Tutor1Id = new SelectList(_parentRepository.Query(x => x), "Id", "FullName");
            ViewBag.Tutor2Id = new SelectList(_parentRepository.Query(x => x), "Id", "FullName");
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(StudentRegisterModel modelStudent)
        {
            Student myStudent = _studentRepository.GenerateStudentFromRegisterModel(modelStudent);

            Student student = _studentRepository.Create(myStudent);
            const string title = "Estudiante Agregado";
            string content = "El estudiante " + myStudent.FullName + " ha sido agregado exitosamente.";
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
            DisplayStudentModel student = _studentRepository.GetStudentDisplayModelById(id);

            return View("Details", student);
        }

        [HttpPost]
        public ActionResult Details(DisplayStudentModel modelStudent)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            StudentEditModel student = _studentRepository.GetStudentEditModelById(id);

            return View("DetailsEdit", student);
        }

        [HttpPost]
        public ActionResult DetailsEdit(StudentEditModel modelStudent)
        {
            Student myStudent = _studentRepository.GetById(modelStudent.Id);
            _studentRepository.UpdateStudentFromStudentEditModel(modelStudent, myStudent);

            const string title = "Estudiante Actualizado";
            string content = "El estudiante " + myStudent.FullName + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Details/" + modelStudent.Id);
        }
    }
}