using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class EventController : Controller
    {
        private readonly EventRepository repository = new EventRepository(new MhotivoContext());
        
        // GET: /Event/

        public ActionResult Index()
        {
            return View(repository.Query(x => x));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventCreate eventCreation)
        {
            var eventModel = new Event
            {
                CreationDate = DateTime.Now,
                Description = eventCreation.Description,
                EndDateTime = eventCreation.EndDateTime,
                IsActive = true,
                StartDateTime = eventCreation.StartDateTime
            };
            repository.Create(eventModel);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}