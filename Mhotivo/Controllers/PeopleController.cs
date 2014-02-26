
using System;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleRepository _peopleRepo = PeopleRepository.Instance;

        public ActionResult Index()
        {
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }

            return View(_peopleRepo.Query(x => x).ToList()
                .Select(x => new DisplayPeopleModel
                {
                    Address = x.Address,
                    BirthDay = x.BirthDate.ToShortDateString(),
                    PeopleId = x.PeopleId,
                    Sexo = _peopleRepo.SexLabel(x.Masculino),
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
            var people = _peopleRepo.GetById(peopleId);

            var editUser = new PeopleEditModel
            {
                FirstName = people.FirstName,
                LastName = people.LastName,
                Address = people.Address,
                PeopleId = people.PeopleId,
                Sexo = _peopleRepo.SexLabel(people.Masculino),
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
            var people = _peopleRepo.GetById(peopleModel.PeopleId);
            people.Address = peopleModel.Address;
            people.BirthDate = new DateTime();
            people.City = peopleModel.City;
            people.FirstName = peopleModel.FirstName;
            people.FullName = peopleModel.FirstName + " " + peopleModel.LastName;
            people.LastName = peopleModel.LastName;
            people.Masculino = _peopleRepo.IsMasculino(peopleModel.Sexo);
            people.Nationality = peopleModel.Nationality;
            people.State = peopleModel.State;
            people.UrlPicture = peopleModel.UrlPicture;

            var result = _peopleRepo.Update(people);
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
