using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class MeisterController : Controller
    {
        private readonly IMeisterRepository _meisterRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IContactInformationRepository _contactInformationRepository;

        public MeisterController(IMeisterRepository meisterRepository, IPeopleRepository peopleRepository, IContactInformationRepository contactInformationRepository)
        {
            _meisterRepository = meisterRepository;
            _peopleRepository = peopleRepository;
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

            return View(_meisterRepository.Query(x => x).ToList()
                .Select(x => new DisplayMeisterModel
                {
                    MeisterID = x.PeopleId,
                    IDNumber = x.IDNumber,
                    UrlPicture = x.UrlPicture,
                    FullName = x.FullName,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    Nationality = x.Nationality,
                    Address = x.Address,
                    City = x.City,
                    State = x.State,
                    Country = x.Country,
                    Gender = Utilities.GenderToString(x.Gender),
                    Contacts = x.Contacts,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    StartDate = x.StartDate.ToShortDateString(),
                    EndDate = x.EndDate.ToShortDateString(),
                    Biography = x.Biography

                }));
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
                Controller = "Meister"
            };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var thisMeister = _meisterRepository.GetById(id);
            var meister = new MeisterEditModel
            {
                FirstName = thisMeister.FirstName,
                LastName = thisMeister.LastName,
                FullName = (thisMeister.FirstName + " " + thisMeister.LastName).Trim(),
                IDNumber = thisMeister.IDNumber,
                BirthDate = thisMeister.BirthDate.ToShortDateString(),
                Gender = Utilities.GenderToString(thisMeister.Gender),
                Nationality = thisMeister.Nationality,
                Country = thisMeister.Country,
                State = thisMeister.State,
                City = thisMeister.City,
                Address = thisMeister.Address,
                Id = thisMeister.PeopleId,
                StartDate = thisMeister.StartDate.ToShortDateString(),
                EndDate = thisMeister.EndDate.ToShortDateString(),
                Biography = thisMeister.Biography
            };

            return View("Edit", meister);
        }

        [HttpPost]
        public ActionResult Edit(MeisterEditModel modelMeister)
        {
            var myMeister = _meisterRepository.GetById(modelMeister.Id);

            myMeister.FirstName = modelMeister.FirstName;
            myMeister.LastName = modelMeister.LastName;
            myMeister.FullName = (modelMeister.FirstName + " " + modelMeister.LastName).Trim();
            myMeister.Country = modelMeister.Country;
            myMeister.IDNumber = modelMeister.IDNumber;
            myMeister.BirthDate = DateTime.Parse(modelMeister.BirthDate);
            myMeister.Gender = Utilities.IsMasculino(modelMeister.Gender);
            myMeister.Nationality = modelMeister.Nationality;
            myMeister.State = modelMeister.State;
            myMeister.City = modelMeister.City;
            myMeister.Address = modelMeister.Address;
            myMeister.StartDate = DateTime.Parse(modelMeister.StartDate);
            myMeister.EndDate = DateTime.Parse(modelMeister.EndDate);
            myMeister.Biography = modelMeister.Biography;

            var meister = _meisterRepository.Update(myMeister);
            const string title = "Maestro Actualizado";
            var content = "El maestro " + myMeister.FullName + " ha sido actualizado exitosamente.";

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
            var meister = _meisterRepository.Delete(id);

            const string title = "Maestro Eliminado";
            var content = "El maestro " + meister.FullName + " ha sido eliminado exitosamente.";
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
                Controller = "Meister"
            };
            return View("ContactAdd", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(MeisterRegisterModel modelMeister)
        {
            var myMeister = new Meister
            {
                FirstName = modelMeister.FirstName,
                LastName = modelMeister.LastName,
                FullName = (modelMeister.FirstName + " " + modelMeister.LastName).Trim(),
                IDNumber = modelMeister.IDNumber,
                BirthDate = DateTime.Parse(modelMeister.BirthDate),
                Gender = Utilities.IsMasculino(modelMeister.Gender),
                Nationality = modelMeister.Nationality,
                State = modelMeister.State,
                Country = modelMeister.Country,
                City = modelMeister.City,
                Address = modelMeister.Address,
                StartDate = DateTime.Parse(modelMeister.StartDate),
                EndDate = DateTime.Parse(modelMeister.EndDate),
                Biography = modelMeister.Biography
            };

            var meister = _meisterRepository.Create(myMeister);
            const string title = "Maestro Agregado";
            var content = "El maestro " + myMeister.FullName + "ha sido agregado exitosamente.";
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
            var thisMeister = _meisterRepository.GetById(id);
            var meister = new DisplayMeisterModel
            {
                MeisterID = thisMeister.PeopleId,
                IDNumber = thisMeister.IDNumber,
                UrlPicture = thisMeister.UrlPicture,
                FirstName = thisMeister.FirstName,
                LastName = thisMeister.LastName,
                FullName = thisMeister.FullName,
                BirthDate = thisMeister.BirthDate.ToShortDateString(),
                Nationality = thisMeister.Nationality,
                Address = thisMeister.Address,
                City = thisMeister.City,
                State = thisMeister.State,
                Country = thisMeister.Country,
                Gender = Utilities.GenderToString(thisMeister.Gender),
                Contacts = thisMeister.Contacts,
                StartDate = thisMeister.StartDate.ToShortDateString(),
                EndDate = thisMeister.EndDate.ToShortDateString(),
                Biography = thisMeister.Biography
            };

            return View("Details", meister);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            var thisMeister = _meisterRepository.GetById(id);
            var meister = new MeisterEditModel
            {
                FirstName = thisMeister.FirstName,
                LastName = thisMeister.LastName,
                FullName = (thisMeister.FirstName + " " + thisMeister.LastName).Trim(),
                IDNumber = thisMeister.IDNumber,
                BirthDate = thisMeister.BirthDate.ToShortDateString(),
                Gender = Utilities.GenderToString(thisMeister.Gender),
                Nationality = thisMeister.Nationality,
                Country = thisMeister.Country,
                State = thisMeister.State,
                City = thisMeister.City,
                Address = thisMeister.Address,
                Id = thisMeister.PeopleId,
                StartDate = thisMeister.StartDate.ToShortDateString(),
                EndDate = thisMeister.EndDate.ToShortDateString(),
                Biography = thisMeister.Biography
            };
            return View("DetailsEdit", meister);
        }

        [HttpPost]
        public ActionResult DetailsEdit(MeisterEditModel modelMeister)
        {
            var myMeister = _meisterRepository.GetById(modelMeister.Id);
            myMeister.FirstName = modelMeister.FirstName;
            myMeister.LastName = modelMeister.LastName;
            myMeister.FullName = (modelMeister.FirstName + " " + modelMeister.LastName).Trim();
            myMeister.IDNumber = modelMeister.IDNumber;
            myMeister.BirthDate = DateTime.Parse(modelMeister.BirthDate);
            myMeister.Gender = Utilities.IsMasculino(modelMeister.Gender);
            myMeister.Nationality = modelMeister.Nationality;
            myMeister.State = modelMeister.State;
            myMeister.City = modelMeister.City;
            myMeister.Country = modelMeister.Country;
            myMeister.Address = modelMeister.Address;
            myMeister.StartDate = DateTime.Parse(modelMeister.StartDate);
            myMeister.EndDate = DateTime.Parse(modelMeister.EndDate);
            myMeister.Biography = modelMeister.Biography;

            var meister = _meisterRepository.Update(myMeister);
            const string title = "Maestro Actualizado";
            var content = "El maestro " + myMeister.FullName + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Details/" + modelMeister.Id);
        }
    }
}
