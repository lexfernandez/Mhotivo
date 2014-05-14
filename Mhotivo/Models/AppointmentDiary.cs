using System;

namespace Mhotivo.Models
{
    public class AppointmentDiary
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public DateTime DateTimeScheduled { get; set; }
        public int AppointmentLength { get; set; }
        public int StatusEnum { get; set; }
        public User Creator { get; set; }
        public bool IsActive { get; set; }
    }
}