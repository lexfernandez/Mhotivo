using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;
using Microsoft.SqlServer.Server;

namespace Mhotivo.Controllers
{
    public class EnrollController : Controller
    {
        //
        // GET: /Enroll/

        private readonly IPeopleRepository _peopleRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollRepository _enrollRepository;
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IGradeRepository _gradeRepository;

        public EnrollController(IPeopleRepository peopleRepository, IAcademicYearRepository academicYearRepository, IStudentRepository studentRepository, IEnrollRepository enrollRepository, IGradeRepository gradeRepository)
        {
            _peopleRepository = peopleRepository;
            _studentRepository = studentRepository;
            _enrollRepository = enrollRepository;
            _academicYearRepository = academicYearRepository;
            _gradeRepository = gradeRepository;
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

            return View(_enrollRepository.Query(x => x).ToList()
                .Select(x => new DisplayEnrollStudents
                {
                    EnrollID = x.Id,
                    FullName = x.Student.FullName,
                    UrlPicture = x.Student.UrlPicture,
                    Gender = Utilities.GenderToString(x.Student.Gender),
                    AccountNumber = x.Student.AccountNumber,
                    Grade = x.AcademicYear.Grade.Name,
                    Section = x.AcademicYear.Section
                }));
        }

        [HttpGet]
        public ActionResult Search(string id)
        {
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }

            var model = _enrollRepository.Filter(x => x.Student.FullName.Contains(id)).ToList()
                .Select(x => new DisplayEnrollStudents
                {
                    EnrollID = x.Id,
                    FullName = x.Student.FullName,
                    UrlPicture = x.Student.UrlPicture,
                    Gender = Utilities.GenderToString(x.Student.Gender),
                    AccountNumber = x.Student.AccountNumber,
                    Grade = x.AcademicYear.Grade.Name,
                    Section = x.AcademicYear.Section
                });

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            _enrollRepository.Delete(id);

            const string title = "Matricula Borrada";
            const string content = "El estudiante ha sido eliminado exitosamente de la lista de matriculados.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.StudentId = new SelectList(_peopleRepository.Query(x => x), "PeopleId", "FullName");
            ViewBag.GradeId = new SelectList(_gradeRepository.Query(x => x), "GradeId", "Name");
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(EnrollRegisterModel modelEnroll)
        {
            var Collection = _academicYearRepository.Filter(x => x.Grade.GradeId == modelEnroll.GradeId).ToList();
            var student = _studentRepository.GetById(modelEnroll.StudentId);
            if (Collection.Count > 0)
            {
                foreach (var academicYear in Collection)
                {
                    var myEnroll = new Enroll
                    {
                        AcademicYear = academicYear,
                        Student = student
                    };

                    var enroll = _enrollRepository.Create(myEnroll);
                }
                const string title = "Usuario Agregado";
                var content = "El usuario ha sido matriculado exitosamente.";
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "SUCCESS",
                    MessageTitle = title,
                    MessageContent = content
                };
            }
            else
            {
                const string title = "Usuario No Agregado";
                var content = "El usuario no se logro matricular.";
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "SUCCESS",
                    MessageTitle = title,
                    MessageContent = content
                };
            }

            return RedirectToAction("Index");
        }
    }
}
