
using System;
using System.Linq;
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

            return View(_peopleRepository.Query(x => x).ToList()
                .Select(x => new DisplayPeopleModel
                {
                    Address = x.Address,
                    BirthDay = x.BirthDate.ToShortDateString(),
                    PeopleId = x.PeopleId,
                    Sexo = Utilities.GenderToString(x.Gender),
                    City = x.City,
                    Nationality = x.Nationality,
                    State = x.State,
                    UrlPicture = x.UrlPicture,
                    FullName = x.FullName
                }));   
        }

        [HttpGet]
        public ActionResult Edit(long peopleId)
        {
            var people = _peopleRepository.GetById(peopleId);

            var editUser = new PeopleEditModel
            {
                FirstName = people.FirstName,
                LastName = people.LastName,
                Address = people.Address,
                PeopleId = people.PeopleId,
                Sexo = Utilities.GenderToString(people.Gender),
                BirthDay = people.BirthDate.ToShortDateString(),
                City = people.City,
                Nationality = people.Nationality,
                State = people.State,
                UrlPicture = people.UrlPicture
            };
            
            return View("Edit", editUser);
        }

        [HttpPost]
        public ActionResult Edit(PeopleEditModel peopleModel)
        {
            var people = _peopleRepository.GetById(peopleModel.PeopleId);
            people.Address = peopleModel.Address;
            people.BirthDate = new DateTime();
            people.City = peopleModel.City;
            people.FirstName = peopleModel.FirstName;
            people.FullName = peopleModel.FirstName + " " + peopleModel.LastName;
            people.LastName = peopleModel.LastName;
            people.Gender = Utilities.IsMasculino(peopleModel.Sexo);
            people.Nationality = peopleModel.Nationality;
            people.State = peopleModel.State;
            people.UrlPicture = peopleModel.UrlPicture;

            var result = _peopleRepository.Update(people);
            const string title = "Persona Actualizada";
            var content = "La persona " + result.FullName + " - " + result.PeopleId + " ha sido actualizada exitosamente.";

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
