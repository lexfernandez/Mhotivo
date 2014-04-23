using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class BenefactorController : Controller
    {
        private readonly BenefactorRepository _benefactorRepo = BenefactorRepository.Instance;
        private readonly PeopleRepository _peopleRepo = PeopleRepository.Instance;
        private readonly StudentRepository _studentRepo = StudentRepository.Instance;
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

            return View(_benefactorRepo.Query(x => x).ToList()
                .Select(x => new DisplayBenefactorModel
                {
                    BenefactorID = x.PeopleId,
                    IDNumber = x.IDNumber,
                    UrlPicture = x.UrlPicture,
                    FullName = x.FullName,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    Nationality = x.Nationality,
                    Address = x.Address,
                    City = x.City,
                    State = x.State,
                    Country = x.Country,
                    Gender = _peopleRepo.SexLabel(x.Gender),
                    Contacts = x.Contacts,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Capacity = x.Capacity
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
                Controller = "Benefactor"
            };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var thisBenefactor = _benefactorRepo.GetById(id);
            var benefactor = new BenefactorEditModel
            {
                FirstName = thisBenefactor.FirstName,
                LastName = thisBenefactor.LastName,
                FullName = (thisBenefactor.FirstName + " " + thisBenefactor.LastName).Trim(),
                IDNumber = thisBenefactor.IDNumber,
                BirthDate = thisBenefactor.BirthDate.ToShortDateString(),
                Gender = _peopleRepo.SexLabel(thisBenefactor.Gender),
                Nationality = thisBenefactor.Nationality,
                Country = thisBenefactor.Country,
                State = thisBenefactor.State,
                City = thisBenefactor.City,
                Address = thisBenefactor.Address,
                Id = thisBenefactor.PeopleId,
                StudentsCount = thisBenefactor.Students.Count
            };

            return View("Edit", benefactor);
        }

        [HttpPost]
        public ActionResult Edit(BenefactorEditModel modelBenefactor)
        {
            var myBenefactor = _benefactorRepo.GetById(modelBenefactor.Id);
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
                myBenefactor.FirstName = modelBenefactor.FirstName;
                myBenefactor.LastName = modelBenefactor.LastName;
                myBenefactor.FullName = (modelBenefactor.FirstName + " " + modelBenefactor.LastName).Trim();
                myBenefactor.Country = modelBenefactor.Country;
                myBenefactor.IDNumber = modelBenefactor.IDNumber;
                myBenefactor.BirthDate = DateTime.Parse(modelBenefactor.BirthDate);
                myBenefactor.Gender = _peopleRepo.IsMasculino(modelBenefactor.Gender);
                myBenefactor.Nationality = modelBenefactor.Nationality;
                myBenefactor.State = modelBenefactor.State;
                myBenefactor.City = modelBenefactor.City;
                myBenefactor.Address = modelBenefactor.Address;
                myBenefactor.Capacity = modelBenefactor.Capacity;

                var benefactor = _benefactorRepo.Update(myBenefactor);
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
            var benefactor = _benefactorRepo.Delete(id);

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
            var myBenefactor = new Benefactor
            {
                FirstName = modelBenefactor.FirstName,
                LastName = modelBenefactor.LastName,
                FullName = (modelBenefactor.FirstName + " " + modelBenefactor.LastName).Trim(),
                IDNumber = modelBenefactor.IDNumber,
                BirthDate = DateTime.Parse(modelBenefactor.BirthDate),
                Gender = _peopleRepo.IsMasculino(modelBenefactor.Gender),
                Nationality = modelBenefactor.Nationality,
                State = modelBenefactor.State,
                Country = modelBenefactor.Country,
                City = modelBenefactor.City,
                Address = modelBenefactor.Address,
                Capacity = int.Parse(modelBenefactor.Capacity)
            };

            var benefactor = _benefactorRepo.Create(myBenefactor);
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
            var thisBenefactor = _benefactorRepo.GetById(id);
            var benefactor = new DisplayBenefactorModel
            {
                BenefactorID = thisBenefactor.PeopleId,
                IDNumber = thisBenefactor.IDNumber,
                UrlPicture = thisBenefactor.UrlPicture,
                FirstName = thisBenefactor.FirstName,
                LastName = thisBenefactor.LastName,
                FullName = thisBenefactor.FullName,
                BirthDate = thisBenefactor.BirthDate.ToShortDateString(),
                Nationality = thisBenefactor.Nationality,
                Address = thisBenefactor.Address,
                City = thisBenefactor.City,
                State = thisBenefactor.State,
                Country = thisBenefactor.Country,
                Gender = _peopleRepo.SexLabel(thisBenefactor.Gender),
                Contacts = thisBenefactor.Contacts,
                Capacity = thisBenefactor.Capacity,
                StudentsCount = thisBenefactor.Students.Count,
                Students = thisBenefactor.Students
            };

            return View("Details", benefactor);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            var thisBenefactor = _benefactorRepo.GetById(id);
            var benefactor = new BenefactorEditModel
            {
                FirstName = thisBenefactor.FirstName,
                LastName = thisBenefactor.LastName,
                FullName = (thisBenefactor.FirstName + " " + thisBenefactor.LastName).Trim(),
                IDNumber = thisBenefactor.IDNumber,
                BirthDate = thisBenefactor.BirthDate.ToShortDateString(),
                Gender = _peopleRepo.SexLabel(thisBenefactor.Gender),
                Nationality = thisBenefactor.Nationality,
                Country = thisBenefactor.Country,
                State = thisBenefactor.State,
                City = thisBenefactor.City,
                Address = thisBenefactor.Address,
                Id = thisBenefactor.PeopleId,
                StudentsCount = thisBenefactor.Students.Count
            };
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
                var myBenefactor = _benefactorRepo.GetById(modelBenefactor.Id);
                myBenefactor.FirstName = modelBenefactor.FirstName;
                myBenefactor.LastName = modelBenefactor.LastName;
                myBenefactor.FullName = (modelBenefactor.FirstName + " " + modelBenefactor.LastName).Trim();
                myBenefactor.IDNumber = modelBenefactor.IDNumber;
                myBenefactor.BirthDate = DateTime.Parse(modelBenefactor.BirthDate);
                myBenefactor.Gender = _peopleRepo.IsMasculino(modelBenefactor.Gender);
                myBenefactor.Nationality = modelBenefactor.Nationality;
                myBenefactor.State = modelBenefactor.State;
                myBenefactor.City = modelBenefactor.City;
                myBenefactor.Country = modelBenefactor.Country;
                myBenefactor.Address = modelBenefactor.Address;
                myBenefactor.Capacity = modelBenefactor.Capacity;
                var benefactor = _benefactorRepo.Update(myBenefactor);
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
            var thisStudent = _studentRepo.GetById(id);
            var student = new StudentBenefactorEditModel
            {
                OldID = (int) id,
                BenefactorID = thisStudent.Benefactor == null ? -1 : thisStudent.Benefactor.PeopleId

            };
            ViewBag.NewID = new SelectList(_studentRepo.Query(x => x), "PeopleID", "FullName", student.OldID);
            return View("StudentEdit", student);
        }

        [HttpGet]
        public ActionResult StudentAdd(long id)
        {
            var student = new StudentBenefactorEditModel
            {
                BenefactorID = (int)id
            };
            ViewBag.NewID = new SelectList(_studentRepo.Query(x => x), "PeopleID", "FullName");
            return View("StudentAdd", student);
        }

        [HttpPost]
        public ActionResult StudentAdd(StudentBenefactorEditModel modelStudent)
        {
            var benefactor = _benefactorRepo.GetById(modelStudent.BenefactorID);
            if (benefactor.Capacity > benefactor.Students.Count)
            {
                if (modelStudent.NewID > 0)
                {
                    var myStudent = _studentRepo.GetById(modelStudent.NewID);
                    if (myStudent.Benefactor == null || myStudent.Benefactor.PeopleId != modelStudent.BenefactorID)
                    {
                        myStudent.Benefactor = _benefactorRepo.GetById(modelStudent.BenefactorID);
                        _benefactorRepo.Detach(myStudent.Benefactor);
                        var student = _studentRepo.Update(myStudent, false, false, true);
                    }
                }
            }
            return RedirectToAction("Details/" + modelStudent.BenefactorID);
        }

        [HttpPost]
        public ActionResult StudentEdit(StudentBenefactorEditModel modelStudent)
        {
            if (modelStudent.NewID <= 0)
            {
                var myStudent = _studentRepo.GetById(modelStudent.OldID);
                myStudent.Benefactor = null;
                var student = _studentRepo.Update(myStudent, false, false, false);
                myStudent = _studentRepo.GetById(modelStudent.NewID);
            }
            else if (modelStudent.OldID != modelStudent.NewID)
            {
                var myStudent = _studentRepo.GetById(modelStudent.NewID);
                if (myStudent.Benefactor == null || myStudent.Benefactor.PeopleId != modelStudent.BenefactorID)
                {
                    myStudent.Benefactor = _benefactorRepo.GetById(modelStudent.BenefactorID);
                    _benefactorRepo.Detach(myStudent.Benefactor);
                    var student = _studentRepo.Update(myStudent, false, false, true);
                    myStudent = _studentRepo.GetById(modelStudent.OldID);
                    myStudent.Benefactor = null;
                    student = _studentRepo.Update(myStudent, false, false, false);
                }
            }
            return RedirectToAction("Details/" + modelStudent.BenefactorID);
        }

        [HttpPost]
        public ActionResult DeleteStudent(long id)
        {
            var myStudent = _studentRepo.GetById(id);
            long ID = myStudent.Benefactor.PeopleId;
            myStudent.Benefactor = null;
            var student = _studentRepo.Update(myStudent, false, false, false);

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
