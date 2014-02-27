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
                    StartDate = x.StartDate,
                    BloodType = x.BloodType,
                    AccountNumber = x.AccountNumber,
                    Biography = x.Biography,
                    StudentID = x.PeopleID,
                    FullName = x.FullName,
                    Tutor1 = x.Tutor1 == null ? null : x.Tutor1.FullName,
                    Tutor2 = x.Tutor2 == null ? null : x.Tutor2.FullName
                }));
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
                DateOfBirth = thisStudent.DateOfBirth.Date,
                Gender = thisStudent.Gender,
                Nationality = thisStudent.Nationality,
                State = thisStudent.State,
                City = thisStudent.City,
                StreetAddress = thisStudent.StreetAddress,
                StartDate = thisStudent.StartDate.Date,
                Id = thisStudent.PeopleID,
                BloodType = thisStudent.BloodType,
                AccountNumber = thisStudent.AccountNumber,
                Biography = thisStudent.Biography,
                Tutor1Id = thisStudent.Tutor1 == null ? -1 : thisStudent.Tutor1.PeopleID,
                Tutor2Id = thisStudent.Tutor2 == null ? -1 : thisStudent.Tutor2.PeopleID
            };

            ViewBag.Tutor1Id = new SelectList(_parentRepo.Query(x => x), "PeopleID", "FullName", thisStudent.Tutor1 == null ? -1 : thisStudent.Tutor1.PeopleID);
            ViewBag.Tutor2Id = new SelectList(_parentRepo.Query(x => x), "PeopleID", "FullName", thisStudent.Tutor2 == null ? -1 : thisStudent.Tutor2.PeopleID);

            return View("Edit", student);
        }

        [HttpPost]
        public ActionResult Edit(StudentEditModel modelStudent)
        {
            var myStudent = _studentRepo.GetById(modelStudent.Id);

            myStudent.FirstName = modelStudent.FirstName;
            myStudent.LastName = modelStudent.LastName;
            myStudent.FullName = (modelStudent.FirstName + " " + modelStudent.LastName).Trim();
            myStudent.IDNumber = modelStudent.IDNumber;
            myStudent.DateOfBirth = modelStudent.DateOfBirth;
            myStudent.Gender = modelStudent.Gender;
            myStudent.Nationality = modelStudent.Nationality;
            myStudent.State = modelStudent.State;
            myStudent.City = modelStudent.City;
            myStudent.StreetAddress = modelStudent.StreetAddress;
            myStudent.StartDate = modelStudent.StartDate;
            myStudent.BloodType = modelStudent.BloodType;
            myStudent.AccountNumber = modelStudent.AccountNumber;
            myStudent.Biography = modelStudent.Biography;
            myStudent.Tutor1 = _parentRepo.GetById(modelStudent.Tutor1Id);
            myStudent.Tutor2 = _parentRepo.GetById(modelStudent.Tutor2Id);

            var student = _studentRepo.Update(myStudent);
            const string title = "Estudiante Actualizado";
            var content = "El estudiante ha sido actualizado exitosamente.";

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

            const string title = "Usuario Eliminado";
            var content = "El estudiante ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO", MessageTitle = title, MessageContent = content
            };

            return RedirectToAction("Index");
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
                DateOfBirth = modelStudent.DateOfBirth.Date,
                Gender = modelStudent.Gender,
                Nationality = modelStudent.Nationality,
                State = modelStudent.State,
                City = modelStudent.City,
                StreetAddress = modelStudent.StreetAddress,
                StartDate = modelStudent.StartDate.Date,
                BloodType = modelStudent.BloodType,
                AccountNumber = modelStudent.AccountNumber,
                Biography = modelStudent.Biography,
                Tutor1 = _parentRepo.GetById(modelStudent.Tutor1),
                Tutor2 = _parentRepo.GetById(modelStudent.Tutor2)
            };

            var student = _studentRepo.Create(myStudent);
            const string title = "Estudiante Agregado";
            var content = "El estudiante ha sido agregado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "SUCCESS",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }
    }
}
