using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Logic;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/
        private readonly UserRepository _userRepo = UserRepository.Instance;

        public ActionResult Index()
        {
            return View();
        }
        /*
        public ActionResult Approve()
        {
            ISessionManagement s = SessionLayer.Instance;
            if (s.GetUserLoggedRole() != "principal")
            {
                RedirectToAction("Index");
            }



            return View();
        }*/

        public void UpdateEvent(int id, string NewEventStart, string NewEventEnd)
        {
            DiaryEvent.UpdateDiaryEvent(id, NewEventStart, NewEventEnd);
        }


        public bool SaveEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration)
        {
            ISessionManagement s = SessionLayer.Instance;
            var username = s.GetUserLoggedName();
            var creator = _userRepo.First(x => x.Email == username);
            return DiaryEvent.CreateNewEvent(Title, NewEventDate, NewEventTime, NewEventDuration, creator);
        }

        public JsonResult GetDiarySummary(double start, double end)
        {
            var ApptListForDate = DiaryEvent.LoadAppointmentSummaryInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.DiaryEventId,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiaryEvents(double start, double end)
        {
            var ApptListForDate = DiaryEvent.LoadAllAppointmentsInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.DiaryEventId,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.StatusColor,
                                className = e.ClassName,
                                someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

    }

    
}
