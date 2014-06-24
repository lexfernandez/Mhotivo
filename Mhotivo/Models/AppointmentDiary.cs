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

    public class EventCreate
    {
        [Required(ErrorMessage = "Debe Ingresar Fecha de Inicio")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Fecha de Finalización")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Descripción")]
        public String Description { get; set; }
    }
}