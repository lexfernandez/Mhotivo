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
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }

            return View(_peopleRepository.GetAllPeople());
        }

        [HttpGet]
        public ActionResult Edit(long peopleId)
        {
            var people = _peopleRepository.GetPeopleEditModelById(peopleId);

            return View("Edit", people);
        }

        [HttpPost]
        public ActionResult Edit(PeopleEditModel peopleModel)
        {
            var people = _peopleRepository.GetById(peopleModel.PeopleId);
            _peopleRepository.UpdatePeopleFromPeopleEditModel(peopleModel, people);
            
            const string title = "Persona Actualizada";
            var content = "La persona " + people.FullName + " - " + people.PeopleId + " ha sido actualizada exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

    }
}
