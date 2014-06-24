using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Logic;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class EventController : Controller
    {
        private readonly IAppointmentDiaryRepository _appointmentDiaryRepository;
        private readonly ISessionManagement _sessionManagement;
        private readonly IUserRepository _userRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public EventController(IAppointmentDiaryRepository appointmentDiaryRepository,
            ISessionManagement sessionManagement, IUserRepository userRepository)
        {
            _appointmentDiaryRepository = appointmentDiaryRepository;
            _sessionManagement = sessionManagement;
            _userRepository = userRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        //
        // GET: /Event/


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Agenda()
        {
            string userEmail = _sessionManagement.GetUserLoggedEmail();
            var appoinments = _appointmentDiaryRepository.Where(x => x.Creator.Email.CompareTo(userEmail) == 0&& x.IsAproveed);

            return View("Agenda", appoinments);
        }

        [HttpGet]
        public ActionResult Principal()
        {
            var role = _sessionManagement.GetUserLoggedRole();
            if (role.CompareTo("Principal")!=0)
            {
                return RedirectToAction("Index");
            }
            //var userName = _sessionManagement.GetUserLoggedName();


            var appoinments = _appointmentDiaryRepository.Query(x => x);

            return View("PrincipalIndex", appoinments);
        }

        
       // [HttpPost]
        public ActionResult Approve(int id)
        {
            var role = _sessionManagement.GetUserLoggedRole();
            if (role.CompareTo("Principal") != 0)
            {
                return RedirectToAction("Index");
            }

            AppointmentDiary rec = _appointmentDiaryRepository.First(s => s.Id == id);
            if (rec != null)
            {
                rec.IsAproveed = !rec.IsAproveed;
                _appointmentDiaryRepository.SaveChanges();
            }
            /*
            People people = _peopleRepository.GetById(peopleModel.Id);
            _peopleRepository.UpdatePeopleFromPeopleEditModel(peopleModel, people);

            const string title = "Persona Actualizada";
            var content = "La persona " + people.FullName + " - " + people.Id + " ha sido actualizada exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);*/

            return RedirectToAction("Principal");
        }

        public void UpdateEvent(int id, string NewEventStart, string NewEventEnd)
        {
            UpdateDiaryEvent(id, NewEventStart, NewEventEnd);
        }


        public bool SaveEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration)
        {
            string userEmail = _sessionManagement.GetUserLoggedEmail();
            User creator = _userRepository.First(x => x.Email == userEmail);
            return CreateNewEvent(Title, NewEventDate, NewEventTime, NewEventDuration, creator);
        }

        public JsonResult GetDiarySummary(double start, double end)
        {
            List<DiaryEvent> apptListForDate = LoadAppointmentSummaryInDateRange(start, end);
            var eventList = from e in apptListForDate
                select new
                       {
                           id = e.Id,
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
            List<DiaryEvent> apptListForDate = LoadAllAppointmentsInDateRange(start, end);
            var eventList = from e in apptListForDate
                select new
                       {
                           id = e.Id,
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
            DateTime fromDate = ConvertFromUnixTimestamp(start);
            DateTime toDate = ConvertFromUnixTimestamp(end);
            using (_appointmentDiaryRepository)
            {
                var loggedEmail = _sessionManagement.GetUserLoggedEmail();
                IQueryable<AppointmentDiary> rslt =
                    _appointmentDiaryRepository.Where(
                        s =>
                            s.Creator.Email.CompareTo(loggedEmail)==0 && s.DateTimeScheduled >= fromDate &&
                            DbFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate);

                var result = new List<DiaryEvent>();
                foreach (AppointmentDiary item in rslt)
                {
                    var rec = new DiaryEvent();
                    rec.Id = item.Id;
                    //rec.SomeImportantKeyID = item.SomeImportantKey;
                    rec.StartDateString = item.DateTimeScheduled.ToString("s");
                        // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                    rec.EndDateString = item.DateTimeScheduled.AddMinutes(item.AppointmentLength).ToString("s");
                        // field AppointmentLength is in minutes
                    rec.Title = item.Title + " - " + item.AppointmentLength + " mins";
                    rec.StatusString = Enums.GetName((AppointmentStatus) item.StatusEnum);
                    rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
                    string colorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                    rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1,
                        rec.StatusColor.Length - colorCode.Length - 1);
                    rec.StatusColor = colorCode;
                    if (item.IsAproveed)
                    {
                        result.Add(rec);
                    }
                }

                return result;
            }
        }


        public List<DiaryEvent> LoadAppointmentSummaryInDateRange(double start, double end)
        {
            DateTime fromDate = ConvertFromUnixTimestamp(start);
            DateTime toDate = ConvertFromUnixTimestamp(end);
            using (_appointmentDiaryRepository)
            {
                var loggedEmail = _sessionManagement.GetUserLoggedEmail();
                IQueryable<AppointmentDiary> rslt =
                    _appointmentDiaryRepository.Where(
                        s =>
                            s.Creator.Email.CompareTo(loggedEmail)==0 &&
                            s.DateTimeScheduled >= fromDate &&
                            DbFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate);

                var result = new List<DiaryEvent>();
                int i = 0;
                foreach (AppointmentDiary item in rslt)
                {
                    var rec = new DiaryEvent {Id = i};
                    string stringDate = string.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled);
                    rec.StartDateString = stringDate + "T00:00:00"; //ISO 8601 format
                    rec.EndDateString = stringDate + "T23:59:59";
                    rec.Title = item.Title;
                    if (item.IsAproveed)
                    {
                        result.Add(rec);
                    }
                        
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
                AppointmentDiary rec = _appointmentDiaryRepository.First(s => s.Id == id);
                if (rec != null)
                {
                    DateTime dateTimeStart =
                        DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime();
                        // and convert offset to localtime
                    rec.DateTimeScheduled = dateTimeStart;
                    if (!String.IsNullOrEmpty(NewEventEnd))
                    {
                        TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() -
                                        dateTimeStart;
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


        public bool CreateNewEvent(string title, string newEventDate, string newEventTime, string newEventDuration,
            User creator)
        {
            try
            {
                var rec = new AppointmentDiary();
                rec.Title = title;
                rec.DateTimeScheduled = DateTime.ParseExact(newEventDate + " " + newEventTime, "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture);
                rec.AppointmentLength = Int32.Parse(newEventDuration);
                rec.Creator = creator;
                _appointmentDiaryRepository.Create(rec);
                _appointmentDiaryRepository.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}