using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class ParentController : Controller
    {
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

            return View(_parentRepo.Query(x => x).ToList()
                .Select(x => new DisplayParentModel
                {
                    ParentID = x.PeopleID,
                    FullName = x.FullName,
                    DateOfBirth = x.DateOfBirth,
                }));
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var thisParent = _parentRepo.GetById(id);
            var parent = new ParentEditModel
            {
                FirstName = thisParent.FirstName,
                LastName = thisParent.LastName,
                FullName = (thisParent.FirstName + " " + thisParent.LastName).Trim(),
                IDNumber = thisParent.IDNumber,
                DateOfBirth = thisParent.DateOfBirth.Date,
                Gender = thisParent.Gender,
                Nationality = thisParent.Nationality,
                State = thisParent.State,
                City = thisParent.City,
                StreetAddress = thisParent.StreetAddress,
                Id = thisParent.PeopleID,
            };

            return View("Edit", parent);
        }

        [HttpPost]
        public ActionResult Edit(ParentEditModel modelParent)
        {
            var myParent = _parentRepo.GetById(modelParent.Id);

            myParent.FirstName = modelParent.FirstName;
            myParent.LastName = modelParent.LastName;
            myParent.FullName = (modelParent.FirstName + " " + modelParent.LastName).Trim();
            myParent.IDNumber = modelParent.IDNumber;
            myParent.DateOfBirth = modelParent.DateOfBirth.Date;
            myParent.Gender = modelParent.Gender;
            myParent.Nationality = modelParent.Nationality;
            myParent.State = modelParent.State;
            myParent.City = modelParent.City;
            myParent.StreetAddress = modelParent.StreetAddress;

            var parent = _parentRepo.Update(myParent);
            const string title = "Padre o Tutor Actualizado";
            var content = "El Padre o Tutor ha sido actualizado exitosamente.";

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
            var parent = _parentRepo.Delete(id);

            const string title = "Usuario Eliminado";
            var content = "El Padre o Tutor ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(ParentRegisterModel modelParent)
        {
            var myParent = new Parent
            {
                FirstName = modelParent.FirstName,
                LastName = modelParent.LastName,
                FullName = (modelParent.FirstName + " " + modelParent.LastName).Trim(),
                IDNumber = modelParent.IDNumber,
                DateOfBirth = modelParent.DateOfBirth.Date,
                Gender = modelParent.Gender,
                Nationality = modelParent.Nationality,
                State = modelParent.State,
                City = modelParent.City,
                StreetAddress = modelParent.StreetAddress
            };

            var parent = _parentRepo.Create(myParent);
            const string title = "Padre o Tutor Agregado";
            var content = "El Padre o Tutor ha sido agregado exitosamente.";
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
