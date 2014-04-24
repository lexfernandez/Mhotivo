using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Mhotivo.App_Data;
using Mhotivo.App_Data.Repositories;
using System.Data.Entity;
using System.Globalization;

namespace Mhotivo.Models
{
    public class DiaryEvent
    {
        public int DiaryEventId;
        public String Title;
        public int SomeImportantKeyID;
        public String StartDateString;
        public String EndDateString;
        public String StatusString;
        public String StatusColor;
        public String ClassName;

        public static List<DiaryEvent> LoadAllAppointmentsInDateRange(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            using (AppointmentDiaryRepository ent = AppointmentDiaryRepository.Instance)
            {
                var rslt = ent.Where(s => s.DateTimeScheduled >= fromDate && DbFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate);

                List<DiaryEvent> result = new List<DiaryEvent>();
                foreach (var item in rslt)
                {
                    DiaryEvent rec = new DiaryEvent();
                    rec.DiaryEventId = item.AppointmentDiaryId;
                    //rec.SomeImportantKeyID = item.SomeImportantKey;
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


        public static List<DiaryEvent> LoadAppointmentSummaryInDateRange(double start, double end)
        {

            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            using (AppointmentDiaryRepository ent = AppointmentDiaryRepository.Instance)
            {
                var rslt =
                    ent.Where(
                        s =>
                            s.DateTimeScheduled >= fromDate &&
                            DbFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate);

                List<DiaryEvent> result = new List<DiaryEvent>();
                int i = 0;
                foreach (var item in rslt)
                {
                    DiaryEvent rec = new DiaryEvent();
                    rec.DiaryEventId = i;
                    string StringDate = string.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled);
                    rec.StartDateString = StringDate + "T00:00:00"; //ISO 8601 format
                    rec.EndDateString = StringDate + "T23:59:59";
                    rec.Title = item.Title;
                    result.Add(rec);
                    i++;
                }

                return result;
            }

        }

        public static void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd)
        {
            // EventStart comes ISO 8601 format, eg:  "2000-01-10T10:00:00Z" - need to convert to DateTime
            using (AppointmentDiaryRepository ent = AppointmentDiaryRepository.Instance)
            {
                var rec = ent.First(s => s.AppointmentDiaryId == id);
                if (rec != null)
                {
                    DateTime DateTimeStart = DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime(); // and convert offset to localtime
                    rec.DateTimeScheduled = DateTimeStart;
                    if (!String.IsNullOrEmpty(NewEventEnd))
                    {
                        TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart;
                        rec.AppointmentLength = Convert.ToInt32(span.TotalMinutes);
                    }
                    ent.SaveChanges();
                }
            }

        }


        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static bool CreateNewEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration, User creator)
        {
            try
            {
                AppointmentDiaryRepository ent = AppointmentDiaryRepository.Instance;
                AppointmentDiary rec = new AppointmentDiary();
                rec.Title = Title;
                rec.DateTimeScheduled = DateTime.ParseExact(NewEventDate + " " + NewEventTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                rec.AppointmentLength = Int32.Parse(NewEventDuration);
                rec.Creator = creator;
                ent.Create(rec);
                ent.SaveChanges();
            }
            catch (Exception c)
            {
                return false;
            }
            return true;
        }
    }
}