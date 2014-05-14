using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class ContactInformationController : Controller
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IPeopleRepository _peopleRepository;

        public ContactInformationController(IContactInformationRepository contactInformationRepository,
            IPeopleRepository peopleRepository)
        {
            _contactInformationRepository = contactInformationRepository;
            _peopleRepository = peopleRepository;
        }


        [HttpPost]
        public ActionResult Edit(ContactInformationEditModel modelContactInformation)
        {
            ContactInformation myContactInformation = _contactInformationRepository.GetById(modelContactInformation.Id);

            myContactInformation.Type = modelContactInformation.Type;
            myContactInformation.Value = modelContactInformation.Value;

            ContactInformation contactInformation = _contactInformationRepository.Update(myContactInformation);
            const string title = "Contacto Actualizado";

            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title
                                      };

            return RedirectToAction("Details/" + contactInformation.People.Id, modelContactInformation.Controller);
        }

        [HttpPost]
        public ActionResult Delete(long id, string control)
        {
            ContactInformation myContactInformation = _contactInformationRepository.GetById(id);
            long ID = myContactInformation.People.Id;
            ContactInformation contactInformation = _contactInformationRepository.Delete(id);
            const string title = "Informacion Eliminada";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                      };

            return RedirectToAction("Details/" + ID, control);
        }

        [HttpGet]
        public ActionResult Add(long id)
        {
            var model = new ContactInformationRegisterModel
                        {
                            Id = (int) id
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
                                           People = _peopleRepository.GetById(modelContactInformation.Id)
                                       };
            ContactInformation contactInformation = _contactInformationRepository.Create(myContactInformation);

            const string title = "Informacion Agregada";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "SUCCESS",
                                          Title = title,
                                      };

            return RedirectToAction("Details/" + contactInformation.People.Id, modelContactInformation.Controller);
        }
    }
}