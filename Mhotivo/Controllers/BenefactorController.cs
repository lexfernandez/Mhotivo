using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class BenefactorController : Controller
    {
        private readonly IBenefactorRepository _benefactorRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IContactInformationRepository _contactInformationRepository;

        public BenefactorController(IBenefactorRepository benefactorRepository, IStudentRepository studentRepository, IContactInformationRepository contactInformationRepository)
        {
            _benefactorRepository = benefactorRepository;
            _studentRepository = studentRepository;
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

            return View(_benefactorRepository.GettAllBenefactors());
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
                var content = "Elimine algunos estudiantes antes de continuar.";

                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "INFO",
                    MessageTitle = title,
                    MessageContent = content
                };
                return RedirectToAction("Index");
            }
            else
            {
                var myBenefactor = _benefactorRepository.GetById(modelBenefactor.Id);
                _benefactorRepository.UpdateBenefactorFromBenefactorEditModel(modelBenefactor, myBenefactor);

                const string title = "Beneficiario Actualizado";
                var content = "El Beneficiario " + myBenefactor.FullName + " ha sido actualizado exitosamente.";

                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "INFO",
                    MessageTitle = title,
                    MessageContent = content
                };
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            var benefactor = _benefactorRepository.Delete(id);

            const string title = "Padre o Tutor Eliminado";
            var content = "El Padre o Tutor " + benefactor.FullName + " ha sido eliminado exitosamente.";
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

            var myBenefactor = _benefactorRepository.GenerateBenefactorFromRegisterModel(modelBenefactor);

            var benefactor = _benefactorRepository.Create(myBenefactor);
            const string title = "Padre o Tutor Agregado";
            var content = "El Padre o Tutor " + myBenefactor.FullName + "ha sido agregado exitosamente.";
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
            var benefactor = _benefactorRepository.GetBenefactorDisplayModelById(id);

            return View("Details", benefactor);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            var benefactor = _benefactorRepository.GetBenefactorEditModelById(id);
            
            return View("DetailsEdit", benefactor);
        }

        [HttpPost]
        public ActionResult DetailsEdit(BenefactorEditModel modelBenefactor)
        {
            if (modelBenefactor.StudentsCount > modelBenefactor.Capacity)
            {
                string title = "Beneficiario No Puede Tener Menos de " + modelBenefactor.StudentsCount;
                var content = "Elimine algunos estudiantes antes de continuar.";

                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "INFO",
                    MessageTitle = title,
                    MessageContent = content
                };
                return RedirectToAction("DetailsEdit/" + modelBenefactor.Id);
            }
            else
            {
                var myBenefactor = _benefactorRepository.GetById(modelBenefactor.Id);
                _benefactorRepository.UpdateBenefactorFromBenefactorEditModel(modelBenefactor, myBenefactor);
                
                const string title = "Beneficiario Actualizado";
                var content = "El Beneficiario " + myBenefactor.FullName + " ha sido actualizado exitosamente.";

                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "INFO",
                    MessageTitle = title,
                    MessageContent = content
                };
                return RedirectToAction("Details/" + modelBenefactor.Id);
            }
        }

        [HttpGet]
        public ActionResult StudentEdit(long id)
        {
            var thisStudent = _studentRepository.GetById(id);
            var student = new StudentBenefactorEditModel
            {
                OldID = (int) id,
                BenefactorID = thisStudent.Benefactor == null ? -1 : thisStudent.Benefactor.PeopleId

            };
            ViewBag.NewID = new SelectList(_studentRepository.Query(x => x), "PeopleID", "FullName", student.OldID);
            return View("StudentEdit", student);
        }

        [HttpGet]
        public ActionResult StudentAdd(long id)
        {
            var student = new StudentBenefactorEditModel
            {
                BenefactorID = (int)id
            };
            ViewBag.NewID = new SelectList(_studentRepository.Query(x => x), "PeopleID", "FullName");
            return View("StudentAdd", student);
        }

        [HttpPost]
        public ActionResult StudentAdd(StudentBenefactorEditModel modelStudent)
        {
            var benefactor = _benefactorRepository.GetById(modelStudent.BenefactorID);
            if (benefactor != null)
            {
                if (benefactor.Capacity > benefactor.Students.Count)
                {
                    var myStudent = _studentRepository.GetById(modelStudent.NewID);
                    myStudent.Benefactor = benefactor;
                    _studentRepository.Update(myStudent);
                }
            }
            return RedirectToAction("Details/" + modelStudent.BenefactorID);
        }

        [HttpPost]
        public ActionResult StudentEdit(StudentBenefactorEditModel modelStudent)
        {
            if (modelStudent.NewID <= 0)
            {
                var myStudent = _studentRepository.GetById(modelStudent.OldID);
                myStudent.Benefactor = null;
                var student = _studentRepository.Update(myStudent);
                myStudent = _studentRepository.GetById(modelStudent.NewID);
            }
            else if (modelStudent.OldID != modelStudent.NewID)
            {
                var myStudent = _studentRepository.GetById(modelStudent.NewID);
                if (myStudent.Benefactor == null || myStudent.Benefactor.PeopleId != modelStudent.BenefactorID)
                {
                    myStudent.Benefactor = _benefactorRepository.GetById(modelStudent.BenefactorID);
                    var student = _studentRepository.Update(myStudent);
                    myStudent = _studentRepository.GetById(modelStudent.OldID);
                    myStudent.Benefactor = null;
                    student = _studentRepository.Update(myStudent);
                }
            }
            return RedirectToAction("Details/" + modelStudent.BenefactorID);
        }

        [HttpPost]
        public ActionResult DeleteStudent(long id)
        {
            var myStudent = _studentRepository.GetById(id);
            long ID = myStudent.Benefactor.PeopleId;
            myStudent.Benefactor = null;
            var student = _studentRepository.Update(myStudent);

            const string title = "Estudiante Eliminado";
            var content = "El estudiante " + myStudent.FullName + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Details/" + ID);
        }
    }
}
