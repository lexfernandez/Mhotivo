
using System;
using System.Collections.Generic;
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
                    Sexo = _peopleRepo.SexLabel(x.Gender),
                    City = x.City,
                    Nationality = x.Nationality,
                    State = x.State,
                    UrlPicture = x.UrlPicture,
                    FullName = x.FullName,
                    Identification = x.IDNumber
                }));   
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var people = _peopleRepo.GetById(id);

            var editUser = new PeopleEditModel
            {
                FirstName = people.FirstName,
                LastName = people.LastName,
                Address = people.Address,
                PeopleId = people.PeopleId,
                Sexo = _peopleRepo.SexLabel(people.Gender),
                BirthDay = people.BirthDate.ToShortDateString(),
                City = people.City,
                Nationality = people.Nationality,
                State = people.State,
                UrlPicture = people.UrlPicture,
                Identification = people.IDNumber
            };

            
            ViewBag.Sexo = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = "Masculino", Text = "Masculino", Selected = _peopleRepo.IsMasculino("Masculino")
                    }, 
                    new SelectListItem
                    {
                        Text = "Femenino", Value = "Femenino", Selected = _peopleRepo.IsMasculino("Femenino")
                    }
                },"Value","Text", _peopleRepo.SexLabel(people.Gender));
            
            return View("Edit", editUser);
        }

        [HttpPost]
        public ActionResult Edit(PeopleEditModel peopleModel)
        {
            var people = _peopleRepo.GetById(peopleModel.PeopleId);
            people.Address = peopleModel.Address;
            people.BirthDate = DateTime.Parse(peopleModel.BirthDay);
            people.City = peopleModel.City;
            people.FirstName = peopleModel.FirstName;
            people.FullName = peopleModel.FirstName + " " + peopleModel.LastName;
            people.LastName = peopleModel.LastName;
            people.Gender = _peopleRepo.IsMasculino(peopleModel.Sexo);
            people.Nationality = peopleModel.Nationality;
            people.State = peopleModel.State;
            people.UrlPicture = peopleModel.UrlPicture;
            people.IDNumber = peopleModel.Identification;

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

        [HttpPost]
        public ActionResult Delete(long id)
        {
            var parent = _peopleRepo.Delete(id);

            const string title = "Persona Eliminada";
            var content = "La persona " + parent.FullName + " ha sido eliminado exitosamente.";
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
