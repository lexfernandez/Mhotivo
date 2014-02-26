using System;

namespace Mhotivo.Models
{
    public class AppointmentDiary
    {
        public int AppointmentDiaryId { get; set; }
        public String Title { get; set; }
        public int SomeImportantKey { get; set; }
        public DateTime DateTimeScheduled { get; set; }
        public int AppointmentLength { get; set; }
        public int StatusENUM { get; set; }
    }
}