using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    public class AppointmentDiary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Title { get; set; }
        public DateTime DateTimeScheduled { get; set; }
        public int StatusEnum { get; set; }
        public int AppointmentLength { get; set; }
        public User Creator { get; set; }
        public bool IsAproveed { get; set; }
    }
}