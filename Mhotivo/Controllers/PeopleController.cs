using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
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

            return View(_peopleRepository.GetAllPeople());
        }

        [HttpGet]
        public ActionResult Edit(long Id)
        {
            PeopleEditModel people = _peopleRepository.GetPeopleEditModelById(Id);

            return View("Edit", people);
        }

        [HttpPost]
        public ActionResult Edit(PeopleEditModel peopleModel)
        {
            People people = _peopleRepository.GetById(peopleModel.Id);
            _peopleRepository.UpdatePeopleFromPeopleEditModel(peopleModel, people);

            const string title = "Persona Actualizada";
            string content = "La persona " + people.FullName + " - " + people.Id + " ha sido actualizada exitosamente.";

            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }
    }
}