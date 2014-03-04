using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepository _studentRepo = StudentRepository.Instance;
        private readonly ParentRepository _parentRepo = ParentRepository.Instance;
        private readonly PeopleRepository _peopleRepo = PeopleRepository.Instance;
        private readonly ContactInformationRepository _contactRepo = ContactInformationRepository.Instance;

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

            return View(_studentRepo.Query(x => x).ToList()
                .Select(x => new DisplayStudentModel
                {
                    StudentID = x.PeopleId,
                    UrlPicture = x.UrlPicture,
                    FullName = x.FullName,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    Nationality = x.Nationality,
                    Address = x.Address,
                    City = x.City,
                    State = x.State,
                    Country = x.Country,
                    Gender = _peopleRepo.SexLabel(x.Gender),
                    StartDate = x.StartDate.ToShortDateString(),
                    BloodType = x.BloodType,
                    AccountNumber = x.AccountNumber,
                    Biography = x.Biography,
                    Tutor1 = x.Tutor1 == null ? null : x.Tutor1.FullName,
                    Tutor2 = x.Tutor2 == null ? null : x.Tutor2.FullName
                }));
        }

        [HttpGet]
        public ActionResult ContactEdit(long id)
        {
            var thisContactInformation = _contactRepo.GetById(id);
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
            var thisStudent = _studentRepo.GetById(id);
            var student = new StudentEditModel
            {
                FirstName = thisStudent.FirstName,
                LastName = thisStudent.LastName,
                FullName = (thisStudent.FirstName + " " + thisStudent.LastName).Trim(),
                IDNumber = thisStudent.IDNumber,
                BirthDate = thisStudent.BirthDate.ToShortDateString(),
                Gender = _peopleRepo.SexLabel(thisStudent.Gender),
                Nationality = thisStudent.Nationality,
                Country = thisStudent.Country,
                State = thisStudent.State,
                City = thisStudent.City,
                Address = thisStudent.Address,
                StartDate = thisStudent.StartDate.Date,
                Id = thisStudent.PeopleId,
                BloodType = thisStudent.BloodType,
                AccountNumber = thisStudent.AccountNumber,
                Biography = thisStudent.Biography,
                Tutor1Id = thisStudent.Tutor1 == null ? -1 : thisStudent.Tutor1.PeopleId,
                Tutor2Id = thisStudent.Tutor2 == null ? -1 : thisStudent.Tutor2.PeopleId
            };

            ViewBag.Tutor1Id = new SelectList(_parentRepo.Query(x => x), "PeopleID", "FullName", thisStudent.Tutor1 == null ? -1 : thisStudent.Tutor1.PeopleId);
            ViewBag.Tutor2Id = new SelectList(_parentRepo.Query(x => x), "PeopleID", "FullName", thisStudent.Tutor2 == null ? -1 : thisStudent.Tutor2.PeopleId);

            return View("Edit", student);
        }

        [HttpPost]
        public ActionResult Edit(StudentEditModel modelStudent)
        {
            var myStudent = _studentRepo.GetById(modelStudent.Id);
            var updateTutor1 = false;
            var updateTutor2 = false;

            myStudent.FirstName = modelStudent.FirstName;
            myStudent.LastName = modelStudent.LastName;
            myStudent.FullName = (modelStudent.FirstName + " " + modelStudent.LastName).Trim();
            myStudent.IDNumber = modelStudent.IDNumber;
            myStudent.BirthDate = DateTime.Parse(modelStudent.BirthDate);
            myStudent.Gender = _peopleRepo.IsMasculino(modelStudent.Gender);
            myStudent.Nationality = modelStudent.Nationality;
            myStudent.State = modelStudent.State;
            myStudent.City = modelStudent.City;
            myStudent.Country = modelStudent.Country;
            myStudent.Address = modelStudent.Address;
            myStudent.StartDate = modelStudent.StartDate;
            myStudent.BloodType = modelStudent.BloodType;
            myStudent.AccountNumber = modelStudent.AccountNumber;
            myStudent.Biography = modelStudent.Biography;
            if (modelStudent.Tutor1Id == 0 || (modelStudent.Tutor1Id != modelStudent.Tutor2Id && (myStudent.Tutor2 == null || myStudent.Tutor2.PeopleId != modelStudent.Tutor1Id) && (myStudent.Tutor1 == null || myStudent.Tutor1.PeopleId != modelStudent.Tutor1Id)))
            {
                myStudent.Tutor1 = _parentRepo.GetById(modelStudent.Tutor1Id);
                updateTutor1 = true;

            }
            _studentRepo.Update(myStudent, updateTutor1, false, false);
            if (modelStudent.Tutor2Id == 0 || (modelStudent.Tutor1Id != modelStudent.Tutor2Id && (myStudent.Tutor1 == null || myStudent.Tutor1.PeopleId != modelStudent.Tutor2Id) && (myStudent.Tutor2 == null || myStudent.Tutor2.PeopleId != modelStudent.Tutor2Id)))
            {
                myStudent.Tutor2 = _parentRepo.GetById(modelStudent.Tutor2Id);
                updateTutor2 = true;
            }

            var student = _studentRepo.Update(myStudent, false, updateTutor2, false);
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
            var student = _studentRepo.Delete(id);

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
            ViewBag.Tutor1Id = new SelectList(_parentRepo.Query(x => x), "PeopleID", "FullName");
            ViewBag.Tutor2Id = new SelectList(_parentRepo.Query(x => x), "PeopleID", "FullName");
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(StudentRegisterModel modelStudent)
        {
            var myStudent = new Student
            {
                FirstName = modelStudent.FirstName,
                LastName = modelStudent.LastName,
                FullName = (modelStudent.FirstName + " " + modelStudent.LastName).Trim(),
                IDNumber = modelStudent.IDNumber,
                BirthDate = DateTime.Parse(modelStudent.BirthDate),
                Gender = _peopleRepo.IsMasculino(modelStudent.Gender),
                Nationality = modelStudent.Nationality,
                Country = modelStudent.Country,
                State = modelStudent.State,
                City = modelStudent.City,
                Address = modelStudent.Address,
                StartDate = modelStudent.StartDate.Date,
                BloodType = modelStudent.BloodType,
                AccountNumber = modelStudent.AccountNumber,
                Biography = modelStudent.Biography,
                Tutor1 = _parentRepo.GetById(modelStudent.Tutor1Id),
                Tutor2 = _parentRepo.GetById(modelStudent.Tutor2Id)
            };
            if(myStudent.Tutor1 != null)
                _parentRepo.Detach(myStudent.Tutor1);
            if(myStudent.Tutor2 != null)
                _parentRepo.Detach(myStudent.Tutor2);
            var student = _studentRepo.Create(myStudent);
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
            var thisStudent = _studentRepo.GetById(id);
            var student = new DisplayStudentModel
            {
                StudentID = thisStudent.PeopleId,
                IDNumber = thisStudent.IDNumber,
                UrlPicture = thisStudent.UrlPicture,
                FirstName = thisStudent.FirstName,
                LastName = thisStudent.LastName,
                FullName = thisStudent.FullName,
                BirthDate = thisStudent.BirthDate.ToShortDateString(),
                Nationality = thisStudent.Nationality,
                Address = thisStudent.Address,
                City = thisStudent.City,
                State = thisStudent.State,
                Country = thisStudent.Country,
                Gender = _peopleRepo.SexLabel(thisStudent.Gender),
                StartDate = thisStudent.StartDate.ToShortDateString(),
                BloodType = thisStudent.BloodType,
                AccountNumber = thisStudent.AccountNumber,
                Biography = thisStudent.Biography,
                Contacts = thisStudent.Contacts,
                Tutor1 = thisStudent.Tutor1 == null ? null : thisStudent.Tutor1.FullName,
                Tutor2 = thisStudent.Tutor2 == null ? null : thisStudent.Tutor2.FullName,
            };

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
            var thisStudent = _studentRepo.GetById(id);
            var student = new StudentEditModel
            {
                FirstName = thisStudent.FirstName,
                LastName = thisStudent.LastName,
                FullName = (thisStudent.FirstName + " " + thisStudent.LastName).Trim(),
                IDNumber = thisStudent.IDNumber,
                BirthDate = thisStudent.BirthDate.ToShortDateString(),
                Gender = _peopleRepo.SexLabel(thisStudent.Gender),
                Nationality = thisStudent.Nationality,
                Country = thisStudent.Country,
                State = thisStudent.State,
                City = thisStudent.City,
                Address = thisStudent.Address,
                StartDate = thisStudent.StartDate.Date,
                Id = thisStudent.PeopleId,
                BloodType = thisStudent.BloodType,
                AccountNumber = thisStudent.AccountNumber,
                Biography = thisStudent.Biography,
                Contacts = thisStudent.Contacts,
                Tutor1Id = thisStudent.Tutor1 == null ? -1 : thisStudent.Tutor1.PeopleId,
                Tutor2Id = thisStudent.Tutor2 == null ? -1 : thisStudent.Tutor2.PeopleId
            };

            ViewBag.Tutor1Id = new SelectList(_parentRepo.Query(x => x), "PeopleID", "FullName", thisStudent.Tutor1 == null ? -1 : thisStudent.Tutor1.PeopleId);
            ViewBag.Tutor2Id = new SelectList(_parentRepo.Query(x => x), "PeopleID", "FullName", thisStudent.Tutor2 == null ? -1 : thisStudent.Tutor2.PeopleId);

            return View("DetailsEdit", student);
        }

        [HttpPost]
        public ActionResult DetailsEdit(StudentEditModel modelStudent)
        {
            var myStudent = _studentRepo.GetById(modelStudent.Id);
            var updateTutor1 = false;
            var updateTutor2 = false;

            myStudent.FirstName = modelStudent.FirstName;
            myStudent.LastName = modelStudent.LastName;
            myStudent.FullName = (modelStudent.FirstName + " " + modelStudent.LastName).Trim();
            myStudent.IDNumber = modelStudent.IDNumber;
            myStudent.BirthDate = DateTime.Parse(modelStudent.BirthDate);
            myStudent.Gender = _peopleRepo.IsMasculino(modelStudent.Gender);
            myStudent.Nationality = modelStudent.Nationality;
            myStudent.State = modelStudent.State;
            myStudent.City = modelStudent.City;
            myStudent.Country = modelStudent.Country;
            myStudent.Address = modelStudent.Address;
            myStudent.StartDate = modelStudent.StartDate;
            myStudent.BloodType = modelStudent.BloodType;
            myStudent.AccountNumber = modelStudent.AccountNumber;
            myStudent.Contacts = modelStudent.Contacts;
            myStudent.Biography = modelStudent.Biography;
            if (modelStudent.Tutor1Id == 0 || (modelStudent.Tutor1Id != modelStudent.Tutor2Id && (myStudent.Tutor2 == null || myStudent.Tutor2.PeopleId != modelStudent.Tutor1Id) && (myStudent.Tutor1 == null || myStudent.Tutor1.PeopleId != modelStudent.Tutor1Id)))
            {
                myStudent.Tutor1 = _parentRepo.GetById(modelStudent.Tutor1Id);
                updateTutor1 = true;

            }
            _studentRepo.Update(myStudent, updateTutor1, false, false);
            if (modelStudent.Tutor2Id == 0 || (modelStudent.Tutor1Id != modelStudent.Tutor2Id && (myStudent.Tutor1 == null || myStudent.Tutor1.PeopleId != modelStudent.Tutor2Id) && (myStudent.Tutor2 == null || myStudent.Tutor2.PeopleId != modelStudent.Tutor2Id)))
            {
                myStudent.Tutor2 = _parentRepo.GetById(modelStudent.Tutor2Id);
                updateTutor2 = true;
            }

            var student = _studentRepo.Update(myStudent, true, updateTutor2, false);
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
