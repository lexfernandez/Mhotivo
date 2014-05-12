using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class ContactInformationController : Controller
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IPeopleRepository _peopleRepository;

        public ContactInformationController(IContactInformationRepository contactInformationRepository,IPeopleRepository peopleRepository)
        {
            _contactInformationRepository = contactInformationRepository;
            _peopleRepository = peopleRepository;

        }
        

        [HttpPost]
        public ActionResult Edit(ContactInformationEditModel modelContactInformation)
        {
            var myContactInformation = _contactInformationRepository.GetById(modelContactInformation.Id);

            myContactInformation.Type = modelContactInformation.Type;
            myContactInformation.Value = modelContactInformation.Value;

            var contactInformation = _contactInformationRepository.Update(myContactInformation);
            const string title = "Contacto Actualizado";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title
            };

            return RedirectToAction("Details/" + contactInformation.People.PeopleId, modelContactInformation.Controller);
        }

        [HttpPost]
        public ActionResult Delete(long id, string control)
        {
            var myContactInformation = _contactInformationRepository.GetById(id);
            long ID = myContactInformation.People.PeopleId;
            var contactInformation = _contactInformationRepository.Delete(id);
            const string title = "Informacion Eliminada";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
            };

            return RedirectToAction("Details/" + ID, control);
        }

        [HttpGet]
        public ActionResult Add(long id)
        {
            var model = new ContactInformationRegisterModel
            {
                PeopleId = (int)id
            };
            return View("ContactAdd", model);
        }

        [HttpPost]
        public ActionResult Add(ContactInformationRegisterModel modelContactInformation)
        {
            var myContactInformation = new ContactInformation
            {
                Type = modelContactInformation.Type,
                Value = modelContactInformation.Value,
                People = _peopleRepository.GetById(modelContactInformation.PeopleId)
            };
            //_peopleRepository.Detach(myContactInformation.People);
            var contactInformation = _contactInformationRepository.Create(myContactInformation);
            
            const string title = "Informacion Agregada";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "SUCCESS",
                MessageTitle = title,
            };

            return RedirectToAction("Details/" + contactInformation.People.PeopleId, modelContactInformation.Controller);
        }
    }
}
