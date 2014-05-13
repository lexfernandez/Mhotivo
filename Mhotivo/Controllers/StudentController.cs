using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IContactInformationRepository _contactInformationRepository;

        public StudentController(IStudentRepository studentRepository, IParentRepository parentRepository, IContactInformationRepository contactInformationRepository)
        {
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _contactInformationRepository = contactInformationRepository;
        }

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

            return View(_studentRepository.GetAllStudents());
        }

        [HttpGet]
        public ActionResult ContactEdit(long id)
        {
            var thisContactInformation = _contactInformationRepository.GetById(id);
            var contactInformation = new ContactInformationEditModel
            {
                Type = thisContactInformation.Type,
                Value = thisContactInformation.Value,
                Id = thisContactInformation.ContactId,
                people = thisContactInformation.People,
                Controller = "Student"
            };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var student = _studentRepository.GetStudentEditModelById(id);

            ViewBag.Tutor1Id = new SelectList(_parentRepository.Query(x => x), "PeopleID", "FullName", student.Tutor1Id);
            ViewBag.Tutor2Id = new SelectList(_parentRepository.Query(x => x), "PeopleID", "FullName", student.Tutor2Id);

            return View("Edit", student);
        }

        [HttpPost]
        public ActionResult Edit(StudentEditModel modelStudent)
        {
            var myStudent = _studentRepository.GetById(modelStudent.Id);
            _studentRepository.UpdateStudentFromStudentEditModel(modelStudent, myStudent);

            const string title = "Estudiante Actualizado";
            var content = "El estudiante " + myStudent.FullName + " ha sido actualizado exitosamente.";

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
            var student = _studentRepository.Delete(id);

            const string title = "Estudiante Eliminado";
            var content = "El estudiante " + student.FullName + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO", MessageTitle = title, MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ContactAdd(long id)
        {
            var model = new ContactInformationRegisterModel
            {
                PeopleId = (int) id,
                Controller = "Student"
            };
            return View("ContactAdd", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Tutor1Id = new SelectList(_parentRepository.Query(x => x), "PeopleID", "FullName");
            ViewBag.Tutor2Id = new SelectList(_parentRepository.Query(x => x), "PeopleID", "FullName");
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(StudentRegisterModel modelStudent)
        {
            var myStudent = _studentRepository.GenerateStudentFromRegisterModel(modelStudent);
            
            var student = _studentRepository.Create(myStudent);
            const string title = "Estudiante Agregado";
            var content = "El estudiante " + myStudent.FullName + " ha sido agregado exitosamente.";
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
            var student = _studentRepository.GetStudentDisplayModelById(id);

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
            var student = _studentRepository.GetStudentEditModelById(id);
            
            return View("DetailsEdit", student);
        }

        [HttpPost]
        public ActionResult DetailsEdit(StudentEditModel modelStudent)
        {
            var myStudent = _studentRepository.GetById(modelStudent.Id);
            _studentRepository.UpdateStudentFromStudentEditModel(modelStudent, myStudent);
            
            const string title = "Estudiante Actualizado";
            var content = "El estudiante " + myStudent.FullName + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Details/" + modelStudent.Id);
        }
    }
}
