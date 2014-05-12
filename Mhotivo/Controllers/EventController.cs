using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
        private readonly IAppointmentDiaryRepository _appointmentDiaryRepository;

        public EventController(IAppointmentDiaryRepository appointmentDiaryRepository)
        {
            _appointmentDiaryRepository = appointmentDiaryRepository;
        }

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
            UpdateDiaryEvent(id, NewEventStart, NewEventEnd);
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
            var ApptListForDate = LoadAppointmentSummaryInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.DiaryEventId,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                someKey = e.SomeImportantKeyId,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiaryEvents(double start, double end)
        {
            var ApptListForDate = LoadAllAppointmentsInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.DiaryEventId,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.StatusColor,
                                className = e.ClassName,
                                someKey = e.SomeImportantKeyId,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public List<DiaryEvent> LoadAllAppointmentsInDateRange(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            using (_appointmentDiaryRepository)
            {
                var rslt = _appointmentDiaryRepository.Where(s => s.DateTimeScheduled >= fromDate && DbFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate);

                var result = new List<DiaryEvent>();
                foreach (var item in rslt)
                {
                    DiaryEvent rec = new DiaryEvent();
                    rec.DiaryEventId = item.AppointmentDiaryId;
                    rec.SomeImportantKeyId = item.SomeImportantKey;
                    rec.StartDateString = item.DateTimeScheduled.ToString("s"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                    rec.EndDateString = item.DateTimeScheduled.AddMinutes(item.AppointmentLength).ToString("s"); // field AppointmentLength is in minutes
                    rec.Title = item.Title + " - " + item.AppointmentLength.ToString() + " mins";
                    rec.StatusString = Enums.GetName<AppointmentStatus>((AppointmentStatus)item.StatusENUM);
                    rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
                    string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                    rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                    rec.StatusColor = ColorCode;
                    result.Add(rec);
                }

                return result;
            }

        }


        public List<DiaryEvent> LoadAppointmentSummaryInDateRange(double start, double end)
        {

            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            using (_appointmentDiaryRepository)
            {
                var rslt = _appointmentDiaryRepository.Where(s => s.DateTimeScheduled >= fromDate && DbFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate)
                                                        .GroupBy(s => DbFunctions.TruncateTime(s.DateTimeScheduled))
                                                        .Select(x => new { DateTimeScheduled = x.Key, Count = x.Count() });

                List<DiaryEvent> result = new List<DiaryEvent>();
                int i = 0;
                foreach (var item in rslt)
                {
                    DiaryEvent rec = new DiaryEvent();
                    rec.DiaryEventId = i; //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                    rec.SomeImportantKeyId = -1;
                    string StringDate = string.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled);
                    rec.StartDateString = StringDate + "T00:00:00"; //ISO 8601 format
                    rec.EndDateString = StringDate + "T23:59:59";
                    rec.Title = "Booked: " + item.Count.ToString();
                    result.Add(rec);
                    i++;
                }

                return result;
            }

        }

        public void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd)
        {
            // EventStart comes ISO 8601 format, eg:  "2000-01-10T10:00:00Z" - need to convert to DateTime
            using (_appointmentDiaryRepository)
            {
                var rec = _appointmentDiaryRepository.First(s => s.AppointmentDiaryId == id);
                if (rec != null)
                {
                    DateTime DateTimeStart = DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime(); // and convert offset to localtime
                    rec.DateTimeScheduled = DateTimeStart;
                    if (!String.IsNullOrEmpty(NewEventEnd))
                    {
                        TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart;
                        rec.AppointmentLength = Convert.ToInt32(span.TotalMinutes);
                    }
                    _appointmentDiaryRepository.SaveChanges();
                }
            }

        }


        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public bool CreateNewEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration)
        {
            try
            {
                AppointmentDiary rec = new AppointmentDiary();
                rec.Title = Title;
                rec.DateTimeScheduled = DateTime.ParseExact(NewEventDate + " " + NewEventTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                rec.AppointmentLength = Int32.Parse(NewEventDuration);
                _appointmentDiaryRepository.Create(rec);
                _appointmentDiaryRepository.SaveChanges();
            }
            catch (Exception c)
            {
                return false;
            }
            return true;
        }

    }

    
}
