using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class MeisterController : Controller
    {
        private readonly MeisterRepository _meisterRepo = MeisterRepository.Instance;
        private PeopleRepository _peopleRepository = PeopleRepository.Instance;
        
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
            return View(_meisterRepo.Query(x => x).ToList()
                .Select(x => new DisplayMeisterModel
                {
                    MeisterID = x.PeopleId,
                    FullName = x.FullName,
                    DateOfBirth = x.BirthDate

                }));


        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            
            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                   Text = "Masculino",
                   Value = "M"
                },

                new SelectListItem
                {
                    Text = "Femenino",
                    Value = "F"
                }
            }, "Value", "Text"
            );

            var thismeister = _meisterRepo.GetById(id);
            var meister = new MeisterEditModel
            {
                FirstName = thismeister.FirstName,
                LastName = thismeister.LastName,
                FullName = (thismeister.FirstName + " " + thismeister.LastName),
                IDNumber = thismeister.IDNumber,
                DateOfBirth = thismeister.BirthDate.Date,
                Gender = _peopleRepository.SexLabel(thismeister.Gender),
                Nationality = thismeister.Nationality,
                State = thismeister.State,
                City = thismeister.City,
                StreetAddress = thismeister.Address,
                Id = thismeister.PeopleId,
                StartDate = thismeister.StartDate.Date,
                EndDate = thismeister.EndDate.Date,
                Biography = thismeister.Biography,

            };

            return View("Edit", meister);

        }

        [HttpPost]
        public ActionResult Edit(MeisterEditModel modelMeister)
        {
            var myMeister = _meisterRepo.GetById(modelMeister.Id);

            myMeister.FirstName = modelMeister.FirstName;
            myMeister.LastName = modelMeister.LastName;
            myMeister.FullName = (modelMeister.FirstName + " " + modelMeister.LastName);
            myMeister.IDNumber = modelMeister.IDNumber;
            myMeister.BirthDate = modelMeister.DateOfBirth.Date;
            myMeister.Gender = _peopleRepository.IsMasculino(modelMeister.Gender);
            myMeister.Nationality = modelMeister.Nationality;
            myMeister.State = modelMeister.State;
            myMeister.City = modelMeister.City;
            myMeister.Address = modelMeister.StreetAddress;
            myMeister.StartDate = modelMeister.StartDate.Date;
            myMeister.EndDate = modelMeister.EndDate.Date;
            myMeister.Biography = modelMeister.Biography;

            var meister = _meisterRepo.Update(myMeister);
            const string title = "Mestro Actualizado";
            var content = "El Maestro fue actualizado Exitosamente.";

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
            var meister = _meisterRepo.Delete(id);

            const string title = "Mestro Eliminado";
            var content = "El Maestro ha Sido Eliminado Exitosamente";
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

            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                   Text = "Masculino",
                   Value = "M"
                },

                new SelectListItem
                {
                    Text = "Femenino",
                    Value = "F"
                }
            }, "Value", "Text"
            );
            

            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(MeisterRegisterModel modelmeister)
        {
            var myMeister = new Meister
            {
                FirstName = modelmeister.FirstName,
                LastName = modelmeister.LastName,
                FullName = (modelmeister.FirstName + " " + modelmeister.LastName),
                IDNumber = modelmeister.IDNumber,
                BirthDate = modelmeister.DateOfBirth.Date,
                Gender = _peopleRepository.IsMasculino(modelmeister.Gender),
                Nationality = modelmeister.Nationality,
                State = modelmeister.State,
                City = modelmeister.City,
                Address = modelmeister.StreetAddress,
                StartDate = modelmeister.StartDate.Date,
                EndDate = modelmeister.EndDate.Date,
                Biography = modelmeister.Biography

            };          

            var meister = _meisterRepo.Create(myMeister);
            const string title = "Maestro Agreado";
            var content = "El Maestro ha sido Agregado Exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "SUCCES",
                MessageTitle = title,
                MessageContent = content

            };

            return RedirectToAction("Index");
        }

    }
}
